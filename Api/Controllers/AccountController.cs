using Api.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AccountController:ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(IAuthenticationService authService,ITokenService tokenService ,IMapper mapper)
        {
            _authService = authService;
            _tokenService = tokenService;
            _mapper = mapper;
        }


        #region register region

        [HttpPost("company-register")]
        public async Task<IActionResult> RegisterAsCompany(CompanyDto companyDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var company = _mapper.Map<Company>(companyDto);
            var result =await _authService.RegisterAsCompany(company,companyDto.Password);

            if (!result)
                return BadRequest("Registeration failed");

            var token = _tokenService.GenerateToken<Company>(company, "Company");

            return Ok(token);
        }

        #endregion
    }
}
