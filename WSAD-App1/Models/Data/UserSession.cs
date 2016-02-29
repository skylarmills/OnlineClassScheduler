using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WSAD_App1.Models.Data
{
    [Table("tblUserSession")]
    public class UserSession
    {
        [Key]
        [Column(Order = 0)]
        public int User_Id { get; set; }
        [Column(Order = 1)]
        [Key]
        public int Session_Id { get; set; }

        [ForeignKey("User_Id")]
        public virtual User User { get; set; }
        [ForeignKey("Session_Id")]
        public virtual Session Session { get; set; }

    }
}