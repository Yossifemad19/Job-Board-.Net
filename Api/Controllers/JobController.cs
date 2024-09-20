using Api.DTOs.CandidateDtos;
using Api.DTOs.JobDtos;
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
    public class JobController:ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public JobController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region create job
        [Authorize(Roles ="Company")]
        [HttpPost("post")]
        public async Task<IActionResult> PostJobAsync(PostJobDto jobDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var companyId=User.GetUserId();

            var job = _mapper.Map<Job>(jobDto);

            job.CompanyId = companyId;

            _unitOfWork.Repository<Job,int>().Add(job);

            var result = await _unitOfWork.CompleteAsync() > 0;
            if (!result)
                return BadRequest("job creation failed");
            return Ok(_mapper.Map<JobToReturnForCompany>(job));
        }
        #endregion

        #region Get Job
        [Authorize(Roles ="Company")]
        [HttpGet("company/get-job/{id:int}")]
        public async Task<IActionResult> GetJobForCompanyAsync(int id)
        {
            var job = await _unitOfWork.Repository<Job,int>().GetByIdAsync(id);
            if (job == null)
                return NotFound("job not exist");
            var companyId = User.GetUserId();
            if (job.CompanyId != companyId)
                return Unauthorized("you can't access this");
            return Ok(_mapper.Map<JobToReturnForCompany>(job));
        }

        [Authorize(Roles = "User")]
        [HttpGet("user/get-job/{id:int}")]
        public async Task<IActionResult> GetJobForUserAsync(int id)
        {
            var spec = new JobWithCompanyAndCandidatesSpecification(id);
            var job = await _unitOfWork.Repository<Job, int>().GetByIdWithSpecAsync(spec);
            if (job == null)
                return NotFound("job not exist");

            return Ok(_mapper.Map<JobToReturnForUser>(job));
        }




        #endregion

        #region Get All Jobs
        [Authorize]
        [HttpGet("get-all-jobs")]
        public async Task<IActionResult> GetAllJobsAsync()
        {
            var spec = new JobWithCompanyAndCandidatesSpecification();
            var jobs = await _unitOfWork.Repository<Job,int>().GetAllWithSpecAsync(spec);
            if (jobs == null || !jobs.Any())
                return NotFound("not exist jobs");

            return Ok(_mapper.Map<IReadOnlyList<JobToReturnForUser>>(jobs));
        }

        [Authorize(Roles ="Company")]
        [HttpGet("get-my-jobs")]
        public async Task<IActionResult> GetCompanyJobsAsync()
        {
            var companyId =User.GetUserId();

            var spec = new CompanyJobsSpecification(companyId);
            var company = await _unitOfWork.Repository<Company, string>().GetByIdWithSpecAsync(spec);
            if (company is null)
                return NotFound("not exist jobs");
            return Ok(_mapper.Map<IReadOnlyList<JobToReturnForCompany>>(company.Jobs));

        }
        #endregion

        #region Apply To Job
        [Authorize(Roles ="User")]
        [HttpPost("apply")]
        public async Task<IActionResult> ApplyToJob(int jobId)
        {
            var userId = User.GetUserId();
            var candidate = new Candidate
            {
                JobId = jobId,
                UserId = userId
            };
            _unitOfWork.Repository<Candidate,int>().Add(candidate);
            var result = await _unitOfWork.CompleteAsync() > 0;
            if (!result)
                return BadRequest("apply to job failed");
            var spec = new CandidateWithUserAndJobSpecification(candidate.Id);
            var candidateSpec = await _unitOfWork.Repository<Candidate, int>().GetByIdWithSpecAsync(spec);

            return Ok(_mapper.Map<CandidateToReturnDto>(candidateSpec));

        }
        #endregion

    }
}
