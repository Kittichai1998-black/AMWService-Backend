using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMWService.Models
{
    public class MachineList
    {
        [Key]

        public int ID { get; set; }
        public int MC_Customer_Id { get; set; }
        public string MC_Code { get; set; }
        public string MC_Name { get; set; }
        public string MC_Size { get; set; }
        public int MC_Count { get; set; }
        public short Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? Updatedate { get; set; }
        public int? UpdateBy { get; set; }
    }
}
