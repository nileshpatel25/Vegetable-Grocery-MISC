using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vegetable_Grocery_MISC.Model
{
  public class TaxSlabMaster
  {
    public string id { get; set; }
    public string stSlabName { get; set; }
    public decimal CGSTHome { get; set; }
    public decimal SGSTHome { get; set; }
    public decimal IGSTHome { get; set; }
    public decimal CGST { get; set; }
    public decimal SGST { get; set; }
    public decimal IGST { get; set; }
    public bool deleted { get; set; }

  }
}
