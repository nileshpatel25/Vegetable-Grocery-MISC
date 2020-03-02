using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vegetable_Grocery_MISC.Model
{
  public class OTPAuthentication
  {
    public string id { get; set; }
    public string customerid { get; set; }
    public string customerphone { get; set; }
    public string otpcode { get; set; }
    public bool deleted { get; set; }

  }
}
