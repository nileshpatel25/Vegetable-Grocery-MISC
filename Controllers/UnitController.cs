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
    public class UnitController : ControllerBase
  {
    public AppDbContex appDbContex { get; }
    public UnitController(AppDbContex _appdbContext)
    {
      this.appDbContex = _appdbContext;
    }

    [HttpPost("AddUnit")]
    public async Task<ResponseStatus> addUnit(UnitRequest unitRequest)
    {
      ResponseStatus status = new ResponseStatus();
      try
      {
        var unitname = appDbContex.Units.Where(a => a.name == unitRequest.name && a.deleted == false).FirstOrDefault();
        if (unitname == null)
        {
          var guId = Guid.NewGuid();
          Unit unit = new Unit
          {
            Id = guId.ToString(),
            name = unitRequest.name,
            code = unitRequest.code,
            deleted = false
          };
          appDbContex.Add(unit);
          await appDbContex.SaveChangesAsync();
          status.status = true;
          status.message = "Unit Save successfully!";
          // status.lstItems = GetAllCity(cityRequest.stateId).Result.lstItems;
          return status;
        }
        else
        {
          status.status = false;
          status.message = "Unit Already Added!";
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

    [HttpPost("updateUnit")]
    public async Task<ResponseStatus> updateunit(UnitRequest unitRequest)
    {
      ResponseStatus status = new ResponseStatus();
      var name = appDbContex.Units.Where(a => a.name == unitRequest.name && a.Id != unitRequest.Id).SingleOrDefault();
      if (name == null)
      {
        var unit = appDbContex.Units.Where(a => a.Id == unitRequest.Id).SingleOrDefault();
        if (unit != null)
        {
          unit.name = unitRequest.name;
        

          appDbContex.Update(unit);
          await appDbContex.SaveChangesAsync();

          status.status = true;
          status.message = "Unit Updated Successfully!";
          return status;

        }
      }
      status.status = false;
      status.message = "Unit Already Exists!";

      return status;

    }


    [HttpGet("AllUnitList")]
    public ActionResult getAllUnit()
    {
      try
      {
        List<Unit> units = appDbContex.Units.Where(a => a.deleted == false).ToList();
        return Ok(units);
      }

      catch (Exception ex)
      {

        throw ex;
      }
    }
  }
}
