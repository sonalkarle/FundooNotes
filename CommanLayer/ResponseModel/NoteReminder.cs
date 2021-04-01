using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommanLayer.ResponseModel
{
    public class NoteReminder
    {
        public long UserID { get; set; }
        [Required]
        public long NoteID { get; set; }
        [Required]
        public DateTime ReminderOn { get; set; }
    }
}
