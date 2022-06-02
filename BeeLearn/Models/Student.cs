using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BeeLearn.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter First Name!")]    
        [Display(Name ="First Name")]
        public string FirstName { get; set; }     
        [Required (ErrorMessage ="Please Enter Last Name!")]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }
        [Required (ErrorMessage ="Please Enter Age!")]     
        //public string Age { get; set; }
        //[Required (ErrorMessage ="Please Enter Date Of Birth!")]
        public DateTime? DOB { get; set; }
        [Required (ErrorMessage ="Please Select Gender!")]
        public string Gender { get; set; }
        [Required (ErrorMessage ="Please Select State!")]
        public string State { get; set; }
        [Required(ErrorMessage ="Please Enter User Name!")]
        //[RegularExpression(@"^([a-zA-Z])[a-zA-Z_-]*[\w_-]*[\S]$|^([a-zA-Z])[0-9_-]*[\S]$|^[a-zA-Z]*[\S]$",
        //    ErrorMessage ="Please Enter Valid User Name!")]
        [Display(Name ="User Name")]
        public string UserName { get; set; }
        [Required (ErrorMessage ="Please Enter Password!")]
        //[RegularExpression("^(?!.*([A-Za-z0-9]))(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,15}$",
        //    ErrorMessage = "please enter valid Password!")]
        public string Password { get; set; }
        [Required (ErrorMessage ="Please Enter Confirm Password!")]
        [Compare("Password",ErrorMessage = "Password doesn't match!")]
        [Display(Name ="Confirm Password")]
        public string ConfirmPassword { get; set; }

        public int? DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
    }
}
