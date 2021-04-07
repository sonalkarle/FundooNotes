using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommanLayer.ResponseModel

{
    public class ForgetPasswordModel
    {
        [Required]
        public string Email { get; set; }
      


    }
}
