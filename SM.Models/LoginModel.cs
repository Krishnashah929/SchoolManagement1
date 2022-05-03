using System;
using SM.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SM.Models
{
    public class LoginModel
    {
        /// <summary>
        /// FirstName input feild.
        /// </summary>
        [Required(ErrorMessage = CommonValidations.RequiredErrorMsg)]
        [MaxLength(10)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// LastName input feild.
        /// </summary>
        [Required(ErrorMessage = CommonValidations.RequiredErrorMsg)]
        [MaxLength(10)]
        [DisplayName("Last Name")]
        public string Lastname { get; set; }

        /// <summary>
        /// Email Address input feild.
        /// </summary>
        [Required(ErrorMessage = CommonValidations.RequiredErrorMsg)]
        [MaxLength(50)]
        [DisplayName ("Email")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Password input feild.
        /// </summary>
        [Required(ErrorMessage = CommonValidations.RequiredErrorMsg)]
        [DataType(DataType.Password)]
        //[RegularExpression(CommonValidations.RequiredStrongPwdErrorMsg)]
        [DisplayName("Password")]
        public string Password { get; set; }
    }
}
