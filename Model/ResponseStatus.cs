using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vegetable_Grocery_MISC.Model
{
  public class ResponseStatus
  {
    public string Token { get; set; }
    public bool status { get; set; }
    public Object lstItems { get; set; }
    public Object objItem { get; set; }

    public string message { get; set; }
  }
}
