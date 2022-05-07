using SM.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace SM.Entity
{
    public partial class User
    {

        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public int UserId { get; set; }

        /// <summary>
        /// FirstName input feild.
        /// </summary>
        [Required(ErrorMessage = CommonValidations.RequiredErrorMsg)]
        [StringLength(8, ErrorMessage = CommonValidations.RequiredLengthErrorMsg, MinimumLength = 6)]
        [DisplayName ("First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// LastName input feild.
        /// </summary>
        [Required(ErrorMessage = CommonValidations.RequiredErrorMsg)]
        [StringLength(8, ErrorMessage = CommonValidations.RequiredLengthErrorMsg, MinimumLength = 6)]
        [DisplayName("Last Name")]
        public string Lastname { get; set; }

        /// <summary>
        /// Email Address input feild.
        /// </summary>
        [Required(ErrorMessage = CommonValidations.RequiredErrorMsg)]
        //[StringLength(8, ErrorMessage = CommonValidations.RequiredLengthMailErrorMsg)]
        //[MinLength(5, ErrorMessage = CommonValidations.RequiredLengthMailErrorMsg)]
        [DisplayName("Email")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Password input feild.
        /// </summary>
        [Required(ErrorMessage = CommonValidations.RequiredErrorMsg)]
        [DataType(DataType.Password)]
        [StringLength(10, ErrorMessage = CommonValidations.RequiredLengthErrorMsg, MinimumLength = 8)]
        [DisplayName("Password")]
        public string Password { get; set; }

        /// <summary>
        /// ISactive feild for users.
        /// </summary>
        [DisplayName("Active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// IsDeleted feild when you have to delete user.
        /// </summary>
        [DisplayName("Delete")]
        public bool IsDelete { get; set; }

        /// <summary>
        /// IsBlocked feild when you have to delete user.
        /// </summary>
        [DisplayName("Blocked")]
        public bool IsBlocked { get; set; }

        /// <summary>
        /// Created by field for first time of creation of user.
        /// </summary>
        [DisplayName("Created By")]
        public int CreatedBy { get; set; }

        /// <summary>
        /// Created date field for first time of user create account date.
        /// </summary>
        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// ModifiedBy field for modiffy details of user.
        /// </summary>
        [DisplayName("Modified By")]
        public int? ModifiedBy { get; set; }

        /// <summary>
        /// ModifiedDate field for user modify their details.
        /// </summary>
        [DisplayName("Modified Date")]
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// RetypePassword it is for confirm user's password.
        /// This value is not stored into the database.
        /// </summary>
        [NotMapped]
        [Required(ErrorMessage = CommonValidations.RetypePasswordMesg)]
        [Compare("Password", ErrorMessage = CommonValidations.ComparePasswordMsg)]
        [DataType(DataType.Password)]
        [DisplayName("Reenter Password")]
        public string RetypePassword { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }

        public string Role { get; set; }
        //public IEnumerable<User> GetUsers()
        //{
        //    return new List<User>() { new User { UserId = 101, FirstName = "Krishna", Lastname = "Shahhh", EmailAddress = "kdshah929@gmail.com", Password = "12345678", Role = "Admin" };
        //}
    }
}

