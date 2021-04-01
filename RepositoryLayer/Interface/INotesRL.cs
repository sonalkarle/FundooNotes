using CommanLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INotesRL
    {


        public ResponseNoteModel AddUserNote(ResponseNoteModel note);
        bool DeleteNote(long UserID, long noteID);

        public ICollection<ResponseNoteModel> GetNotes(long UserID, bool IsTrash, bool IsArchieve);
        public bool ChangeColor(long noteID, long userID, string colorCode);
        public bool Image(int id, ImageModel image);
        public bool SetNoteReminder(NoteReminder reminder);
        public bool isPin(long noteID, long userID);
        public bool isArchive(long noteID, long userID);




    }

}
