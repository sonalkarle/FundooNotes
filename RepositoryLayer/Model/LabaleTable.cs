using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryLayer
{
    public partial class LabaleTable
    {
        public long? NoteId { get; set; }
        public long LableId { get; set; }
        public string LableName { get; set; }
        public long? UserId { get; set; }

        public virtual Note Note { get; set; }
        public virtual UserAccount User { get; set; }
    }
}
