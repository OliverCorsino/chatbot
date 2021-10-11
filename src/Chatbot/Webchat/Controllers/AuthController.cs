using System;
using AutoMapper;
using Boundary.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Webchat.Helpers;
using Webchat.Models;
using Webchat.Validators;

namespace Webchat.Controllers
{
    /// <summary>
    /// Represents the API controller used for handling the requests for the authentication functionality.
    /// </summary>
    [Route("api/auth")]
    [ApiController]
    public sealed class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly JwtHandler _jwtHandler;

        /// <summary>
        /// Initializes a new instance of <see cref="AuthController"/>.
        /// </summary>
        /// <param name="userManager">Represents a <see cref="UserManager{T}"/> for the user managing.</param>
        /// <param name="mapper">Represents an implementation of <see cref="IMapper"/> used for mapping the DTO to the desired object.</param>
        /// <param name="jwtHandler">Represents the JWT configuration used for this application.</param>
        public AuthController(UserManager<User> userManager, IMapper mapper, JwtHandler jwtHandler)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtHandler = jwtHandler;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="signUpRequest"></param>
        /// <returns></returns>
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest signUpRequest)
        {
            if (signUpRequest == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var signUpRequestValidator = new SignUpRequestValidator();
            var validationResult = await signUpRequestValidator.ValidateAsync(signUpRequest);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var user = _mapper.Map<User>(signUpRequest);
            var result = await _userManager.CreateAsync(user, signUpRequest.Password);

            if (!result.Succeeded)
            {
                var errorMessage = result.Errors.Select(e => e.Description);

                return BadRequest(errorMessage);
            }

            return StatusCode(201);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authRequest"></param>
        /// <returns></returns>
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] AuthRequest authRequest)
        {
            if (authRequest == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var signInRequestValidator = new SignInRequestValidator();
            var validationResult = await signInRequestValidator.ValidateAsync(authRequest);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var user = await _userManager.FindByNameAsync(authRequest.Username);

            if (user == null || !await _userManager.CheckPasswordAsync(user, authRequest.Password))
            {
                return Unauthorized("Invalid credentials.");
            }

            SigningCredentials signingCredentials = _jwtHandler.GetSigningCredentials();
            List<Claim> claims = _jwtHandler.GetClaims(user);
            JwtSecurityToken tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);

            string token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new { token });
        }
    }
}
