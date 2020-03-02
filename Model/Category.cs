using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vegetable_Grocery_MISC.Model
{
  public class Category
  {
    public string id { get; set; }
    public string name { get; set; }
    public string code { get; set; }
    public string subcategoryid { get; set; }
    public string subsubcategoryid { get; set; }
    public bool deleted { get; set; }

  }
}
