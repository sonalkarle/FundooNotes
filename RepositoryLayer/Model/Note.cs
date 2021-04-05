using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryLayer
{
    public partial class Note
    {
        public Note()
        {
            Collaborators = new HashSet<Collaborator>();
            NoteLables = new HashSet<NoteLable>();
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
        public virtual ICollection<NoteLable> NoteLables { get; set; }
    }
}
