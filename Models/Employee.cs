using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace AMWService.Models
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }
        public string EP_Username { get; set; }
        //public string EP_FirsName { get; set; }
        //public string EP_LastName { get; set; }
    }
}
