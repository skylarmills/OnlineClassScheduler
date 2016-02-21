using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WSAD_App1.Models.Data
{
    [Table("tblSession")]
    public class Session
    {
        [Key]
        public int Id { get; set; }
        public string Course { get; set; }
        public string Instructor { get; set; }
        public string MeetingDate { get; set; }
        public string MeetingTime { get; set; }
        public int Occupancy { get; set; }
    }
}