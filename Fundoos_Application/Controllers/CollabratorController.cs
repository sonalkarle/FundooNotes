using BusinessLayer.Interface;
using CommanLayer.Collabrsatorresponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class CollabratorController : ControllerBase
    {
         readonly IcollabatorBL collaboratorBL;

        public CollabratorController(IcollabatorBL collaboratorBL)
        {
            this.collaboratorBL = collaboratorBL;
        }

        [HttpPost]
   
        public async Task<IActionResult> AddColloborator(CollbratorResponse collaborator,long NoteId)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long UserID = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
                    string Email = claims.Where(p => p.Type == "Email").FirstOrDefault()?.Value;
                    var result = await this.collaboratorBL.AddCollaborator(collaborator, UserID, NoteId);
                    if (result != null)
                    {
                        return this.Ok(new { Message=result });
                    }
                }

                return BadRequest();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete]
        [Route("{NoteId}")]
        public async Task<IActionResult> DeleteCollaborator(long collbratorID)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long UserID = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
                    string Email = claims.Where(p => p.Type == "Email").FirstOrDefault()?.Value;
                    var result = await this.collaboratorBL.DeleteCollaborator(collbratorID);
                    return this.Ok(new { result });
                }
                return this.Ok(new { Message = "Bad request" });
            }
            catch (Exception exception)
            {
                return this.BadRequest(exception.Message);
            }
        }

        [HttpGet]
       
        public async Task<IActionResult> GetAllCollaborators()
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long UserID = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
                    string Email = claims.Where(p => p.Type == "Email").FirstOrDefault()?.Value;
                    var result = await this.collaboratorBL.GetAllCollabarators();
                    return this.Ok(result);
                }
                return this.Ok(new { Message = "Bad request" });
            }
            catch (Exception exception)
            {
                return this.BadRequest(exception.Message);
            }
        }
    }
}
