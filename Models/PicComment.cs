using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AMWService.Models
{
    public class PicComment
    {
        [Key]
        public string Pic_Repair_AQ_Id { get; set; }
        public string? PRAQ_Name_Full { get; set; }
        public string PRAQ_Name { get; set; }
        public int? PRAQ_Ans_Question_Id { get; set; }
        public int PR_Repair_Id { get; set; }
        public int? Comment_Id { get; set; }
        //public int CreateBy { get; set; }
        [NotMapped]
        //[Required(ErrorMessage = "Please choose Photo image")]
        //[Display(Name = "Profile Picture")]
        //public IFormFile ImageFile { get; set; }
        public string ImageSrc { get; set; }
    }
}
