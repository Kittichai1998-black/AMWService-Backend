using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMWService.Models
{
    public class ViewServiceOrder
    {
        //[Key]
        public int ID { get; set; }
        //[Column(TypeName = "nvarchar(100)")]
        //public string uuid { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Code { get; set; }
        public int Customer_Id { get; set; }
        public string Customer { get; set; }
        public int Status_Id { get; set; }
        public int Priority_Id { get; set; }
        public string priorityName { get; set; }
        public int Type_Id { get; set; }
        public int Rootcause_Id { get; set; }

        public string CastName { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Problem { get; set; }

        //[Column(TypeName = "nvarchar(100)")]
        //public string Image { get; set; }
        public int User_Id { get; set; }
        public string UserName { get; set; }
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public DateTime CreateDate { get; set; }
        //public DateTime ModifyTime { get; set; }

        //[NotMapped]
        //[Required(ErrorMessage = "Please choose Photo image")]
        //[Display(Name = "Profile Picture")] 
        //public IFormFile ImageFile { get; set; }
        //public string ImageSrc { get; set; }
        //public int CreateBy { get; set; }

        //[Required]
        //public DateTime CreateTime { get; set; }
    }
}
