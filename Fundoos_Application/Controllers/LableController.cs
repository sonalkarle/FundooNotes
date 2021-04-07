using BusinessLayer.Interface;
using CommanLayer.LableResponsemodel;
using CommanLayer.NoteResponseModel;
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
    public class LableController : ControllerBase
    {
        
            readonly ILableBL labelManagementBL;

            public LableController(ILableBL labelManagementBL)
            {
                this.labelManagementBL = labelManagementBL;
            }
        [HttpPost]
        [Route("{noteId}")]
        public ActionResult AddLabel(LabaleResponse labelModel,long noteId)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long UserID = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
                    string Email = claims.Where(p => p.Type == "Email").FirstOrDefault()?.Value;
                    LabaleResponse result = this.labelManagementBL.AddLabel(labelModel,UserID, noteId);
                    return Ok(new { success = true,Data = result });
                }
                return this.Ok(new { Message = "Bad request" });
            }
            catch (Exception exception)
            {
                return this.BadRequest(exception.Message);
            }
        }

        /// <summary>
        /// Deletes the specific label.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{LableId}")]
        public IActionResult Delete(long LableId)
        {
            try
            {
                var result = this.labelManagementBL.DeleteLabel(LableId);
                return this.Ok(new { result });
            }
            catch (Exception exception)
            {
                return this.BadRequest(exception.Message);
            }
        }

        /// <summary>
        /// Updates the specified label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{LableId}")]
        public IActionResult Update(long LableId, string name)
        {
            try
            {
                var result = this.labelManagementBL.UpdateLabel(LableId, name);
                return this.Ok(new { result });
            }
            catch (Exception exception)
            {
                return this.BadRequest(exception.Message);
            }
        }

        /// <summary>
        /// Gets All Labels.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getAllLabels")]
        public async Task<List<LabaleTable>> GetAllLables()
        {
            return await this.labelManagementBL.GetAllLabels();
        }

        /// <summary>
        /// Gets Label by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{LableId}")]
        public List<LabaleTable> GetLabel(long LableId)
        {
            return this.labelManagementBL.GetLabel(LableId);
        }
    }
}
