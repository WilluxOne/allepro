using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllePro
{
  class ItemClass
  {
    public List<ItemData> Elements { get; set; }
    public double MeanValue { get; set; }

    public ItemClass(List<ItemData> InputElements)
    {
      Elements = InputElements;
    }

    public ItemClass()
    {
      Elements = new List<ItemData>();
    }

    public void SortByPrice()
    {
      if (Elements != null)
      {
        Elements = Elements.OrderBy(x => x.Price).ToList();
      }
    }
  }
}
