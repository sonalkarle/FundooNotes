using CommanLayer.LableResponsemodel;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
     public interface ILableBL
    {
        LableResponseModel AddLabel(LabaleResponse labelModel,long UserId,long noteId);

        LableResponseModel UpdateLabel(long LableID, string name);
        string DeleteLabel(long LableID);
        Task<List<LabaleTable>> GetAllLabels();
        List<LabaleTable> GetLabel(long LableID);

    }
}
