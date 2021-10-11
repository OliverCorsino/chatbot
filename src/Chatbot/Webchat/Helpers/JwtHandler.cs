using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Webchat.Helpers
{
    /// <summary>
    /// Represents a helper class for handling the configuration of the JWT functionality.
    /// </summary>
    public sealed class JwtHandler
    {
        private readonly IConfigurationSection _jwtSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtHandler"/> class.
        /// </summary>
        /// <param name="configuration">Represents an implementation of <see cref="IConfiguration"/></param>
        public JwtHandler(IConfiguration configuration) => _jwtSettings = configuration.GetSection("JwtSettings");

        /// <summary>
        /// Retrieves the configured signing credentials.
        /// </summary>
        /// <returns>A configured <see cref="SigningCredentials"/>.</returns>
        public SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        /// <summary>
        /// Retrieves the configured claims.
        /// </summary>
        /// <param name="user">Represents an <see cref="IdentityUser"/> instance used for configuring the claims.</param>
        /// <returns>An list with the configured claims</returns>
        public List<Claim> GetClaims(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName, ClaimValueTypes.String),
                new Claim("username", user.UserName, ClaimValueTypes.String)
            };

            return claims;
        }

        /// <summary>
        /// Generates the token that will be used for an user.
        /// </summary>
        /// <param name="signingCredentials">Represents the configured signing credentials.</param>
        /// <param name="claims">Represents the configured claims.</param>
        /// <returns>A valid token.</returns>
        public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSettings.GetSection("validIssuer").Value,
                audience: _jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.GetSection("expiryInMinutes").Value)),
                signingCredentials: signingCredentials);

            return tokenOptions;
        }
    }
}
