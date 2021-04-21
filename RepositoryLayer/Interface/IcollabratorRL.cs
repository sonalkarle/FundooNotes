using CommanLayer.Collabrsatorresponse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IcollabratorRL
    {
        public CollbratorModel AddCollaboratorToNotes(CollbratorResponse collaborator,long UserId,long NoteId);
        Task<string> DeleteCollaborator(long collbratorID);
        Task<List<Collaborator>> GetAllCollabarators();
    }
}
