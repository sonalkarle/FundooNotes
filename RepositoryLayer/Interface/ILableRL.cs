using CommanLayer.LableResponsemodel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
   public interface ILableRL
    {
        LabaleResponse AddLabel(LabaleResponse labelModel,long UserId,long noteId);
        string UpdateLabel(long LableId, string name);
        string DeleteLabel(long LableId);
        Task<List<LabaleTable>> GetAllLabels();
        List<LabaleTable> GetLabel(long id);

    }
}

