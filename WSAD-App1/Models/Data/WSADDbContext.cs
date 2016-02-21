using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WSAD_App1.Models.Data
{
    public class WSADDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Correspondence> Correspondences { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
    }
}