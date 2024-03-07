using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMWService.Models
{
    public class CreateSO
    {
        [Key]
        public int ID { get; set; }
        //[Column(TypeName = "nvarchar(50)")]
        public string Code { get; set; }
        public int Customer_Id { get; set; }
        public int? Project_Id { get; set; }
        //public int Status_Id { get; set; }
        public int Priority_Id { get; set; }
        //public int Type_Id { get; set; }
        public int Rootcause_Id { get; set; }

        //[Column(TypeName = "nvarchar(100)")]
        public string Problem { get; set; }
        public int? Department_Id { get; set; }
        //public int? User_Id { get; set; }
        public int? Operator_Id { get; set; }
        public DateTime? DueDate { get; set; }
        public string Reason { get; set; }
        public short Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public int CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateBy { get; set; }
    }
}
