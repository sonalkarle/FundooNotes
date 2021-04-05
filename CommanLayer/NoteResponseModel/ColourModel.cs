using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommanLayer.ResponseModel
{
    public class ColorModel
    {
        [Required]
        public string Color { get; set; }
    }
}
