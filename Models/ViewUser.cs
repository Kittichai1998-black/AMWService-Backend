using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AMWService.Models
{
    public class ViewUser
    {
        [Key]
        public string Id { get; set; }
        //public int UserID { get; set; }
        public string UserName { get; set; }
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public int DepartmentID { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int UserRoleID { get; set; }
        //public string OP_Line_id { get; set; }
        //public string OP_Line_pic { get; set; }
        public short Status { get; set; }

    }
}
