using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMWService.Models
{
    public class InputViewSO
    {
        [Key]
        //public int ID { get; set; }
        public int? UserID { get; set; }
        public int? StatusID { get; set; }
        public string SO { get; set; }
        public int? CustomerID { get; set; }
        public int? CauseID { get; set; }
        public int? PriolityID { get; set; }
        public string Owner { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
