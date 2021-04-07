using BusinessLayer.Interface;
using CommanLayer.Collabrsatorresponse;
using RepositoryLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CollabratorBL : IcollabatorBL
    {
        private readonly IcollabratorRL icollabratorRL;
        public CollabratorBL (IcollabratorRL icollabratorRL)
        {
            this.icollabratorRL = icollabratorRL;
        }

        public async Task<string> AddCollaborator(CollbratorResponse collaborator,long UserId,long noteId)
        {

            try
            {
                return await this.icollabratorRL.AddCollaboratorToNotes(collaborator, UserId, noteId);
              
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
