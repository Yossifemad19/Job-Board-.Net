using Api.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController:ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UserController(IAuthenticationService authService, ITokenService tokenService, IMapper mapper)
        {
            _authService = authService;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        #region Register
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsUser([FromForm] UserResiterDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _authService.DoesUserExist(userDto.Email))
                return BadRequest("Email is already exist");
            var user = _mapper.Map<AppUser>(userDto);

            if (userDto.Resume != null)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CVs");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }
                var fileName = $"{userDto.Email.Split("@")[0]}_Cv{Path.GetExtension(userDto.Resume.FileName)}";
                var filePath = Path.Combine(uploadsFolderPath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await userDto.Resume.CopyToAsync(fileStream);
                }
                user.ResumeUrl = $"CVs/{fileName}";
            }

            var result = await _authService.RegisterAsUser(user, userDto.Password);

            if (!result)
                return BadRequest("Registeration failed");

            var token = _tokenService.GenerateToken<AppUser>(user, "User");
            var userToReturn = _mapper.Map<UserToReturn>(user);
            userToReturn.Token = token;
            return Ok(userToReturn);
        }

        #endregion

        #region Login
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsUser(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _authService.LoginAsUser(loginDto.Email, loginDto.Password);

            if (user is null)
                return BadRequest("email or password is wrong");

            var token = _tokenService.GenerateToken<AppUser>(user, "User");

            var userToReturn = _mapper.Map<UserToReturn>(user);
            userToReturn.Token = token;
            return Ok(userToReturn);

        }
        #endregion

        #region Check if user eist
        [HttpGet("IsExist")]
        public async Task<IActionResult> DoesUserExist(string Email)
        {
            return Ok(await _authService.DoesUserExist(Email));
        }
        #endregion
    }
}
