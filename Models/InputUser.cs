using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMWService.Models
{
    public class InputUser
    {
        [Key]
        //public int ID { get; set; }
        //public int? StatusID { get; set; }
        //public int? CustomerID { get; set; }
        public string UserName { get; set; }
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public int DepartmentID { get; set; }
        public int PhoneNumber { get;set;}
        public string Email { get; set; }
        //public string LineID { get; set; }
    }
}
