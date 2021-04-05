using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommanLayer.ResponseModel
{
    public class ReminderModel
    {
        [Required]
        public DateTime Reminder { get; set; }
    }
}
