using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vegetable_Grocery_MISC.Model
{
  public class Applicationuser: IdentityUser
  {
    public string firstname { get; set; }
    public string lastname { get; set; }
    public string landmark { get; set; }
    public string address { get; set; }
    public string othercontactno { get; set; }

    public string source { get; set; }
  }
}
