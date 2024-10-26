using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Security.Claims;
using Talabat.API.DTOs.AccountUsersDto;
using Talabat.API.Errors;
using Talabat.API.Helper;
using Talabat.Core.Entities.IdentityEntities;
using Talabat.Core.ServicesInterfaces;

namespace Talabat.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                 IAuthService authService,
                                 IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
            _mapper = mapper;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null) return Unauthorized(new ApiResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (result.Succeeded is false) return Unauthorized(new ApiResponse(401));
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if(CheckRegisteredEmail(model.Email).Result) return BadRequest(new ApiValidationResponse() { Errors=new List<string>(),Msg="This Email is Taken" });
            var user = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumer
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded is false) return BadRequest(new ApiResponse(400));
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        public async Task <ActionResult<UserDto>> GetCurrentLoginUser()
        {
            var email = User.FindFirst(ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return BadRequest(new ApiResponse(400));
            return Ok(new UserDto()
            {
                DisplayName = user?.DisplayName,
                Email = user?.Email,
                Token = await _authService.CreateTokenAsync(user,_userManager)
            });
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("Address")]
        public async Task<ActionResult<Address>> GetUserAddressAsync()
        {
            var user = await _userManager.FindUseIncludesAddressAsync(User);
            return (user.Address);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("updateAddress")]
        public async Task<ActionResult<Address>> UpdateUserAddress(AddressDto addressDto)
        {
            var updatedAddress = _mapper.Map<AddressDto,Address>(addressDto);
            var user = await _userManager.FindUseIncludesAddressAsync(User);

            updatedAddress.Id = user.Address.Id;
            user.Address = updatedAddress;
            var result = await _userManager.UpdateAsync(user);
            return Ok(user.Address);
        }
        private async Task<bool> CheckRegisteredEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email)is not null;
        }
    }
}
