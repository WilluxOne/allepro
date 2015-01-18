using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AllePro.AllegroApiService;
using FuzzyString;
using XnaFan.ImageComparison;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Globalization;

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

    public void parseName(ItemData item)
    {
      if (item.ParsedName != null)
      {
        return;
      }

      String cleanedName = item.AuctionName.ToLower();
      cleanedName = cleanedName.Replace("+", " ");
      cleanedName = cleanedName.Replace("-", " ");
      cleanedName = cleanedName.Replace("!", " ");
      cleanedName = cleanedName.Replace("@", " ");
      cleanedName = cleanedName.Replace("\"", " ");
      cleanedName = cleanedName.Replace("'", " ");
      cleanedName = cleanedName.Replace(",", " ");
      cleanedName = cleanedName.Replace(".", " ");
      cleanedName = cleanedName.Replace("\\", " ");
      cleanedName = cleanedName.Replace("/", " ");
      cleanedName = cleanedName.Replace("*", " ");
      cleanedName = cleanedName.Replace("=", " ");
      cleanedName = cleanedName.Replace("nowy", " ");
      cleanedName = cleanedName.Replace("używany", " ");
      cleanedName = cleanedName.Replace("super", " ");
      cleanedName = cleanedName.Replace("tanio", " ");
      cleanedName = cleanedName.Replace("tani", " ");
      cleanedName = cleanedName.Replace("gratis", " ");
      cleanedName = cleanedName.Replace("okazja", " ");
      cleanedName = cleanedName.Replace("cena", " ");
      cleanedName = cleanedName.Replace("cenie", " ");
      cleanedName = cleanedName.Replace("prezent", " ");
      cleanedName = cleanedName.Replace("hit", " ");
      cleanedName = cleanedName.Replace("szybki", " ");
      cleanedName = cleanedName.Replace("szybko", " ");
      cleanedName = cleanedName.Replace("wyprzedaż", " ");
      String[] ss = cleanedName.Split(new char[]{' '});
      List<String> pName = new List<String>();
      foreach(String s in ss) {
        if (s != null && s.Length > 0)
        {
          pName.Add(s);
        }
      }
      item.ParsedName = pName;
    }

    public double[] getNameSimilarity(ItemData first, ItemData second)
    {
      parseName(first);
      parseName(second);

      double similarity = 0;
      int numTheSameWords = 0;
      List<String> shoterNameWords = first.ParsedName.Count < second.ParsedName.Count ? first.ParsedName
          : second.ParsedName;
      List<String> longerNameWords = first.ParsedName.Count >= second.ParsedName.Count ? first.ParsedName
          : second.ParsedName;
      double oneWordSimilarityWeight = (double)1 / (double)shoterNameWords.Count;
      for (int i = 0; i < shoterNameWords.Count; i++)
      {
        if (longerNameWords.Contains(shoterNameWords[i]))
        {
          similarity += oneWordSimilarityWeight;
          numTheSameWords++;
        }
      }
      return new double[] { similarity, numTheSameWords };
    }

    public bool isTheSameProduct(ItemData first, ItemData second)
    {
      double[] nameSimilarityTab = getNameSimilarity(first, second);
      double nameSimilarity = nameSimilarityTab[0];
      double nameSimilarityWordsNum = nameSimilarityTab[1];
      if (nameSimilarity < 0.3)
      {
        return false;
      }
      //System.Console.Out.WriteLine("NameSimilarity=" + nameSimilarity + " dla " + this + " i " + first.AuctionName);
      // if (nameSimilarity >= 1) {
      // return true;
      // }

      double imageSimilarity = ItemClassificator4(first, second);
        //getImageSimilarity(first, second);
      //System.Console.Out.WriteLine("ImageSimilarity=" + imageSimilarity + " dla " + this + " i " + p);
      if (imageSimilarity >= 0.98)
      {
        return true;
      }

      if (nameSimilarityWordsNum >= 5 && nameSimilarity > 0.7)
      {
        //System.Console.Out.WriteLine("High name similarity=" + nameSimilarity + " dla " + this + " i " + p
        //    + ". Num of the same words: " + nameSimilarityWordsNum);
        return true;
      }

      //System.Console.Out.WriteLine("Name+ImageSimilarity=" + (imageSimilarity + nameSimilarity) + " dla " + this + " i " + p);
      if (imageSimilarity + nameSimilarity > 1.7)
      {
        return true;
      }
      return false;
    }

    private double ItemClassificator1(ItemData item, ItemData item2)
    {
      if (item.UserName == item2.UserName)
      {
        return 1.0;
      }
      return 0.0;
    }

    private double ItemClassificator2(ItemData item, ItemData item2)
    {
      if (item.CathegoryId == item2.CathegoryId)
      {
        return 1.0;
      }
      return 0.0;
    }

    private double ItemClassificator3(ItemData item, ItemData item2)
    {
      string name1 = item.AuctionName;
      string name2 = item2.AuctionName;

      name1 = name1.ToLower();
      name2 = name2.ToLower();

      string[] ToRemove = {
                          "kolorów",
                          "kolory",
                          "promocja",
                          "super",
                          "okazja",
                          "zwrot",
                          "gwarancja",
                          "wysyłka",
                          "kurier",
                          "hit",
                          "fv",
                          "bcm"};

      List<string> ToRemoveList = new List<string>(ToRemove);

      String additionalToRemove = SearchQueryField.Text.ToLower();

      foreach (string AdditionalElem in SearchQueryField.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
      {
        ToRemoveList.Add(AdditionalElem);
      }

      foreach (string BlackWord in ToRemoveList)
      {
        name1 = name1.Replace(BlackWord, "");
        name2 = name2.Replace(BlackWord, "");
      }

      RegexOptions options = RegexOptions.None;
      Regex regex = new Regex(@"[ ]{2,}", options);
      name1 = regex.Replace(name1, @" ");
      name2 = regex.Replace(name2, @" ");

      name1 = name1.Trim();
      name2 = name2.Trim();
      double test = ComparisonMetrics.RatcliffObershelpSimilarity(name1, name2);
      return test; // ComparisonMetrics.LevenshteinDistance(name1, name2); //RatcliffObershelpSimilarity(name1, name2);
    }

    private double ItemClassificator4(ItemData item, ItemData item2)
    {
      
      double test = 0.0;
      /*
      try
      {
        // Redirect the output stream of the child process.
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.CreateNoWindow = true;
        //p.StartInfo.WorkingDirectory = "cygwinDLLs\\";
        //p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.RedirectStandardError = true;
        p.StartInfo.FileName = "cygwinDLLs\\compare.exe";
        p.StartInfo.Arguments = String.Format("-metric ncc Images\\{0}.jpg Images\\{1}.jpg :null", item.ImageId, item2.ImageId);
        p.Start();
        // Do not wait for the child process to exit before
        // reading to the end of its redirected stream.
        // p.WaitForExit();
        // Read the output stream first and then wait.
       // string output = p.StandardOutput.ReadToEnd();
        string output = p.StandardError.ReadToEnd();
        p.WaitForExit();

        var nfi = new NumberFormatInfo();

        nfi.NumberDecimalSeparator = ".";
        test = Double.Parse(output, nfi);
      }
      catch (Exception error)
      {
        System.Console.Out.WriteLine(error.Message);
      }*/
      try
      {
        test = ImageTool.GetPercentageDifference("Images\\" + item.GetImageKey() + ".jpg", "Images\\" + item2.GetImageKey() + ".jpg", 50);
      }
      catch (Exception Error)
      {

      }

      test = 1.0 - test;

      return test;
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
            float price = 0;
            if (Item.sitbuynowprice > 0)
            {
              price = Item.sitbuynowprice;
            }
            else
            {
              price = Item.sitprice;
            }
            NewElements.Add(new ItemData(Item.sitid, Item.sitname, Item.sitthumburl, Item.sitsellerinfo.sellerid, Item.sitcategoryid, price));
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

      double[] weights = { 0.4, 0.9, 1.5, 1.0 };
      double threshold = 2;

      double[,] itemsRelations = new double[SourceList.Count, SourceList.Count];
      Dictionary<String, double> ItemsRelations = new Dictionary<String, double>();

      double itemValue = 0.0;

      int kl = 0, j = 0;
      /*foreach (ItemData Item1 in SourceList)
      {
        kl++;
        foreach (ItemData Item2 in SourceList)
        {
          j++;
          double[] itemValues = { ItemClassificator1(Item1, Item2), 
                                  ItemClassificator2(Item1, Item2), 
                                  ItemClassificator3(Item1, Item2), 
                                  ItemClassificator4(Item1, Item2) };
          for (int k = 0; k < itemValues.Length; k++)
          {
            itemValue += itemValues[k] * weights[k];
          }
          ItemsRelations[Item1.ImageId.ToString() + Item2.ImageId.ToString()] = itemValue;
          itemValue = 0;
        }
      }*/

      foreach (ItemData Item1 in SourceList)
      {
        if (ResultClusters.Count == 0)
        {
          ResultClusters.Add(new ItemClass());
          ResultClusters[0].Elements.Add(Item1);
        }
        else
        {
          int ClosestClusterIndex = 0;
          int i = 0;
          double ClosestClusterDistance = Double.MaxValue; // Lest start from a giant length so that everything will bo closer
          bool NewClass = true;
          foreach (ItemClass cluster in ResultClusters)
          {
            /*double MeanValue = 0.0;
            foreach (ItemData Item2 in cluster.Elements)
            {
              MeanValue += ItemsRelations[Item1.ImageId.ToString() + Item2.ImageId.ToString()];
            }
            if (ClosestClusterDistance > MeanValue)
            {
              ClosestClusterIndex = i;
              ClosestClusterDistance = MeanValue;
            }
            ;*/
            uint match = 0;
            foreach (ItemData Item2 in cluster.Elements)
            {
              if (isTheSameProduct(Item1, Item2))
              {
                match++;
              }
            }
            if (match >= 0.8 * cluster.Elements.Count)
            {
              cluster.Elements.Add(Item1);
              NewClass = false;
              break;
            }
          }
          if (NewClass) //ClosestClusterDistance == Double.MaxValue || ClosestClusterDistance > threshold)
          {
            // No suitable clusters found, create a new one
            ResultClusters.Add(new ItemClass());
            ResultClusters[ResultClusters.Count - 1].Elements.Add(Item1);
          }
          //else
          //{
            
          //}
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

      ClusteredItems = ClusteredItems.OrderByDescending(x => x.Elements.Count).ToList();
        //Elements.OrderBy(x => x.Price).ToList();
      ClusterImages.ImageSize = new Size(128, 96);

      foreach(ItemClass cluster in ClusteredItems)
      {
        ClusterImages.Images.Add(cluster.Elements[0].GetImageKey(), cluster.Elements[0].GetImage());
        ClusterListView.LargeImageList = ClusterImages;
        ClusterListView.Items.Add("Elementy: " + cluster.Elements.Count.ToString() + " " + cluster.Elements[0].AuctionName + " Cena: " + cluster.Elements[0].Price + "zł", cluster.Elements[0].GetImageKey());
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
