using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AMWService.Models
{
    public class Operator
    {
        [Key]
        public int ID { get; set; }
        public int OP_Customer_id { get; set; }
        public string OP_Name { get; set; }
        public string OP_Name_Eng { get; set; }
        public string OP_NickName { get; set; }
        public string OP_Phone { get; set; }
        public string OP_Email { get; set; }
        public string OP_Line_id { get; set; }
        public string OP_Line_Pic { get; set; }
        public short Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? Updatedate { get; set; }
        public int? UpdateBy { get; set; }

    }
}
