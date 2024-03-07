using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMWService.Models
{
    public class OutputSPViewSO
    {
        [Key]

        //public int ID { get; set; }
        public int ID { get; set; }
        public string Code { get; set; }
        public int Custommer_Id { get; set; }
        public string Customer { get; set; }
        public int Status_Id { get; set; }
        public string StatusName { get; set; }
        public int Priority_Id { get; set; }
        public string priorityName { get; set; }
        public int Rootcause_Id { get; set; }
        public string CaustName { get; set; }

        public string Cause { get; set; }
        public string HowToFix { get; set; }
        public string Problem { get; set; }
        public string FirsName { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
