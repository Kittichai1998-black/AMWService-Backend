using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMWService.Models
{
    public class InputOperator
    {
        [Key]
        //public int ID { get; set; }
        //public int? StatusID { get; set; }
        public int? CustomerID { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public string NickName { get; set; }
        public int Phone { get;set;}
        public string Email { get; set; }
        public string LineID { get; set; }
    }
}
