using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMWService.Models
{
    public class Department
    {
        [Key]
        public int Department_Id { get; set; }
        public string DP_Name { get; set; }

    }
}
