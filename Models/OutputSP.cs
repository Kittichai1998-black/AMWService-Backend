using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMWService.Models
{
    public partial class OutputSP
    {
        [Key]
        
        //public int ID { get; set; }
        public int So_id { get; set; }
        public string So { get; set; }
        public int Project_Id { get; set; }
        public string MA_Name { get; set; }
        public int StatusID { get; set; }
        public string Reason { get; set; }
        public int StepID { get; set; }
        public string StatusName { get; set; }
        public string Custommer { get; set; }
        public string RootCause { get; set; }
        public string ChargeType { get; set; }
        //public string HowToFix { get; set; }
        //public string Effort { get; set; }
        //public int? CauseDocID { get; set; }
        public string Operator_Name { get; set; }
        //public string Responsible { get; set; }
        public string Company { get; set; }
        public string Latitude { get; set; }
        public string Longtitude { get; set; }
        //public string Imgname { get; set; }
        public string Problem { get; set; }
        public int CreateBy { get; set; }
        public string CreateName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
