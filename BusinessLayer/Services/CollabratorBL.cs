using BusinessLayer.Interface;
using CommanLayer.Collabrsatorresponse;
using RepositoryLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Distributed;
using BusinessLayer.RedisCacheService;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CollabratorBL : IcollabatorBL
    {
        private readonly IDistributedCache distributedCache;
        private readonly IcollabratorRL icollabratorRL;
        RedisCacheServiceBL redis;
        public CollabratorBL (IcollabratorRL icollabratorRL, IDistributedCache distributedCache)
        {
            this.icollabratorRL = icollabratorRL;
            this.distributedCache = distributedCache;
            redis = new RedisCacheServiceBL(this.distributedCache);
        }

        public CollbratorModel AddCollaboratorToNotes(CollbratorResponse collaborator, long UserId, long NoteId)
        {

            try
            {

               var result =  this.icollabratorRL.AddCollaboratorToNotes(collaborator, UserId, NoteId);
                return result;
              
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Delete Collaborator method is used to delete collaborator using id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Delted Successfully
        /// </returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> DeleteCollaborator(long collbratorID)
        {
            try
            {
                await this.icollabratorRL.DeleteCollaborator(collbratorID);
                return "Deleted Successfully";
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<List<Collaborator>> GetAllCollabarators()
        {
            try
            {
                var list = await this.icollabratorRL.GetAllCollabarators();
                return list;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
