using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMWService.Models
{
    public class FileMA
    {
        [Key]
        //public int MA_Project_Id { get; set; }
        public string MA_Project_Name { get; set; }
        public string File_Name { get; set; }
        [NotMapped]
        public string FileSrc { get; set; }
    }
}
