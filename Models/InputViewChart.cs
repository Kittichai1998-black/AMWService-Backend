using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AMWService.Models
{
    public class InputViewChart
    {
        public int? UserID { get; set; }
        public int? StatusID { get; set; }
    }
}
