using Api.DTOs.CandidateDtos;
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
    public class CandidateController:ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CandidateController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Apply To Job
        [Authorize(Roles = "User")]
        [HttpPost("apply-job")]
        public async Task<IActionResult> ApplyToJob(int jobId)
        {
            var userId = User.GetUserId();
            var candidate = new Candidate
            {
                JobId = jobId,
                UserId = userId
            };
            _unitOfWork.Repository<Candidate, int>().Add(candidate);
            var result = await _unitOfWork.CompleteAsync() > 0;
            if (!result)
                return BadRequest("apply to job failed");
            var spec = new CandidateWithUserAndJobSpecification(candidate.Id);
            var candidateSpec = await _unitOfWork.Repository<Candidate, int>().GetByIdWithSpecAsync(spec);

            return Ok(_mapper.Map<CandidateToReturnDto>(candidateSpec));

        }
        #endregion

        #region Get User Candidates
        [Authorize(Roles = "User")]
        [HttpGet("get-my-applications")]
        public async Task<IActionResult> GetUserApplications()
        {
            var userId = User.GetUserId();
            var spec = new UserApplicationSpecification(userId);
            var applications = await _unitOfWork.Repository<Candidate, int>().GetAllWithSpecAsync(spec);
            if (applications is null || !applications.Any())
                return NotFound("Doesn't Exist Applications");

            return Ok(_mapper.Map<IReadOnlyList<CandidateToReturnDto>>(applications));
        }
        #endregion


        #region Get Job Candidates
        [Authorize(Roles = "Company")]
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
