using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMWService.Models
{
    public class MA_Project
    {
        [Key]
        public int ID { get; set; }
        public int MA_Customer_Id { get; set; }
        public string MA_Code { get; set; }
        public string MA_Name { get; set; }
        public bool MA_PM { get; set; }
        public bool MA_Service { get; set; }
        public bool MA_Insurance { get; set; }
        public string? MA_Detail { get; set; }
        public DateTime? MA_Effective_Start { get; set; }
        public DateTime? MA_Effective_End { get; set; }
        public short Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateBy { get; set; }

    }
}
