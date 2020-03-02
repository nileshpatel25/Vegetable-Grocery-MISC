using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vegetable_Grocery_MISC.Model
{
  public class orderdetails
  {
    public string id { get; set; }
 public string orderid { get; set; }
    public string productid { get; set; }
    public decimal quantity { get; set; }
    public decimal unitprice { get; set; }
    public decimal discountper { get; set; }
    public decimal discountprice { get; set; }
    public string taxslabid { get; set; }
    public decimal totaltax { get; set; }
    public decimal totalprice { get; set; }
    public bool deleted { get; set; }

  }
}
