using AutoMapper;
using Boundary.Persistence.Entities;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Webchat.Models;

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

            var user = _mapper.Map<User>(signUpRequest);
            var result = await _userManager.CreateAsync(user, signUpRequest.Password);

            if (!result.Succeeded)
            {
                var message = result.Errors.First().Description;

                return BadRequest(BasicOperationResult<User>.Fail(message));
            }

            return StatusCode(201);
        }
    }
}
