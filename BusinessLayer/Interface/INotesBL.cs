using CommanLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INotesBL
    {

        public ResponseNoteModel AddUserNote(ResponseNoteModel note);

        public ICollection<ResponseNoteModel> GetActiveNotes(long UserID);
        bool DeleteNote(long UserID, long noteID);

        public bool ChangeColor(long noteID, long userID, string colorCode);
        public bool Image(int id, ImageModel image);
        public bool SetNoteReminder(NoteReminder reminder);
        public bool Pin(long noteID, long userID);
        public bool Archive(long noteID, long userID);

       




    }

}
