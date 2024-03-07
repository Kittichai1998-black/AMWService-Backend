using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMWService.Models
{
    public class User_Roles
    {
        [Key]
        public int ID { get; set; }
        public string UR_Name_Full { get; set; }
        public bool UR_New_Status { get; set; }
        public bool UR_BackLog_Status { get; set; }
        public bool UR_Todo_Status { get; set; }
        public bool UR_Doing_Status { get; set; }
        public bool UR_Resolved_Status { get; set; }
        
        public bool UR_Done_Status { get; set; }
        public bool UR_Charge_Status { get; set; }
        public bool UR_Close_Status { get; set; }
        public bool UR_Reject_Status { get; set; }



    }
}
