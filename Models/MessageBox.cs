using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AMWService.Models
{
    public class MessageBox
    {
        [Key]
        public int ID { get; set; }
        public int Service_ID { get; set; }
        public string Service_No { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public int? ParentID { get; set; }
        public short Status { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
