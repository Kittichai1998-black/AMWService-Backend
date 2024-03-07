using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMWService.Models
{
    public class ViewMachineList
    {
        public int ID { get; set; }
        public int MC_Customer_Id { get; set; }
        public string Customer { get; set; }
        public string MC_Code { get; set; }
        public string MC_Name { get; set; }
        public string MC_Size { get; set; }
        public int MC_Count { get; set; }
        public short Status { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
