using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMWService.Models
{
    public class ResponseCreate
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Customer { get; set; }
        public string Problem { get; set; }
        public short Status { get; set; }
        public string Message { get; set; }

    }
}
