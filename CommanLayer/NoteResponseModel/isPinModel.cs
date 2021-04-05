using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommanLayer.ResponseModel
{
    public class isPinModel
    {
        [Required]
        public bool isPin { get; set; }
    }
}
