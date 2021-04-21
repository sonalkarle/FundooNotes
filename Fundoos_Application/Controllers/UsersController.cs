using BusinessLayer.Interface;
using BusinessLayer.Services;
using CommanLayer.ResponseModel;
using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepositoryLayer;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Fundoos_Application.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
  //  [EnableCors("AllowOrigin")]
    public class UsersController : ControllerBase
    {
        IUserBL userBL;
        private IConfiguration Configuration { get; }


        //Constructor n passing an object to controller
        public UsersController(IUserBL userBL, IConfiguration configuration)
        {
            //to get an access of IUserBL
            this.userBL = userBL;
            Configuration = configuration;

        }


        /// <summary>
        /// Register the new ID
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        [HttpPost("Register")]
        //Here return type represents the result of an action method
        public IActionResult Registration(ResponseUserAccount user)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    bool result = this.userBL.Registration(user);
                    if (result != false)
                    {
                        //this.Ok returns the data in json format
                        return this.Ok(new { Success = true, Message = "Register Record Successfully", Users = result });
                    }
                    else
                    {
                        return this.BadRequest(new { Success = false, Message = "Register Record Unsuccessfully" });
                    }
                }

                else
                {
                    throw new Exception("Model is not valid");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, ex.Message });
            }
        }


        /// <summary>
        /// Get login to register ID
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        //Here return type represents the result of an action method
        public IActionResult Login(LoginModule user)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    UserAccount result = this.userBL.Login(user);
                    var tokenString = this.userBL.GenerateToken(result);
                    if (result != null)
                    {
                        //this.Ok returns the data in json format
                        return this.Ok(new { Success = true, Message = "Login Successfully", token = tokenString, Users = result });
                    }
                    else
                    {
                        return this.BadRequest(new { Success = false, Message = "Login Unsuccessfully" });
                    }
                }
                else
                {
                    throw new Exception("Model is not valid");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, ex.Message });
            }
        }


        /// <summary>
        /// Reset password of authorized ID
        /// </summary>
        /// <param name="resetPasswordModel"></param>
        /// <returns></returns>
       [Authorize]
        [HttpPost("Resetpassword")]
        public IActionResult ResetPassword(ResetPasswordModel reset)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    var UserId = claims.Where(x => x.Type == "UserId").FirstOrDefault()?.Value;
                    var Email = claims.Where(p => p.Type == "Email").FirstOrDefault()?.Value;

                    bool result = this.userBL.ResetAccountPassword(reset, Email);
                    if (result)
                    {
                        return Ok(new { success = true, Message = "password is reset successfully" });
                    }
                }
                return BadRequest(new { success = false, Message = "password is change unsuccessfully" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        /// <summary>
        /// Forget password of Registered ID
        /// </summary>
        /// <param name="forgetPasswordModel"></param>
        /// <returns></returns>
        [HttpPost("ForgetPassword")]
        public ActionResult ForgetPassword(ForgetPasswordModel forgetPasswordModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    forgetclass result = userBL.ForgetPassword(forgetPasswordModel);                   //getting the data from BusinessLayer
                    var msmq = new MSMQ(Configuration);
                    msmq.MSMQSender(result);
                    if (result != null)
                    {
                        return this.Ok(new { Success = true, Message = "Your password has been forget sucessfully now you can reset your password" });   //(smd format)    //this.Ok returns the data in json format
                    }

                    else
                    {
                        return this.Ok(new { Success = true, Message = "Other User is trying to login from your account" });   //(smd format)    //this.Ok returns the data in json format
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, ex.Message });
            }
        }
    }
}
