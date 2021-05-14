using CommanLayer.ResponseModel;
using CommonLayer.RequestModel;
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


        public ResponseNoteModel AddUserNote(ResponseNoteModel note);
        public ResponseNoteModel DeleteNote(long UserID, long noteID);
       
        public ICollection<ResponseNoteModel> GetNotes(long UserID);
        Task<ResponseNoteModel> Archive(long UserID, long noteID);
     
        List<Note> ArchiveList();
        Task<ResponseNoteModel> UploadImage(IFormFile image, long UserID, long noteID);
        Task<ResponseNoteModel> Restore(long UserID, long noteID);

       Task<ResponseNoteModel> pin(long UserID, long noteID);
        Task<ResponseNoteModel> ChangeColor(long UserID, string color, long noteID);
        Task<NoteReminder> SetNoteReminder(NoteReminder reminder, long UserID, long NoteID);
        Task<string> DeleteNoteReminder(long UserID, long NoteID);
        ResponseNoteModel UpdateNote(ResponseNoteModel note);







        Task<string> EmptyTrash();
       
        
       




    }

}
