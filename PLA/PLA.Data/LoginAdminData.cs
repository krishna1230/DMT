using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;



namespace PLA.Data
{
    public class LoginAdminData : LoginAdmin
    {

      
        public string AdminPhoto { get; set; }

        [Required(ErrorMessage = "Please Select Role")]
        public int Fk_Role { get; set; }

        [Required(ErrorMessage = "Admin Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Contact No is required")]
        public string ContactNo { get; set; }



        [Required(ErrorMessage = "E-mail Id is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [EmailAddress]
        public string EmailId { get; set; }

   
        [Required(ErrorMessage = "Password is required")]
        [StringLength(10, ErrorMessage = "Must be between 5 and 10 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [StringLength(10, ErrorMessage = "Must be between 5 and 10 characters", MinimumLength = 5)]
        public string ConfirmPassword { get; set; }



    }

    public class AdminLoginModel : LoginAdmin
    {
        [Required(ErrorMessage = "E-mail Id is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [EmailAddress]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Password is required")]
        //[StringLength(10, ErrorMessage = "Must be between 5 and 10 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public class ForgotPasswordModel : LoginAdmin
    {
        [Required(ErrorMessage = "E-mail Id is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [EmailAddress]
        public string EmailId { get; set; }


    }
    public class ChangePasswordModel : LoginAdmin
    {
        [Required(ErrorMessage = "Password is required")]
        [StringLength(10, ErrorMessage = "Must be between 5 and 10 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New Password is required")]
        [DataType(DataType.Password)]
        [StringLength(10, ErrorMessage = "Must be between 5 and 10 characters", MinimumLength = 5)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm New Password is required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }


    }
}

