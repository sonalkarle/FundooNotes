using CommanLayer.ResponseModel;
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

        public Note AddUserNote(ResponseNoteModel note);
        public Task<ICollection<ResponseNoteModel>> GetActiveNotes(long UserID);
        Task<ResponseNoteModel> DeleteNote(long UserID);
        Task<ResponseNoteModel> ChangeColor(long UserID, string color);
        public ResponseNoteModel SetNoteReminder(long userID, DateTime reminder);
        Task<ResponseNoteModel> Archive(long UserID);
      

        Task<ResponseNoteModel> Pin(long UserId);
        Task<ResponseNoteModel> UploadImage(IFormFile image, long UserID);
        Task<ResponseNoteModel> Restore(long UserID);
        Task<string> EmptyTrash();
       

        











    }

}
