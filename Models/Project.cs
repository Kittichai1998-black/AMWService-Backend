using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMWService.Models
{
    public class Project
    {
        public int ID { get; set; }
        public string Project_Code { get; set; }
        public string Project_Name { get; set; }
        public int Customer_Id { get; set; }
        public short Status { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateBy { get; set; }
    }
}
