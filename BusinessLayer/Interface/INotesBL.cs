using CommanLayer.ResponseModel;
using CommonLayer.RequestModel;
using Microsoft.AspNetCore.Http;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface INotesBL
    {
        
        public ResponseNoteModel AddUserNote(ResponseNoteModel note);
        public Task<ICollection<ResponseNoteModel>> GetActiveNotes(long UserID);
        Task<ResponseNoteModel> DeleteNote(long UserID,long noteID);
        Task<ResponseNoteModel> ChangeColor(long UserID, string color, long noteID);
      
        Task<ResponseNoteModel> Archive(long UserID,long noteID);

        Task<string> DeleteNoteReminder( long UserId, long NoteId);
        Task<NoteReminder> SetNoteReminder(NoteReminder reminder, long UserId, long NoteId);
        Task<ResponseNoteModel> UpdateNote(ResponseNoteModel note, long UserId, long NoteId);




        Task<ResponseNoteModel> Pin(long UserId, long noteID);
        Task<ResponseNoteModel> UploadImage(IFormFile image, long UserID,long noteID);
        Task<ResponseNoteModel> Restore(long UserID,long noteID);
        Task<string> EmptyTrash();
       

        

        









    }

}
