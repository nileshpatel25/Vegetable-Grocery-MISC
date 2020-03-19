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
    public class ProductController : ControllerBase
    {
    public AppDbContex appDbContex { get; }
    public ProductController(AppDbContex _appdbContext)
    {
      this.appDbContex = _appdbContext;
    }
    [HttpPost("AddProduct")]
    public async Task<ResponseStatus> addProduct(ProductRequest productRequest, IFormFile Image)
    {
      ResponseStatus status = new ResponseStatus();
      try
      {
        var productname = appDbContex.Products.Where(a => a.name == productRequest.name && a.deleted == false).FirstOrDefault();
        if (productname == null)
        {
          var guId = Guid.NewGuid();
          Product product = new Product
          {
            Id = guId.ToString(),
            categoryid=productRequest.categoryid,
            subcategoryid = productRequest.subcategoryid,
            subsubcategoryid = productRequest.subsubcategoryid,
            name = productRequest.name,
            code = productRequest.code,
            price=productRequest.price,
            intaxslabid=productRequest.intaxslabid,
            unitid=productRequest.unitid,
            discountper=productRequest.discountper,
            discription=productRequest.discription,
            orderno=productRequest.orderno,
            quantity=productRequest.quantity,
            // image=categoryRequest.image,
            deleted = false
          };
          appDbContex.Add(product);
          await appDbContex.SaveChangesAsync();
          status.status = true;
          status.message = "Product Save successfully!";
          // status.lstItems = GetAllCity(cityRequest.stateId).Result.lstItems;
          return status;
        }
        else
        {
          status.status = false;
          status.message = "Product Already Added!";
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
    [HttpPost("updateProduct")]
    public async Task<ResponseStatus> updateProduct(ProductRequest productRequest)
    {
      ResponseStatus status = new ResponseStatus();
      var productname = appDbContex.Products.Where(a => a.name == productRequest.name && a.Id != productRequest.Id).SingleOrDefault();
      if (productname == null)
      {
        var product = appDbContex.Products.Where(a => a.Id == productRequest.Id).SingleOrDefault();
        if (product != null)
        {
          product.name = productRequest.name;
          product.subcategoryid = productRequest.subcategoryid;
          product.subsubcategoryid = productRequest.subsubcategoryid;
          // category.image = categoryRequest.image;
          product.categoryid = productRequest.categoryid;

          product.name = productRequest.name;
          product.code = productRequest.code;
          product.price = productRequest.price;
          product.intaxslabid = productRequest.intaxslabid;
          product.unitid = productRequest.unitid;
          product.discountper = productRequest.discountper;
          product.discription = productRequest.discription;
          product.orderno = productRequest.orderno;
          product.quantity = productRequest.quantity;
          appDbContex.Update(product);
          await appDbContex.SaveChangesAsync();

          status.status = true;
          status.message = "Product Updated Successfully!";
          return status;

        }
      }
      status.status = false;
      status.message = "Product Already Exists!";

      return status;

    }

    [Route("upload")]
    [HttpPost, DisableRequestSizeLimit]
    public async Task<string> UploadFile()
    {
      try
      {

        var file = Request.Form.Files[0];
        string cid = Convert.ToString(Request.Form["PId"]);
        string folderName = Path.Combine("Content", "Images\\Product\\" + cid);
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
          Product product = appDbContex.Products.Where(a => a.Id == cid).SingleOrDefault();
          if (product != null)
          {

            product.image = dbPath;
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



    [HttpPost("deleteProduct")]
    public async Task<ResponseStatus> deleteProduct(ProductRequest productRequest)
    {
      ResponseStatus status = new ResponseStatus();
      var productname = appDbContex.Products.Where(a => a.name == productRequest.name && a.Id != productRequest.Id).SingleOrDefault();
      if (productname == null)
      {
        var product = appDbContex.Products.Where(a => a.Id == productRequest.Id).SingleOrDefault();
        if (product != null)
        {
          product.deleted = true;
          appDbContex.Update(product);
          await appDbContex.SaveChangesAsync();

          status.status = true;
          status.message = "Product deleted Successfully!";
          return status;

        }
      }
      status.status = false;
      status.message = "Product Already Exists!";

      return status;

    }


    [HttpGet("AllProduct")]
    public async Task<ResponseStatus> getAllProduct()
    {
      try
      {
        ResponseStatus status = new ResponseStatus();
        status.lstItems = appDbContex.Products.ToList();
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
