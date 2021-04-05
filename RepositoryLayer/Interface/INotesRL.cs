using CommanLayer.ResponseModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface INotesRL
    {


        public Note AddUserNote(ResponseNoteModel note);
        public ResponseNoteModel DeleteNote(long UserID);
       
        public ICollection<ResponseNoteModel> GetNotes(long UserID);
        public ResponseNoteModel SetNoteReminder( long userID, DateTime reminder);
        Task<ResponseNoteModel> Archive(long UserID);
     
        List<Note> ArchiveList();
        Task<ResponseNoteModel> UploadImage(IFormFile image, long UserID);
        Task<ResponseNoteModel> Restore(long UserID);

       Task<ResponseNoteModel> pin(long UserID);
        Task<ResponseNoteModel> ChangeColor(long UserID, string color);

       
        Task<string> EmptyTrash();
       
        
       




    }

}
