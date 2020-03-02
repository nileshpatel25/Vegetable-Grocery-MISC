using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vegetable_Grocery_MISC.Model
{
  public class Product
  {
    public string Id { get; set; }
public string name { get; set; }
    public string code { get; set; }
    public double unitid { get; set; }
    public double categoryid { get; set; }
    public double intaxslabid { get; set; }
    public double price { get; set; }
    public double discountper { get; set; }
    public double discountprice { get; set; }
    public string discription { get; set; }
    public string image { get; set; }
    public double orderno { get; set; }
    public double quantity { get; set; }
    public bool deleted { get; set; }
    public bool active { get; set; }

  }
}
