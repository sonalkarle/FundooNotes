using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.ResponseModel
{
    public class ResponseUserAccount
    {
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
       
        public string Password { get; set; }
        public DateTime Creationtime { get; set; }
        public DateTime Modificationtime { get; set; }
        public long UserId { get; set; }
    }
}