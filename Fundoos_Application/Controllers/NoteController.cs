using BusinessLayer.Interface;
using CommanLayer.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Fundoos_Application.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        INotesBL notesBL;
        private IConfiguration Configuration { get; }
        private readonly IDistributedCache distributedCache;

        public NotesController(INotesBL notesBL, IConfiguration configuration, IDistributedCache distributedCache)
        {
            //Constructor n passing an object to controller
            //to get an access of IEmployeeBL
            this.notesBL = notesBL;
            Configuration = configuration;
            this.distributedCache = distributedCache;
        }
        /// <summary>
        /// Get infromation of all notes
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public IActionResult GetActiveNotes()
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long UserID = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
                    string Email = claims.Where(p => p.Type == "Email").FirstOrDefault()?.Value;

                    var result = notesBL.GetActiveNotes(UserID);
                    return Ok(new { success = true, user = Email, Notes = result });
                }
                return BadRequest(new { success = false, Message = "no user is active please login" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.InnerException });
            }
        }
        /// <summary>
        /// Add new node to the database
        /// </summary>
        /// <param name="Note"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddUserNote(ResponseNoteModel responseNoteModel)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long UserID = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
                    responseNoteModel.UserID = UserID;
                    Note result = notesBL.AddUserNote(responseNoteModel);
                    return Ok(new { success = true, Note = result });
                }
                return BadRequest(new { success = false, Message = "no user is active please login" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }



        [HttpPut("Pin")]

        public async Task<IActionResult> Pin()
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long UserID = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

                    var result = await this.notesBL.Pin(UserID);
                    if (result != null)
                    {
                        return this.Ok(new { success = true, Message = "Note unpin successfully", Data = result });
                    }

                    return BadRequest(new { success = false, Message = "no user is active please login" });
                }
                return BadRequest(new { success = false, Message = "Unable to unpin notes" });
            }
            catch (Exception)
            {
                return BadRequest(new { success = false, Message = "Unable to unPin notes" });
            }
        }

        [HttpPut]
        [Route("Archive")]
        public async Task<IActionResult> IsArchive()
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long UserID = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
                    var result = await this.notesBL.Archive(UserID);
                    if (result != null)
                    {
                        return this.Ok(new { success = true, Message = "Note Archive change successfully", Data = result });
                    }

                    return BadRequest(new { success = false, Message = "no user is active please login" });
                }
                return BadRequest(new { success = false, Message = "Unable to Archive notes" });

            }
            catch (Exception exception)
            {
                return this.BadRequest(exception.Message);
            }
        }

        [HttpPut]
        [Route("ChangeColor")]
        public async Task<IActionResult> ChangeColor(string changeColor)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long UserID = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
                    var result = await this.notesBL.ChangeColor(UserID, changeColor);
                    if (result != null)
                    {
                        return this.Ok(new { success = true, Message = "Note color change successfully", Data = result });
                    }

                    return BadRequest(new { success = false, Message = "no user is active please login" });
                }
                return BadRequest(new { success = false, Message = "Unable to change color notes" });


            }

            catch (Exception exception)
            {
                return this.BadRequest(exception.Message);
            }
        }
       

        [HttpPost]
        [Route("uploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long UserID = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
                    var result = await this.notesBL.UploadImage(image, UserID);
                    return this.Ok(new { success = true, Message = "Image upload successfully", Data = result });
                }
                return BadRequest(new { success = false, Message = "no user is active please login" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }


        [HttpPatch("Reminder")]
        public IActionResult SetNoteReminder( DateTime Reminder)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long UserID = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
                    ResponseNoteModel result = notesBL.SetNoteReminder(UserID, Reminder);
                    return Ok(new { success = true, Message = "Note reminder added", Data = result });
                }
                return BadRequest(new { success = false, Message = "no user is active please login" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [HttpDelete]
        public IActionResult DeleteNote()
        {
            try
            {
                if (User.Identity is ClaimsIdentity identity)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long UserID = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
                    string Email = claims.Where(p => p.Type == "Email").FirstOrDefault()?.Value;

                    ResponseNoteModel result = notesBL.DeleteNote(UserID).Result;
                    if (result == null)
                    {
                        return Ok(new { success = true, user = Email, Message = "Note deleted sucessfully", Note = result });
                    }
                    else
                    {
                        return Ok(new { success = true, user = Email, Message = "Note is added to trash", Note = result });
                    }
                }
                return BadRequest(new { success = false, Message = "no user is active please login" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
      
        [HttpPut]
        [Route("restore")]
        public async Task<IActionResult> Restore()
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long UserID = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
                    var result = await this.notesBL.Restore(UserID);
                    return this.Ok(new { success = true, Message = "Note restore successfully", Data = result });
                }
                return BadRequest(new { success = false, Message = "no user is active please login" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [HttpDelete]
        [Route("emptyTrash")]
        public async Task<IActionResult> EmptyTrash()
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long UserID = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
                    var result = await this.notesBL.EmptyTrash();
                    return this.Ok(new { success = true, Message = "Trash empty successfully", Data = result });
                }
                return BadRequest(new { success = false, Message = "no user is active please login" });

            }
            catch (Exception)
            {
                return BadRequest(new { success = false, Message = "Unable to empty trash" });
            }
        }  

    }

}
