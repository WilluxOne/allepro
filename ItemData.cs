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
    public int UserName { get; set; }
    public int CathegoryId { get; set; }
    public long ImageId { get; set; }
    public List<String> ParsedName { get; set; }

    public Image GetImage()
    {
      return Image.FromFile(ImageLocalName);
    }

    public string GetImageKey()
    {
      return ImageId.ToString();
    }

    public ItemData(long ItemId, String Name, String ImageUrl, int User, int CatId, float CurrentPrice) {
      ParsedName = null;
      AuctionName = Name;
      UserName = User;
      CathegoryId = CatId;
      ImageId = ItemId;
      Price = CurrentPrice;
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
