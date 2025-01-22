using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Entities;
using LMS.Shared.DTOs.Activity;
using Microsoft.AspNetCore.JsonPatch;
using Services.Contracts;

namespace LMS.Presemtation.Controllers
{
    [Route("api/activity")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public ActivityController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityDto>> GetActivity(int id)
        {
            if (!await _serviceManager.ActivityService.AnyAsync(id))
            {
                return NotFound($"There is no activity with id: {id}");
            }
            var courseDto = await _serviceManager.ActivityService.FindByIdAsync(id);
            return Ok(courseDto);
        }
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<ActivityDto>>> GetActivitysAsync()
        {
            if (!await _serviceManager.ActivityService.AnyAsync())
            {
                return NotFound($"There are no activitys");
            }
            var courseDtos = _serviceManager.ActivityService.FindAll();
            return Ok(courseDtos);
        }
        [HttpPost()]
        public async Task<ActionResult> CreateActivityAsync(ActivityCreateDto dto)
        {
            var createdActivityDto = await _serviceManager.ActivityService.CreateAsync(dto);
            return Created();
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchActivityAsync(int id, JsonPatchDocument<ActivityUpdateDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("No patch document found");
            }

            if (!await _serviceManager.ActivityService.AnyAsync(id))
            {
                return NotFound($"There is no activity with id: {id}");
            }

            var changedActivity = await _serviceManager.ActivityService.UpdateAsync(id, patchDocument);

            return Ok(changedActivity);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteActivityAsync(int id)
        {
            await _serviceManager.ActivityService.DeleteAsync(id);
            return NoContent();
        }
    }
}
