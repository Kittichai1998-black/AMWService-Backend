using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AMWService.Models
{
    public class Register
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[Key]
        public int UserID { get; set; }
        [Required(ErrorMessage="Username is requied")]
        public string Username { get; set; }
        [Required(ErrorMessage ="Password is requied")]
        public string Password { get; set; }
        [Required(ErrorMessage = "FirsName is requied")]
        public string FirsName { get; set; }
        [Required(ErrorMessage = "LastName is requied")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "PhoneNumber is requied")]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is requied")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Department is requied")]
        public int DepartmentID { get; set; }
        [Required(ErrorMessage = "UserRoleID is requied")]
        public int UserRoleID { get; set; }
        public short Status { get; set; }
        //public int UserID { get; set; }
    }
}
