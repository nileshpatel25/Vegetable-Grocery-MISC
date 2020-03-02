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

      }
      catch (Exception ex)
      {

        throw ex;
      }
      return status;
    }
  }
}
