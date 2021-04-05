using BusinessLayer.Interface;
using BusinessLayer.RedisCacheService;
using CommanLayer.ResponseModel;
using CommonLayer.NoteException;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using System.Linq;
using RepositoryLayer;

namespace BusinessLayer.Services
{
    public class NotesBL : INotesBL
    {

        private readonly IDistributedCache distributedCache;
        readonly INotesRL notesRL;
        RedisCacheServiceBL redis;
        public NotesBL(INotesRL notesRL, IDistributedCache distributedCache)
        {
            this.notesRL = notesRL;
            this.distributedCache = distributedCache;
            redis = new RedisCacheServiceBL(this.distributedCache);
        }



        public Note AddUserNote(ResponseNoteModel note)
        {
            try
            {
                return notesRL.AddUserNote(note);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

        public async Task<string> EmptyTrash()
        {
            try
            {
                await this.notesRL.EmptyTrash();
                return "Trash Removed";
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

       

        public async Task<ResponseNoteModel> Pin(long UserID)
        {
            try
            {
                
                var result = await this.notesRL.pin(UserID);
                return result;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
      
      
        public async Task<ResponseNoteModel> UploadImage(IFormFile image, long UserID)
        {
            try
            {
            
                var result = await this.notesRL.UploadImage(image, UserID);
                return result;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }


        public async Task<ResponseNoteModel> DeleteNote(long UserID)
        {
            try
            {
                await redis.RemoveNotesRedisCache(UserID);
                ResponseNoteModel result = notesRL.DeleteNote(UserID);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ICollection<ResponseNoteModel>> GetActiveNotes(long UserID)
        {
            var cacheKey = "ActiveNotes:" + UserID.ToString();
            string serializedNotes;
            ICollection<ResponseNoteModel> Notes;
            try
            {
                var redisNoteCollection = await distributedCache.GetAsync(cacheKey);
                if (redisNoteCollection != null)
                {
                    serializedNotes = Encoding.UTF8.GetString(redisNoteCollection);
                    Notes = JsonConvert.DeserializeObject<List<ResponseNoteModel>>(serializedNotes);
                }
                else
                {
                    Notes = notesRL.GetNotes(UserID);
                    await redis.AddRedisCache(cacheKey, Notes);
                }
                return Notes;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseNoteModel> Restore(long UserID)
        {
            try
            {
                var result =  await this.notesRL.Restore(UserID);
                return result;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
       
  


        public ResponseNoteModel SetNoteReminder( long UserID, DateTime reminder)
        {
            try
            {

                return notesRL.SetNoteReminder( UserID, reminder);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<ResponseNoteModel> ChangeColor(long UserID, string color)
        {
            try
            {
             
                var result = await this.notesRL.ChangeColor(UserID, color);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
      
        public async Task<ResponseNoteModel> Archive(long UserID)
        {
            try
            {
                 var result = await this.notesRL.Archive(UserID);
                return result;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

    
       

       

    }

}
