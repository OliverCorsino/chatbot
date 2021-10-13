namespace Webchat.Models
{
    /// <summary>
    /// Represents the view entity for handling the sign up request.
    /// </summary>
    public sealed class SignUpRequest
    {
        /// <summary>
        /// Represents the username a user will have for its account.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Represents the security password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Represents the confirmation password for security reasons.
        /// </summary>
        public string ConfirmPassword { get; set; }
    }
}
