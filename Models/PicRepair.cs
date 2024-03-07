using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMWService.Models
{
    public class PicRepair
    {
        //public int ID { get; set; }
        [Key]
        public string Pic_Repair_Id { get; set; }
        public string PR_Name { get; set; }
        public int PR_Repair_Id { get; set; }
        //public short Status { get; set; }
        [NotMapped]
        //[Required(ErrorMessage = "Please choose Photo image")]
        //[Display(Name = "Profile Picture")]
        //public IFormFile ImageFile { get; set; }
        public string ImageSrc { get; set; }
    }
}
