using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryLayer
{
    public partial class NoteLable
    {
        public NoteLable()
        {
            Lables = new HashSet<Lable>();
        }

        public long LableId { get; set; }
        public long? NoteId { get; set; }

        public virtual Note Note { get; set; }
        public virtual ICollection<Lable> Lables { get; set; }
    }
}
