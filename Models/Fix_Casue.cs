using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AMWService.Models
{
    public class Fix_Casue
    {
        [Key]
        //public int ID { get; set; }
        public string CauseName { get; set; }
        public string HowToFix { get; set; }
        public string Effort { get; set; }
        public int CreateBy { get; set; }
        public int Service_ID { get; set; }
        public DateTime? CreateDate { get; set; }

    }
}
