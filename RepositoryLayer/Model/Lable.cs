using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryLayer
{
    public partial class Lable
    {
        public long? UserId { get; set; }
        public long? LableId { get; set; }
        public string LableName { get; set; }

        public virtual NoteLable LableNavigation { get; set; }
        public virtual UserAccount User { get; set; }
    }
}
