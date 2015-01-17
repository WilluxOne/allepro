using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AllePro.AllegroApiService;

namespace AllePro
{
  public partial class MainForm : Form
  {
    private String SessionHandle = "";
    AllegroWebApiPortTypeClient AllegroClient = null;
    List<ItemData> Items = new List<ItemData>();
    List<ItemClass> ClusteredItems = null;

    enum AppState
    {
      Login,
      Search,
      Cluster,
      View
    };

    Random rand = new Random();

    private double ItemClassificator1(ItemData item, ItemData item2)
    {
      return rand.NextDouble();
    }

    private double ItemClassificator2(ItemData item, ItemData item2)
    {
      return rand.NextDouble();
    }

    private double ItemClassificator3(ItemData item, ItemData item2)
    {
      return rand.NextDouble();
    }

    private double ItemClassificator4(ItemData item, ItemData item2)
    {
      return rand.NextDouble();
    }

    private AppState GlobalState = AppState.Login;

    public MainForm()
    {
      InitializeComponent();

      AllegroClient = new AllegroWebApiPortTypeClient();
    }

    private void DisableAllUIControls()
    {
      LoginButton.Enabled = false;
      UserNameField.Enabled = false;
      PasswordField.Enabled = false;

      SearchButton.Enabled = false;
      SearchQueryField.Enabled = false;

      ClusterButton.Enabled = false;
    }

    private void EnterResumeWaiting(bool state, string OperationName)
    {
      if (!state)
      {
        StatusLabel.Text = OperationName;
        StatusLabel.Visible = true;
        progressBar1.Style = ProgressBarStyle.Marquee;
        DisableAllUIControls();
      }
      else
      {
        StatusLabel.Text = "";
        StatusLabel.Visible = false;
        progressBar1.Style = ProgressBarStyle.Blocks;

        switch (GlobalState)
        {
          case AppState.Login:
            LoginButton.Enabled = true;
            UserNameField.Enabled = true;
            PasswordField.Enabled = true;

            SearchButton.Enabled = false;
            SearchQueryField.Enabled = false;

            ClusterButton.Enabled = false;
            break;
          case AppState.Search:
            LoginButton.Enabled = false;
            UserNameField.Enabled = false;
            PasswordField.Enabled = false;

            SearchButton.Enabled = true;
            SearchQueryField.Enabled = true;

            ClusterButton.Enabled = false;
            break;
          case AppState.Cluster:
            LoginButton.Enabled = false;
            UserNameField.Enabled = false;
            PasswordField.Enabled = false;

            SearchButton.Enabled = false;
            SearchQueryField.Enabled = false;

            ClusterButton.Enabled = true;
            break;
          case AppState.View:
            ClusterButton.Enabled = false;
            ClusterListView.Enabled = true;
            break;
          default:
            throw new Exception("Unknown app state!");
        }
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      string[] Args = { SessionHandle, SearchQueryField.Text };
      EnterResumeWaiting(false, "Searching in Allegro...");

      Items.Clear();
      SearchWorker.RunWorkerAsync((object)Args);
    }

    private void button2_Click(object sender, EventArgs e)
    {
      EnterResumeWaiting(false, "Authenticating...");
      string[] UserData = { UserNameField.Text, PasswordField.Text };
      LoginWorker.RunWorkerAsync((object)UserData);
    }

    private void button3_Click(object sender, EventArgs e)
    {
      EnterResumeWaiting(false, "Clustering the results...");
      ClusterWorker.RunWorkerAsync(Items);
    }

    private void LoginWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      string[] UserData = (string[]) e.Argument;

      if (UserData == null || UserData.Length < 2)
      {
        e.Result = null;
        return;
      }

      long UserId = 0;
      long ServerTime = 0;
      e.Result = AllegroClient.doLogin(out UserId, out ServerTime, UserData[0], UserData[1], 1, "a938874e", 1421323001);
    }

    private void LoginWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      SessionHandle = (String)e.Result;

      if (SessionHandle != null && SessionHandle.Length > 0)
      {
        GlobalState = AppState.Search;
        EnterResumeWaiting(true, "");
      }
      else
      {
        GlobalState = AppState.Login;
        EnterResumeWaiting(true, "");
        MessageBox.Show("Could not login!");
      }
    }

    private void SearchWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      string[] Args = (string[])e.Argument;
      List<ItemData> NewElements = new List<ItemData>();

      if (Args == null || Args[0] == null || Args[0].Length == 0)
      {
        e.Result = new Exception("Passed argument is invalid!");
        return;
      }

      int Found = 0;
      SearchResponseType[] ResponseTypes;
      string[] ExcludedWords;
      CategoriesStruct[] FoundCathegories;
      SearchOptType Query = new SearchOptType();
      Query.searchstring = Args[1];
      Query.searchlimit = 100;
      Query.searchoffset = 0;

