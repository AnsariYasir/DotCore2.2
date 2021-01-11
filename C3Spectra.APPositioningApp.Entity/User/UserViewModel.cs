using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace C3Spectra.APPositioningApp.Entity.User
{
    public class UserViewModel : BaseViewModel
    {

        public int UserID { get; set; }

        public int RoleID { get; set; }

        [Required(ErrorMessage = "Please enter email")]
        public string EmailAddress
        {
            get; set;
        }
        [Required(ErrorMessage = "Please enter password")]
        public string password
        {
            get; set;

        }
        public bool Remember { get; set; }

        public string newpassword { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
