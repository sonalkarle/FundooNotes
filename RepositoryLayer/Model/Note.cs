using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryLayer
{
    public partial class Note
    {
        internal bool isPin;

        public Note()
        {
            Collaborators = new HashSet<Collaborator>();
            LabaleTables = new HashSet<LabaleTable>();
        }

        public long? UserId { get; set; }
        public long NoteId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public DateTime? Reminder { get; set; }
        public bool? IsPin { get; set; }
        public bool? IsArchieve { get; set; }
        public bool? IsTrash { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModificaionTime { get; set; }

        public virtual UserAccount User { get; set; }
        public virtual ICollection<Collaborator> Collaborators { get; set; }
        public virtual ICollection<LabaleTable> LabaleTables { get; set; }
    }
}
