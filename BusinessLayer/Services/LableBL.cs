using BusinessLayer.Interface;
using BusinessLayer.RedisCacheService;
using CommanLayer.LableResponsemodel;
using Microsoft.Extensions.Caching.Distributed;
using RepositoryLayer;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class LableBL : ILableBL
    {
        private readonly IDistributedCache distributedCache;
        public readonly ILableRL label;
        RedisCacheServiceBL redis;
        
        public LableBL(ILableRL labelManagementRL, IDistributedCache distributedCache)
        {
            this.label = labelManagementRL;

            this.distributedCache = distributedCache;
            redis = new RedisCacheServiceBL(this.distributedCache);
        }

       
        public LabaleResponse AddLabel(LabaleResponse labelModel,long UserId,long noteId)
        {
            try
            {
                LabaleResponse result = this.label.AddLabel(labelModel,UserId,noteId);
                return result;
            }
            catch (Exception exception)
            {
                throw exception;
            }

        }
        public string DeleteLabel(long LableID)
        {
            try
            {
                this.label.DeleteLabel(LableID);
                return "Deleted Successfully";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }
        public async Task<List<LabaleTable>> GetAllLabels()
        {
            try
            {

                return await this.label.GetAllLabels();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public List<LabaleTable> GetLabel(long LableID)
        {
            try
            {
                var list = new List<LabaleTable>();
                var result = this.label.GetLabel(LableID);
                foreach (var item in result)
                {
                    list.Add(item);
                }
                return list;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public string UpdateLabel(long LableID, string name)
        {
            try
            {
                this.label.UpdateLabel(LableID, name);
                return "Updated Successfully";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }
    }
}
