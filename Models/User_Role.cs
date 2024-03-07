using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AMWService.Models
{
    public class User_Role
    {
        [Key]
        public int User_ID { get; set; }
        public int Role_ID { get; set; }
    }
}
