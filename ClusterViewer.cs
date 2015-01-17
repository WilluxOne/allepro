using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllePro
{
  public partial class ClusterViewer : Form
  {
    public ClusterViewer(List<ItemData> ListToDisplay)
    {
      InitializeComponent();

      foreach (ItemData item in ListToDisplay)
      {
        ListImages.Images.Add(item.GetImageKey(), item.GetImage());
        ClusterItemsList.Items.Add(item.AuctionName + " Cena: " + item.Price + "zł", item.GetImageKey());
      }
    }
  }
}
