using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WSAD_App1.Areas.Admin.Models.ViewModels.ManageSession
{
    public class CreateSessionViewModel
    {
        [Required]
        public string Course { get; set; }
        [Required]
        public string Instructor { get; set; }
        [Required]
        public string MeetingDate { get; set; }
        [Required]
        public string MeetingTime { get; set; }
        [Required]
        public string Description { get; set; }
        //public int Occupancy { get; set; }
    }
}