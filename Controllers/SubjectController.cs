﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SigmaBackend.Models;
using SigmaBackend.Services;

namespace SigmaBackend.Controllers
{
    [EnableCors("CorsRules")]
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : Controller
    {
        private readonly ISubjectService _subjectService;
        public readonly SigmaContext _sigmaContext;

        public SubjectController(SigmaContext sigmaContext, ISubjectService subjectService)
        {
            _sigmaContext = sigmaContext;
            _subjectService = subjectService;
        }

        [HttpGet("GetSubjecttInfo/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var subject = await _subjectService.Get(id);
            if (subject == null)
            {
                return NotFound("Info not found");
            }

            try
            {
                return Ok(subject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("SaveSubjectInfo")]
        public async Task<IActionResult> Save([FromBody] Subject subject)
        {
            try
            {
                await _subjectService.Save(subject);
                return Ok("Info saved!");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateSubjectInfo/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Subject subject)
        {
            var proof = await _subjectService.Update(id, subject); ;
            try
            {
                if (proof == true)
                {
                    return Ok("Info saved!");
                }
                else
                {
                    return NotFound("Info not found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSubjectInfo/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var proof = await _subjectService.Delete(id);
            try
            {
                if (proof == true)
                {
                    return Ok("Info Deleted!");
                }
                else
                {
                    return NotFound("Info not found");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }


}
