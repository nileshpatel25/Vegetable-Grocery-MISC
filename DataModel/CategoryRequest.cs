using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vegetable_Grocery_MISC.DataModel
{
  public class CategoryRequest
  {
    public string Id { get; set; }
    public string name { get; set; }
    public string code { get; set; }
    public string subcategoryid { get; set; }
    public string subsubcategoryid { get; set; }
    public IFormFile Image { get; set; }
    public bool deleted { get; set; }
  }

//  {
//	subcategoryid:"",
//	subsubcategoryid:"",
//	name:"Category2",
//	code:"Cat2",
//	image:""

//}
}
