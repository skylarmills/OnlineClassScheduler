﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WSAD_App1.Models.Data
{
    [Table("tblUserRoles")]
    public class UserRole
    {
        [Key]
        [Column(Order = 0)]
        public int UserId { get; set; }
        [Key, Column(Order = 1)]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}