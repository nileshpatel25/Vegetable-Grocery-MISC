using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vegetable_Grocery_MISC.Model
{
  public class Product
  {
    public string id { get; set; }
public string name { get; set; }
    public string code { get; set; }
    public decimal unitid { get; set; }
    public decimal categoryid { get; set; }
    public decimal intaxslabid { get; set; }
    public decimal price { get; set; }
    public decimal discountper { get; set; }
    public decimal discountprice { get; set; }
    public string discription { get; set; }
    public string image { get; set; }
    public decimal orderno { get; set; }
    public decimal quantity { get; set; }
    public bool deleted { get; set; }
    public bool active { get; set; }

  }
}