      try
      {
        do
        {
          AllegroClient.doSearch(out Found, out ResponseTypes, out ExcludedWords, out FoundCathegories, Args[0], Query);

          foreach (SearchResponseType Item in ResponseTypes)
          {
            NewElements.Add(new ItemData(Item.sitid, Item.sitname, Item.sitthumburl, Item.sitsellerinfo.sellerid, Item.sitcategoryid, Item.sitprice));
          }

          Query.searchoffset += 1;
        } while (ResponseTypes.Length > 0);
      }
      catch (Exception error)
      {
        e.Result = error;
      }

      e.Result = NewElements;
    }

    private void SearchWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (e.Result is Exception)
      {
        MessageBox.Show(((Exception)e.Result).Message);
      }
      else
      {
        Items = (List<ItemData>)e.Result;
      }

      if (Items.Count > 0)
      {
        SearchLabel.Text = "Found " + Items.Count.ToString() + " items!";
        GlobalState = AppState.Cluster;
        EnterResumeWaiting(true, "");
      }
      else
      {
        GlobalState = AppState.Search;
        EnterResumeWaiting(true, "");
        SearchLabel.Text = "No items found!";
      }
    }

    private void ClusterWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      if (e.Argument == null || !(e.Argument is List<ItemData>))
      {
        e.Result = new Exception("Wrong call arguments!");
        return;
      }

      List<ItemData> SourceList = (List<ItemData>)e.Argument;
      List<ItemClass> ResultClusters = new List<ItemClass>();

      double[] weights = { 1.0, 1.0, 1.0, 1.0 };
      double threshold = 2.0;

      foreach (ItemData Item1 in SourceList)
      {
        foreach (ItemData Item2 in SourceList)
        {

        }
      }

      foreach (ItemData item in SourceList)
      {
        double[] itemValues = { ItemClassificator1(item), ItemClassificator2(item), ItemClassificator3(item), ItemClassificator4(item) };
        double itemValue = 0.0;
        for (int i = 0; i < itemValues.Length; i++)
        {
          itemValue += itemValues[i] * weights[i];
        }

        if (ResultClusters.Count == 0)
        {
          ResultClusters.Add(new ItemClass());
          ResultClusters[0].Elements.Add(item);
          ResultClusters[0].MeanValue = itemValue;
        }
        else
        {
          int ClosestClusterIndex = 0;
          int i = 0;
          double ClosestClusterDistance = Double.MaxValue; // Lest start from a giant length so that everything will bo closer
          foreach (ItemClass cluster in ResultClusters)
          {
            if (Math.Abs(itemValue - cluster.MeanValue) < ClosestClusterDistance)
            {
              ClosestClusterDistance = Math.Abs(itemValue - cluster.MeanValue);
              ClosestClusterIndex = i;
            }
            i++;
          }
          if (ClosestClusterDistance == Double.MaxValue || ClosestClusterDistance > threshold)
          {
            // No suitable clusters found, create a new one
            ResultClusters.Add(new ItemClass());
            ResultClusters[ResultClusters.Count - 1].Elements.Add(item);
            ResultClusters[ResultClusters.Count - 1].MeanValue = itemValue;
          }
          else
          {
            ResultClusters[ClosestClusterIndex].Elements.Add(item);
            ResultClusters[ClosestClusterIndex].MeanValue *= (ResultClusters[ClosestClusterIndex].Elements.Count - 1);
            ResultClusters[ClosestClusterIndex].MeanValue += itemValue;
            ResultClusters[ClosestClusterIndex].MeanValue /= ResultClusters[ClosestClusterIndex].Elements.Count;
          }
        }
      }

      foreach (ItemClass Class in ResultClusters)
      {
        Class.SortByPrice();
      }

      e.Result = ResultClusters;
    }

    private void ClusterWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (!(e.Result is List<ItemClass>)) {
        GlobalState = AppState.Search;
        EnterResumeWaiting(true, "");
        MessageBox.Show("Could not cluster the data.");
        return;
      }
      ClusteredItems = (List<ItemClass>)e.Result;

      ClusterImages.ImageSize = new Size(128, 96);

      foreach(ItemClass cluster in ClusteredItems)
      {
        ClusterImages.Images.Add(cluster.Elements[0].GetImageKey(), cluster.Elements[0].GetImage());
        ClusterListView.LargeImageList = ClusterImages;
        ClusterListView.Items.Add(cluster.Elements[0].AuctionName + " Price: " + cluster.Elements[0].Price, cluster.Elements[0].GetImageKey());
      }

      GlobalState = AppState.View;
      EnterResumeWaiting(true, "");
    }

    private void ClusterListView_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ClusterListView.SelectedItems.Count == 1)
      {
        int index = ClusterListView.SelectedItems[0].Index;
        ClusterViewer ClusterView = new ClusterViewer(ClusteredItems[index].Elements);
        ClusterView.ShowDialog();
      }
    }
  }
}
