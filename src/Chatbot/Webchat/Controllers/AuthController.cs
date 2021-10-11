using AutoMapper;
using Boundary.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly SignUpRequestValidator _signUpRequestValidator = new SignUpRequestValidator();

        /// <summary>
        /// Initializes a new instance of <see cref="AuthController"/>.
        /// </summary>
        /// <param name="userManager">Represents a <see cref="UserManager{T}"/> for the user managing.</param>
        /// <param name="mapper">Represents an implementation of <see cref="IMapper"/> used for mapping the DTO to the desired object.</param>
        public AuthController(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest signUpRequest)
        {
            if (signUpRequest == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var validationResult = await _signUpRequestValidator.ValidateAsync(signUpRequest);

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
    }
}
