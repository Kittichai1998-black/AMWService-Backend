using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMWService.Models
{
    public class Company
    {
        public int ID { get; set; }
        public string CP_Short_Name { get; set; }
        public string CP_Name { get; set; }
        public string CP_Address { get; set; }
        public string CP_Phone { get; set; }
        public string CP_Email { get; set; }
        public int Customer_Id { get; set; }
        public string CP_Latitude { get; set; }
        public string CP_Longitude { get; set; }
        public short Status { get; set; }
    }
}
