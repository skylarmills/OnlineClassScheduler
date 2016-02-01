using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSAD_App1.Models.ViewModels.Account
{
    public class CreateUserViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string PasswordConfirm { get; set; }
        public bool ReceiveEmail { get; set; }
    }
}