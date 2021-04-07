using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryLayer
{
    public partial class UserAccount
    {
        public UserAccount()
        {
            Collaborators = new HashSet<Collaborator>();
            LabaleTables = new HashSet<LabaleTable>();
            Notes = new HashSet<Note>();
        }

        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? Creationtime { get; set; }
        public DateTime? Modificationtime { get; set; }

        public virtual ICollection<Collaborator> Collaborators { get; set; }
        public virtual ICollection<LabaleTable> LabaleTables { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
    }
}
