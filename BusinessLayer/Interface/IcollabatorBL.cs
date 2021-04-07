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
        Task<string> AddCollaborator(CollbratorResponse collaborator,long UserId,long noteId);
        Task<string> DeleteCollaborator(long collbratorID);

        Task<List<Collaborator>> GetAllCollabarators();

    }
}
