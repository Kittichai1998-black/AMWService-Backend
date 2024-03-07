using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMWService.Models
{
    public class RemoveOperator
    {
        public int ID { get; set; }
        public short Status { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateBy { get; set; }
    }
}
