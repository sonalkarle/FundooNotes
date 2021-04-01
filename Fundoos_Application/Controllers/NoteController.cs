using BusinessLayer.Interface;
using CommanLayer.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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

        public NotesController(INotesBL notesBL, IConfiguration configuration)                           //Constructor n passing an object to controller
        {                                                                           //to get an access of IEmployeeBL
            this.notesBL = notesBL;
            Configuration = configuration;
        }



        [HttpGet("GetNote")]
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

        [HttpPost("AddNote")]
        public IActionResult AddUserNote(ResponseNoteModel Note)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long UserID = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
                    Note.UserID = UserID;
                    ResponseNoteModel result = notesBL.AddUserNote(Note);
                    return Ok(new { success = true, Note = result });
                }
                return BadRequest(new { success = false, Message = "no user is active please login" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [HttpDelete("Delete/{NoteID}")]
        public IActionResult DeleteNote(long NoteID)
        {
            try
            {
                if (User.Identity is ClaimsIdentity identity)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long UserID = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
                    string Email = claims.Where(p => p.Type == "Email").FirstOrDefault()?.Value;

                    bool result = notesBL.DeleteNote(UserID, NoteID);
                    return Ok(new { success = true, user = Email, Message = "note deleted" });
                }
                return BadRequest(new { success = false, Message = "no user is active please login" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

       

        [HttpPatch("Image/{id}")]
        public IActionResult UpdateImage(int id, ImageModel image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = this.notesBL.Image(id, image);
                    return Ok(new { success = true, Message = "image is updated sucessfully" });
                }
                return BadRequest(new { success = false, Message = "image is not updated sucessfully" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

       
       
        [HttpPatch("Pin/{NoteID}")]
        public IActionResult Pin(long NoteID)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long UserID = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
                    bool result = notesBL.Pin(NoteID, UserID);
                    return Ok(new { success = true, Message = "note pin toggled", Note = result });
                }
                return BadRequest(new { success = false, Message = "no user is active please login" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
        [HttpPatch("Color/{NoteID}/{ColorCode}")]
        public IActionResult ChangeNoteBackgroundColor(long NoteID, string ColorCode)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long UserID = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
                    bool result = notesBL.ChangeColor(NoteID, UserID, ColorCode);
                    return Ok(new { success = true, Message = "note background color changed", Note = result });
                }
                return BadRequest(new { success = false, Message = "no user is active please login" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
       
        [HttpPatch("SetReminder")]
        public IActionResult SetNoteReminder(NoteReminder Reminder)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long UserID = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
                    Reminder.UserID = UserID;
                    bool result = notesBL.SetNoteReminder(Reminder);
                    return Ok(new { success = true, Message = "note reminder added" });
                }
                return BadRequest(new { success = false, Message = "no user is active please login" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }


        [HttpPatch("Archive/{NoteID}")]
        public IActionResult Archive(long NoteID)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long UserID = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
                    bool result = notesBL.Archive(NoteID, UserID);
                    return Ok(new { success = true, Message = "note is Archieved", Note = result });
                }
                return BadRequest(new { success = false, Message = "no user is active please login" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
    }

}
