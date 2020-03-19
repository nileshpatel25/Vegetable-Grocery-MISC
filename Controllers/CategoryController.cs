using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vegetable_Grocery_MISC.Model;
using Vegetable_Grocery_MISC.DataModel;
using System.IO;
using System.Net.Http.Headers;

namespace Vegetable_Grocery_MISC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

    public AppDbContex appDbContex { get; }
    public CategoryController(AppDbContex _appdbContext)
    {
      this.appDbContex = _appdbContext;
    }
    [HttpPost("AddCategory")]
    public async Task<ResponseStatus> addCategory(CategoryRequest categoryRequest)
    {
      ResponseStatus status = new ResponseStatus();
      try
      {
        var categoryname = appDbContex.Categories.Where(a => a.name == categoryRequest.name && a.deleted == false).FirstOrDefault();
        if (categoryname == null)
        {
          var guId = Guid.NewGuid();
          Category category = new Category
          {
            Id = guId.ToString(),
            subcategoryid=categoryRequest.subcategoryid,
            subsubcategoryid=categoryRequest.subsubcategoryid,          
            name = categoryRequest.name,
            code = categoryRequest.code,
           // image=categoryRequest.image,
            deleted = false
          };
          appDbContex.Add(category);
          await appDbContex.SaveChangesAsync();
          status.status = true;
          status.message = "Category Save successfully!";
          // status.lstItems = GetAllCity(cityRequest.stateId).Result.lstItems;
          return status;
        }
        else
        {
          status.status = false;
          status.message = "Category Already Added!";
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

    [HttpPost("updateCategory")]
    public async Task<ResponseStatus> updateCategory(CategoryRequest categoryRequest)
    {
      ResponseStatus status = new ResponseStatus();
      var categoryname = appDbContex.Categories.Where(a => a.name == categoryRequest.name && a.Id != categoryRequest.Id).SingleOrDefault();
      if (categoryname == null)
      {
        var category = appDbContex.Categories.Where(a => a.Id == categoryRequest.Id).SingleOrDefault();
        if (category != null)
        {
          category.name = categoryRequest.name;
          category.subcategoryid = categoryRequest.subcategoryid;
          category.subsubcategoryid = categoryRequest.subsubcategoryid;
         // category.image = categoryRequest.image;

          appDbContex.Update(category);
          await appDbContex.SaveChangesAsync();

          status.status = true;
          status.message = "Category Updated Successfully!";
          return status;

        }
      }
      status.status = false;
      status.message = "Category Already Exists!";

      return status;

    }


    [HttpGet("AllCategory")]
    public async Task<ResponseStatus> getAllCategory()
    {
      try
      {

        ResponseStatus status = new ResponseStatus();
        status.lstItems = appDbContex.Categories.ToList();
        status.status = true;
        return status;
      
      }

      catch (Exception ex)
      {

        throw ex;
      }
    }

    [Route("upload")]
    [HttpPost, DisableRequestSizeLimit]
    public async Task<string> UploadFile()
    {
      try
      {

        var file = Request.Form.Files[0];
        string cid = Convert.ToString(Request.Form["CId"]);
        string folderName = Path.Combine("Content", "Images\\Category\\" + cid);
        string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        if (file.Length > 0)
        {
          string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
          string ext = Path.GetExtension(fileName);
          string fullPath = Path.Combine(pathToSave, String.Concat(cid, ext));
          string dbPath = Path.Combine(folderName, String.Concat(cid, ext));

          FileInfo fl = new FileInfo(fullPath);
          if (fl.Exists)
          {
            fl.Delete();
          }

          if (!Directory.Exists(pathToSave))
          {
            Directory.CreateDirectory(pathToSave);
          }

          using (var stream = new FileStream(fullPath, FileMode.Create))
          {
            file.CopyTo(stream);
            await file.CopyToAsync(stream);
          }
          Category category = appDbContex.Categories.Where(a => a.Id == cid).SingleOrDefault();
          if (category != null)
          {

            category.image = dbPath;
            await appDbContex.SaveChangesAsync();
          }


        }


        return "success";
      }
      catch (Exception ex)
      {
        return "error";
      }
    }

  }
}
