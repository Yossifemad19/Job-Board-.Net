using Api.DTOs;
using Api.DTOs.CandidateDtos;
using Api.DTOs.CompanyDtos;
using Api.Extensions;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CompanyController:ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationService _authService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public CompanyController(IUnitOfWork unitOfWork,IAuthenticationService authService,ITokenService tokenService ,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
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

        #region Get Companies
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<GetCompanyDto>>> GetAllCompanies()
        {
            var companies=await _unitOfWork.CompanyRepository.GetAllAsync();
            if (companies is null || companies.Any())
                return NotFound("not exist companies");
            return Ok(_mapper.Map<IEnumerable<GetCompanyDto>>(companies));
        }
        #endregion

        #region Get Job Candidates
        [Authorize(Roles ="Company")]
        [HttpGet("get-job-candidates")]
        public async Task<IActionResult> GetJobCandidates(int jobId)
        {
            var companyId = User.GetUserId();
            var spec = new CompanyJobCandidatesSpecification(companyId, jobId);
            var jobCandidates = await _unitOfWork.Repository<Candidate, int>().GetAllWithSpecAsync(spec);
            if (jobCandidates is null || !jobCandidates.Any())
                return NotFound("not exist candidates");
            return Ok(_mapper.Map<IReadOnlyList<CandidateToReturnDto>>(jobCandidates));
        }

        #endregion
    }

}
