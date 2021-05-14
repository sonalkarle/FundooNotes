using System;
using System.Collections.Generic;
using System.Text;

namespace CommanLayer.ResponseModel
{
    public class ResponseNoteModel
    {
        public long UserID { get; set; }
        public long NoteId { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public DateTime Reminder { get; set; }
        public bool isPin { get; set; }
        public bool isArchieve { get; set; }
        public bool isTrash { get; set; }
        public DateTime CreateonTime { get; set; }
        public DateTime ModificationTime { get; set; }

        public ICollection<string> Collaborators { get; set; }

       



    }
}
