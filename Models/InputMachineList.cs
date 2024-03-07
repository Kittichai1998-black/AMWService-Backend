using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMWService.Models
{
    public class InputMachineList
    {
        [Key]
        //public int ID { get; set; }
        //public int? StatusID { get; set; }
        //public string SO { get; set; }
        public int? CustomerID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
    }
}
