using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMWService.IdentityAuth
{
    public class User : IdentityUser
    {
        //[Key]
        //[NotMapped]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public int UserID { get; set; }
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public int DepartmentID { get; set; }
        public int UserRoleID { get; set; }
        public short Status { get; set; }
    }
}
