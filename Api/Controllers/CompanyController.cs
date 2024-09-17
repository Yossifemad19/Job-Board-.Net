using Api.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CompanyController:ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public CompanyController(IAuthenticationService authService,ITokenService tokenService ,IMapper mapper)
        {
            _authService = authService;
            _tokenService = tokenService;
            _mapper = mapper;
        }


        #region register region

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsCompany(CompanyDto companyDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(await _authService.DoesCompanyExist(companyDto.Email))
                return BadRequest("Email is exist");

            var company = _mapper.Map<Company>(companyDto);
            var result =await _authService.RegisterAsCompany(company,companyDto.Password);

            if (!result)
                return BadRequest("Registeration failed");

            var token = _tokenService.GenerateToken<Company>(company, "Company");
            var companyToReturn= _mapper.Map<CompanyToReturn>(company);
            companyToReturn.Token = token;
            return Ok(companyToReturn);
        }

        
        #endregion

        #region Login Region

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsCompany(LoginDto loginDto)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            var company = await _authService.LoginAsCompany(loginDto.Email, loginDto.Password);

            if (company is null)
                return BadRequest("email or password is wrong");

            var token =  _tokenService.GenerateToken<Company>(company, "Company");

            var companyToReturn = _mapper.Map<CompanyToReturn>(company);
            companyToReturn.Token=token;
            return Ok(companyToReturn);

        }

        
        #endregion

        #region check if account exist
        [HttpGet("IsExist")]
        public async Task<IActionResult> DoesCompanyExist(string Email)
        {
            return Ok(await _authService.DoesCompanyExist(Email));
        }

        
        #endregion
    }

}
