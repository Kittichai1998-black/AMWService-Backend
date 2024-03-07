using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace AMWService.Models
{
    public class Role
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public short Status { get; set; }
    }
}
