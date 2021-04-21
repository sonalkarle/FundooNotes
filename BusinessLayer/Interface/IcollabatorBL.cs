using CommanLayer.Collabrsatorresponse;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
   public  interface IcollabatorBL
    {
        public CollbratorModel AddCollaboratorToNotes(CollbratorResponse collaborator, long UserId, long NoteId);
        Task<string> DeleteCollaborator(long collbratorID);

        Task<List<Collaborator>> GetAllCollabarators();

    }
}
