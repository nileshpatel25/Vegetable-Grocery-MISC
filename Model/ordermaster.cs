using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vegetable_Grocery_MISC.Model
{
  public class ordermaster
  {
    public string Id { get; set; }
  public string customerid { get; set; }

    public string invoiceno { get; set; }

    public DateTime orderdate { get; set; }
    public string ordertype { get; set; }
    public string status { get; set; }
    public double tax { get; set; }
    public double shippingCharge { get; set; }
    public double discount { get; set; }
    public string remark { get; set; }
    public bool deleted { get; set; }
    public DateTime createat { get; set; }
    public DateTime updatedat { get; set; }

  }
}
