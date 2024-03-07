using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMWService.Models
{
    public class ResetPassword
    {
        //public string Id { get; set; }
        //public int UserID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
