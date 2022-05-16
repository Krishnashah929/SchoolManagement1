using System;

namespace SM.Common
{
    public static class CommonValidations
    {
        /// <summary>
        /// The required error message....
        /// </summary>
        public const string RequiredErrorMsg = "Please enter {0}.";

        /// <summary>
        /// The select required error message....
        /// </summary>
        public const string SelectRequiredErrorMsg = "Please select {0}.";

        /// <summary>
        /// Defines the RecordExistsMsg.
        /// </summary>
        public const string RecordExistsMsg = "The record already exists.";

        /// <summary>
        /// Defines the RecordExistsMsg.
        /// </summary>
        public const string RecordNotExistsMsg = "The record is not exist.";

        /// <summary>
        /// Defines the ValidData.
        /// </summary>
        public const string ValidData = "Please enter valid data.";

        /// <summary>
        /// Defines the PleaseEnterValidEmail.
        /// </summary>
        public const string PleaseEnterValidEmail = "Please enter a valid Email Address.";

        /// <summary>
        /// The required length  error message...
        /// </summary>
        public const string RequiredLengthErrorMsg = "{0} length must be between {2} and {1}.";

        /// <summary>
        /// The invalid mobile error message...
        /// </summary>
        public const string InvalidMobileErrorMsg = "PLease Enter valid Mobile Number";

        /// <summary>
        /// The password regular expression...
        /// </summary>
        public const string PasswordRegex = "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$";

        /// <summary>
        /// The comparing password message...
        /// </summary>
        public const string ComparePasswordMsg = "Password does not match";

        /// <summary>
        /// The reenter password message...
        /// </summary>
        public const string RetypePasswordMesg = "Please reenter your password";

        /// <summary>
        /// The string regular expression...
        /// </summary>
        public const string StringRegex = "[a-zA-Z0-9]*$";

        /// <summary>
        /// The string max length...
        /// </summary>
        public const string StringLength = "The {0} must be less than {10} characters.";

        /// <summary>
        /// The update password message...
        /// </summary>
        public const string PasswordUpdateMsg = "New password updated successfully";

         /// <summary>
        /// The not update password message...
        /// </summary>
        public const string PasswordNotUpdateMsg = "Something is invalid";

        /// <summary>
        /// The sending link for reset password message...
        /// </summary>
        public const string LinkSendMsg = "Reset password link has been sent to your email id";
        
        /// <summary>
        /// The sending link for reset password message...
        /// </summary>
        public const string WrongMailMsg = "You entered wrong e-mail address";

    }
}

