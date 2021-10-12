using AutoMapper;
using Core.Models;
using Webchat.Models;

namespace Webchat.MapperProfiles
{
    /// <summary>
    /// Represents the configuration used for mapping a <see cref="SignUpRequest"/> object and a <see cref="User"/>
    /// </summary>
    public sealed class UserMapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UserMapperProfile"/>.
        /// </summary>
        public UserMapperProfile()
        {
            CreateMap<SignUpRequest, User>();
        }
    }
}
