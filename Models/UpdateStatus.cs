using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMWService.Models
{
    public class UpdateStatus
    {
        public int ID { get; set; }
        public int Status_Id { get; set; }
        public string? ChargeType { get; set; }
        public string? Reason { get; set; }
        public int UpdateBy { get; set;}
        public DateTime UpdateDate { get; set; }
    }
}
