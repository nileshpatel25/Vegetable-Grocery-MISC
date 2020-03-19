using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Vegetable_Grocery_MISC.DataModel;
using Vegetable_Grocery_MISC.Model;

namespace Vegetable_Grocery_MISC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
  {
    private readonly UserManager<Applicationuser> userManager;
    private readonly SignInManager<Applicationuser> signInManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public AppDbContex appDbContex { get; }

    public AccountController(UserManager<Applicationuser> userManager, SignInManager<Applicationuser> signInManager, RoleManager<IdentityRole> roleManager, AppDbContex _appDbContex)
    {
      this.userManager = userManager;
      this.signInManager = signInManager;
      this.roleManager = roleManager;
      this.appDbContex = _appDbContex;
    }
    //[AllowAnonymous]
    [HttpPost("register")]
    public async Task<ResponseStatus> Register(RegisterRequest registerRequest)
    {
      ResponseStatus status = new ResponseStatus();
     

      var user = new Applicationuser
      {
       UserName=registerRequest.phonenumber,
       PhoneNumber=registerRequest.phonenumber,
       Email=registerRequest.email,
       address=registerRequest.address,
       firstname=registerRequest.firstname,
       lastname=registerRequest.lastname,
       landmark=registerRequest.landmark,
       source=registerRequest.source,
       othercontactno=registerRequest.othercontactno

      };

      var result = await userManager.CreateAsync(user, registerRequest.password);
      if (result.Succeeded)
      {
        // SendEmail sendEmail = new SendEmail();
        //sendEmail.sendemail(registerRequest.Email);
        await signInManager.SignInAsync(user, isPersistent: false);
        status.status = true;
        return status;
      }
      List<string> errors = new List<string>();
      foreach (var error in result.Errors)
      {
        errors.Add(error.Description);
      }
      status.status = false;
      status.message = errors[0].ToString();
      return status;
      //  return BadRequest(new { message = errors });

    }

   
    [HttpPost("login")]
    public async Task<ResponseStatus> Login(LoginRequest loginRequest)
    {
      ResponseStatus status = new ResponseStatus();
      var result = await signInManager.PasswordSignInAsync(loginRequest.UserName, loginRequest.Password, loginRequest.RememberMe, false);

      if (result.Succeeded)
      {
        var user = await userManager.FindByNameAsync(loginRequest.UserName);
        var claims = new[]
        {
                    new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };
        var signinkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecurityKey"));
        var tocken = new JwtSecurityToken(
           issuer: "http://saveafrica.com",
           audience: "http://saveafrica.com",
           expires: DateTime.Now.AddHours(1),
           claims: claims,
           signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(signinkey, SecurityAlgorithms.HmacSha256)
        );
        var role = await userManager.GetRolesAsync(user);
        // return Ok(user);
        //return Ok(new
        //{
        //    authonticationTocken = new JwtSecurityTokenHandler().WriteToken(tocken),
        //    token = new JwtSecurityTokenHandler().WriteToken(tocken),
        //    expiration = tocken.ValidTo,
        //    role = role,
        //    userId = user.Id
        //});
        status.Token = new JwtSecurityTokenHandler().WriteToken(tocken);
        status.objItem = user;
        status.message = "Login Successfully";
        status.status = true;
        return status;
      }
      else
      {
        status.Token = string.Empty;
        status.message = "Invalid Username Or Passwrord!";
        status.status = false;
        return status;
        //  return BadRequest(new { Message = "Invalid Username Or Passwrord!" });
      }
    }


    [AllowAnonymous]
    [HttpPost("logout")]
    public async Task<ResponseStatus> Logout()
    {
      ResponseStatus status = new ResponseStatus();
      await signInManager.SignOutAsync();
      status.status = true;
      status.message = "Logout Successfull !";
      return status;
    }
  }
}
