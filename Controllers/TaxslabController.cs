using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vegetable_Grocery_MISC.Model;
using Vegetable_Grocery_MISC.DataModel;

namespace Vegetable_Grocery_MISC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxslabController : ControllerBase
    {
    public AppDbContex appDbContex { get; set; }
    public TaxslabController(AppDbContex _appdbContext)
    {
      this.appDbContex = _appdbContext;
    }

    [HttpPost("AddTaxSlabdetail")]
    public async Task<ResponseStatus> addTaxSlabDetails(TaxSlabRequest taxSlabRequest)
    {
      ResponseStatus status = new ResponseStatus();
      try
      {
        var SlabName = appDbContex.TaxSlabMasters.Where(a => a.stSlabName == taxSlabRequest.stSlabName && a.deleted == false).FirstOrDefault();
        if (SlabName == null)
        {
          var guId = Guid.NewGuid();
          TaxSlabMaster taxSlabMaster = new TaxSlabMaster
          {
            Id = guId.ToString(),
            stSlabName=taxSlabRequest.stSlabName,
            CGSTHome=taxSlabRequest.CGSTHome,
            SGSTHome=taxSlabRequest.SGSTHome,
            IGSTHome=taxSlabRequest.IGSTHome,
            CGST=taxSlabRequest.CGST,
            SGST=taxSlabRequest.SGST,
            IGST=taxSlabRequest.IGST,
            deleted = false
          };
          appDbContex.Add(taxSlabMaster);
          await appDbContex.SaveChangesAsync();
          status.status = true;
          status.message = "Save successfully!";
          // status.lstItems = GetAllCity(cityRequest.stateId).Result.lstItems;
          return status;
        }
        else
        {
          status.status = false;
          status.message = "Tax Slab Already Added!";
        }
      }
      catch (Exception ex)
      {
        status.status = false;
        status.message = ex.Message;
        throw ex;
      }
      return status;
    }
    [HttpPost("updateTaxSlab")]
    public async Task<ResponseStatus> updateTaxSlab(TaxSlabRequest taxSlabRequest)
    {
      ResponseStatus status = new ResponseStatus();
      var slabname = appDbContex.TaxSlabMasters.Where(a => a.stSlabName == taxSlabRequest.stSlabName && a.Id != taxSlabRequest.Id).SingleOrDefault();
      if (slabname == null)
      {
        var slab = appDbContex.TaxSlabMasters.Where(a => a.Id == taxSlabRequest.Id).SingleOrDefault();
        if (slab != null)
        {

          slab.stSlabName = taxSlabRequest.stSlabName;

          slab.CGSTHome = taxSlabRequest.CGSTHome;
          slab.SGSTHome = taxSlabRequest.SGSTHome;
          slab.IGSTHome = taxSlabRequest.IGSTHome;
          slab.CGST = taxSlabRequest.CGST;
          slab.SGST = taxSlabRequest.SGST;
          slab.IGST = taxSlabRequest.IGST;
          appDbContex.Update(slab);
          await appDbContex.SaveChangesAsync();

          status.status = true;
          status.message = "TaxSlab Updated Successfully!";
          return status;

        }
      }
      status.status = false;
      status.message = "TaxSlab Already Exists!";

      return status;

    }
    [HttpGet("AllTaxSlabList")]
    public async Task<ResponseStatus> getAllTaxSlab()
    {
      try
      {
        ResponseStatus status = new ResponseStatus();
        status.lstItems = appDbContex.TaxSlabMasters.ToList();
        status.status = true;
        return status;

      }

      catch (Exception ex)
      {

        throw ex;
      }
    }

  }
}
