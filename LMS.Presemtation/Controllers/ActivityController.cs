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
        public async Task<ActionResult<ActivityDto>> GetActivity(int id, bool includeDocuments = false, bool trackChanges = false)
        {
            if (!await _serviceManager.ActivityService.AnyAsync(id))
            {
                return NotFound($"There is no activity with id: {id}");
            }
            var activityDto = await _serviceManager.ActivityService.FindByIdAsync(id, includeDocuments, trackChanges);
            return Ok(activityDto);
        }
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<ActivityDto>>> GetActivitiesAsync(bool includeDocuments = false, bool trackChanges = false)
        {
            if (!await _serviceManager.ActivityService.AnyAsync())
            {
                return NotFound($"There are no activitys");
            }
            var activityDtos = _serviceManager.ActivityService.FindAll(includeDocuments, trackChanges);
            return Ok(activityDtos);
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

        [HttpPut("{id}")]
        public async Task<ActionResult> PutActivityAsync(int id, ActivityUpdateDto dto)
        {
            if (dto == null)
            {
                return BadRequest("No Input found");
            }

            if (!await _serviceManager.ActivityService.AnyAsync(id))
            {
                return NotFound($"There is no activity with id: {id}");
            }

            var changedActivity = await _serviceManager.ActivityService.PutAsync(id, dto);

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
