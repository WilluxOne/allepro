using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;

namespace AllePro
{
  public class ItemData
  {
    public String AuctionName { get; set; }
    public float Price { get; set; }

    String ImageLocalName { get; set; }
    int UserName { get; set; }
    int CathegoryId { get; set; }
    long ImageId { get; set; }

    public Image GetImage()
    {
      return Image.FromFile(ImageLocalName);
    }

    public string GetImageKey()
    {
      return ImageId.ToString();
    }

    public ItemData(long ItemId, String Name, String ImageUrl, int User, int CatId, float CurrentPrice) {
      AuctionName = Name;
      UserName = User;
      CathegoryId = CatId;
      ImageId = ItemId;

      String ImageFolder = "Images";

      if (!System.IO.Directory.Exists(ImageFolder))
      {
        System.IO.Directory.CreateDirectory(ImageFolder);
      }

      ImageLocalName = ImageFolder + "\\" + ItemId.ToString() + ".jpg";

      if (!System.IO.File.Exists(ImageLocalName))
      {
        WebClient Downloader = new WebClient();
        Downloader.DownloadFile(ImageUrl, ImageLocalName);
      }
    }
  }
}
