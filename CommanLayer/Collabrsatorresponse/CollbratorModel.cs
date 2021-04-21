using System;
using System.Collections.Generic;
using System.Text;

namespace CommanLayer.Collabrsatorresponse
{
    public class CollbratorModel
    {

        public int CollaboratorId { get; set; }
        public long? UserId { get; set; }
        public long? NoteId { get; set; }
        public string CollaboratorEmail { get; set; }
    }
}
