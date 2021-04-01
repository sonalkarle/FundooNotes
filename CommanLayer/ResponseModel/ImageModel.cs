using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommanLayer.ResponseModel
{
    public class ImageModel
    {
        [Required]
        public string Image { get; set; }
    }
}
