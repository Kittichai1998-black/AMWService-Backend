using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AMWService.Models
{
    public class WorkGroup
    {
        [Key]

        public int ID { get; set; }
        public int Service_ID { get; set; }
        //public string Service_Code { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string FirsName { get; set; }
        public int DepartmentID { get; set; }
        public short Status { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateBy { get; set; }

    }
}
