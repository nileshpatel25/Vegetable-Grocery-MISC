using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vegetable_Grocery_MISC.Model
{
  public class CustomerMaster
  {
    public string id { get; set; }
    public string name { get; set; }
    public string contactno { get; set; }
    public string emailid { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public string address { get; set; }
    public bool deleted { get; set; }
    public bool active { get; set; }
    public DateTime createat { get; set; }
    public DateTime updatedat { get; set; }

  }
}
