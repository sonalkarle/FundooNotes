using CommanLayer.LableResponsemodel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public  class LableRL :   ILableRL
    {

        readonly FundooApiContext LablesDB;
        
        public LableRL(FundooApiContext lableDB)
        {
            LablesDB = lableDB;
            
        }


        public LableResponseModel AddLabel(LabaleResponse labelModel,long UserId,long noteID)
        {
            try
            {

                LabaleTable label = new LabaleTable()
                {
                    NoteId = noteID,
                    LableName = labelModel.LableName,
                    UserId =UserId
                };
                this.LablesDB.LabaleTables.Add(label);
                var result = this.LablesDB.SaveChanges();
                var lable = LablesDB.LabaleTables.FirstOrDefault(N => N.NoteId == noteID && N.UserId == UserId);
                LableResponseModel lable1 = new LableResponseModel()
                {
                    NoteId = noteID,
                    LableId = lable.LableId,
                    LableName = labelModel.LableName,
                    UserId = UserId
                };

                return lable1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DeleteLabel(long LableId)
        {
            var label = LablesDB.LabaleTables.Where(r => r.LableId == LableId).SingleOrDefault();
            if (label != null)
            {
                LablesDB.LabaleTables.Remove(label);
                LablesDB.SaveChanges();
                return "Deleted Successfully";
            }
            else
            {
                return null;
            }
        }
        public async Task<List<LabaleTable>> GetAllLabels()
        {
          
            await this.LablesDB.SaveChangesAsync();
           return this.LablesDB.LabaleTables.ToList<LabaleTable>();
        }

        public List<LabaleTable> GetLabel(long LableId)
        {
            var note = LablesDB.LabaleTables.Where(r => r.LableId == LableId).SingleOrDefault();
            if (note != null)
            {
                return LablesDB.LabaleTables.Where(r => r.LableId == LableId).ToList();
            }
            return null;
        }

        public LableResponseModel UpdateLabel(long LableId, string name)
        {
            var result = this.LablesDB.LabaleTables.Where(op => op.LableId == LableId).SingleOrDefault();
            if (result != null)
            {
                result.LableName = name;
                var res = this.LablesDB.SaveChanges();
                LableResponseModel lable1 = new LableResponseModel()
                {
                    NoteId =(long) result.NoteId,
                    LableId = LableId,
                    LableName = result.LableName,
                    UserId = (long)result.UserId
                };
                return lable1;
            }
            return default;
        }

    }
}
