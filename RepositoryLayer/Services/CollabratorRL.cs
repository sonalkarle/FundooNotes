using CommanLayer.Collabrsatorresponse;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class CollabratorRL : IcollabratorRL
    {
        /// <summary>
        /// UserContext private readonly variable
        /// </summary>
        private readonly FundooApiContext collbratorcontext;
      

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorRepository"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        public CollabratorRL(FundooApiContext collbratorcontext)
        {
            this.collbratorcontext = collbratorcontext;
            
        }

        /// <summary>
        /// Adds the collaborators to notes.
        /// </summary>
        /// <param name="collaborator">The collaborator.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> AddCollaboratorToNotes(CollbratorResponse collaborator,long UserId,long NoteId)
        {
            try
            {
                bool result = this.collbratorcontext.Notes.Any(option => option.UserId == UserId && option.NoteId == NoteId);
                if (result)
                {

                    Collaborator addCollaborator = new Collaborator()
                       {
                       
                        CollaboratorEmail = collaborator.CollaboratorEmail,
                       
                        
                       };
                    collbratorcontext.Collaborators.Add(addCollaborator);
                }
                await this.collbratorcontext.SaveChangesAsync();
                return "Added successFully";

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Deletes the collaborator.
        /// </summary>
        /// <param name="collaborator">The collaborator.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> DeleteCollaborator(long collbratorID)
        {
            var result = this.collbratorcontext.Collaborators.Where(op => op.CollaboratorId == collbratorID).SingleOrDefault();
            if (result != null)
            {
                this.collbratorcontext.Collaborators.Remove(result);
                await this.collbratorcontext.SaveChangesAsync();
                return "Deleted Successfully";
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Collaborator>> GetAllCollabarators()
        {
            await this.collbratorcontext.SaveChangesAsync();
            return this.collbratorcontext.Collaborators.ToList();
        }
    }
}
