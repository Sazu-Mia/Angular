using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mid_10_Evidence_API.ViewModels
{
    public class ImageUpload
    {

        public IFormFile Picture { get; set; } = default!;
    }
}