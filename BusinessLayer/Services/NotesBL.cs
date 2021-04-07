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
using CommonLayer.RequestModel;

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


        public ResponseNoteModel AddUserNote(ResponseNoteModel note)
        {
            return AddUserNoteTask(note).Result;
        }
        public async Task<ResponseNoteModel> AddUserNoteTask(ResponseNoteModel note)
        {
            try
            {
               
                if (note.Collaborators != null)
                {
                    note.Collaborators = note.Collaborators.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
                }
                if (note.isTrash || note.isArchieve)
                {
                    note.isPin = false;
                }
                await redis.RemoveNotesRedisCache(note.UserID);
                return notesRL.AddUserNote(note);
            }
            catch (Exception)
            {
                throw;
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

       

        public async Task<ResponseNoteModel> Pin(long UserID,long noteID)
        {
            try
            {
                
                var result = await this.notesRL.pin(UserID,noteID);
                return result;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
      
      
        public async Task<ResponseNoteModel> UploadImage(IFormFile image, long UserID,long noteID)
        {
            try
            {
            
                var result = await this.notesRL.UploadImage(image, UserID,noteID);
                return result;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }


        public async Task<ResponseNoteModel> DeleteNote(long UserID,long noteID)
        {
            try
            {
                await redis.RemoveNotesRedisCache(UserID);
                ResponseNoteModel result = notesRL.DeleteNote(UserID,noteID);
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
        public async Task<ResponseNoteModel> Restore(long UserID,long noteID)
        {
            try
            {
                var result =  await this.notesRL.Restore(UserID,noteID);
                return result;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<ResponseNoteModel> UpdateNote(ResponseNoteModel note,long UserId,long NoteId)
        {
            try
            {
                
               var result=  await notesRL.UpdateNote(note,UserId,NoteId);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<NoteReminder> SetNoteReminder(NoteReminder reminder,long UserId,long NoteId )
        {
            try
            {
             
                if (reminder.ReminderOn < DateTime.Now)
                {
                    throw new Exception("Time is passed");
                }
                if (reminder.NoteID == default)
                {
                    throw new Exception("NoteID missing");
                }
                var result = await notesRL.SetNoteReminder(reminder,UserId,NoteId);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<string> DeleteNoteReminder( long UserId, long NoteId)
        {
            try
            {

               
               return await notesRL.DeleteNoteReminder( UserId, NoteId);
            
            }
            catch (Exception)
            {

                throw;
            }
        }



        public async Task<ResponseNoteModel> ChangeColor(long UserID, string color,long noteID)
        {
            try
            {
             
                var result = await this.notesRL.ChangeColor(UserID, color,noteID);
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
      
        public async Task<ResponseNoteModel> Archive(long UserID,long noteID)
        {
            try
            {
                 var result = await this.notesRL.Archive(UserID,noteID);
                return result;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

    
       

       

    }

}
