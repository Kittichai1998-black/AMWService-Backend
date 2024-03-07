using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMWService.Models
{
    public class ViewMAProject
    {
        //[Key]
        public int ID { get; set; }
        public int MA_Customer_Id { get; set; }
        public string Customer { get; set; }
        public string MA_Code { get; set; }
        public string MA_Name { get; set; }
        public bool MA_PM { get; set; }
        public bool MA_Service { get; set; }
        public bool MA_Insurance { get; set; }
        public string MA_Detail { get; set; }
        public string File_Name { get; set; }
        
        public DateTime MA_Effective_Start { get; set; }
        public DateTime MA_Effective_End { get; set; }
        public short Status { get; set; }

        [NotMapped]
        public string FileSrc { get; set; }

    }
}
