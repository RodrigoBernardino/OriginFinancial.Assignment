#pragma warning disable 1591
using Microsoft.AspNetCore.Mvc;
using ScoreCalculationEngine.Application.DTOs;
using ScoreCalculationEngine.Application.Services;
using ScoreCalculationEngine.Domain.Models;

namespace ScoreCalculationEngine.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScoreCalculationController : ControllerBase
    {
        private readonly ScoreCalculationService _scoreCalculationService;

        public ScoreCalculationController(ScoreCalculationService scoreCalculationService)
        {
            _scoreCalculationService = scoreCalculationService;
        }

        /// <summary>Calculate the Risk Profile.</summary>
        /// <response code="200">The risk profile.</response>
        /// <response code="400">Bad request: Invalid personal information.</response>
        /// <response code="401">Unauthorized: Invalid Api Key.</response>
        /// <param name="personalInfo">The personal information object.</param>
        /// <returns><see cref="IActionResult" />.</returns>
        [HttpPost("getRiskProfile")]
        public IActionResult GetRiskProfile(PersonalInformation personalInfo)
        {
            try
            {
                RiskProfile riskProfile = _scoreCalculationService
                    .CalculateScore(personalInfo);

                return Ok(riskProfile);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.GetBaseException().Message);
            }
        }
    }
}
