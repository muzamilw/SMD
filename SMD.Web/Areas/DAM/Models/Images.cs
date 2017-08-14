using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SMD.Models.DomainModels;


namespace SMD.MIS.Areas.DAM.Models
{
    public class ImagesModel
    {
   
        public List<DamImage> Images { get; set; }
        public List<DamImage> FreeImages { get; set; }

        [Display(Name = "Local file")]
        public HttpPostedFileBase File { get; set; }

        public bool IsFile { get; set; }

        [Range(0, int.MaxValue)]
        public int X { get; set; }

        [Range(0, int.MaxValue)]
        public int Y { get; set; }

        [Range(1, int.MaxValue)]
        public int Width { get; set; }

        [Range(1, int.MaxValue)]
        public int Height { get; set; }

        public int CompanyId { get; set; }

        public int mode { get; set; }
        public string bytes { get; set; }
    }
    public class ImageCropModel
    {
        public string fileName { get; set; }
        public string bytes { get; set; }
        public long imageId { get; set; }
        public int CompanyId { get; set; }
        public int mode { get; set; }
    }
}