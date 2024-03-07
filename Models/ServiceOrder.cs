using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMWService.Models
{
    public class ServiceOrder
    {
        public int ID { get; set; }
        public int Repair_id { get; set; }
        public int RP_Operator_Id { get; set; }
        public int RP_Custommer_id { get; set; }
        public int RP_Employee_Id { get; set; }
        public DateTime RP_Inform_Date { get; set; }
        public DateTime RP_DueDate { get; set; }
    }
}
