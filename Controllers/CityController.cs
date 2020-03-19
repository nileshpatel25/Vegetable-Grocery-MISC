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
  public class CityController : ControllerBase
  {
    public AppDbContex appDbContex { get; }
    public CityController(AppDbContex _appdbContext)
    {
      this.appDbContex = _appdbContext;
    }

    [HttpPost("AddCity")]
    public async Task<ResponseStatus> addCity(CityRequest cityRequest)
    {
      ResponseStatus status = new ResponseStatus();
      try
      {
        var cityname = appDbContex.Cities.Where(a => a.name == cityRequest.name && a.deleted == false).FirstOrDefault();
        if (cityname == null)
        {
          var guId = Guid.NewGuid();
          City city = new City
          {
            Id = guId.ToString(),
            name = cityRequest.name,
            code = cityRequest.code,
            deleted = false
          };
          appDbContex.Add(city);
          await appDbContex.SaveChangesAsync();
          status.status = true;
          status.message = "Save successfully!";
          // status.lstItems = GetAllCity(cityRequest.stateId).Result.lstItems;
          return status;
        }
        else
        {
          status.status = false;
          status.message = "City Already Added!";
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

    [HttpGet("AllCity")]
    public async Task<ResponseStatus>  getAllCity()
    {
      try
      {
        ResponseStatus status = new ResponseStatus();
        status.lstItems = appDbContex.Cities.ToList();
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
