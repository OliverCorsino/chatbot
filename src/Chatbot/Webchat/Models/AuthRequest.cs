namespace Webchat.Models
{
    /// <summary>
    /// Represents the view entity for handling the authentication request.
    /// </summary>
    public sealed class AuthRequest
    {
        /// <summary>
        /// Represents the username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Represents the security password.
        /// </summary>
        public string Password { get; set; }
    }
}
