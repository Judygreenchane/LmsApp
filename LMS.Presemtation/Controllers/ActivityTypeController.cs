using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Entities;
using LMS.Shared.DTOs.ActivityType;
using Microsoft.AspNetCore.JsonPatch;
using Services.Contracts;

namespace LMS.Presemtation.Controllers
{
    [Route("api/activitytype")]
    [ApiController]
    public class ActivityTypeController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public ActivityTypeController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityTypeDto>> GetActivityType(int id, bool includeActivities = false)
        {
            if (!await _serviceManager.ActivityTypeService.AnyAsync(id))
            {
                return NotFound($"There is no module with id: {id}");
            }
            var courseDto = await _serviceManager.ActivityTypeService.FindByIdAsync(id, includeActivities);
            return Ok(courseDto);
        }
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<ActivityTypeDto>>> GetActivityTypesAsync(bool includeActivities = false)
        {
            if (!await _serviceManager.ActivityTypeService.AnyAsync())
            {
                return NotFound($"There are no modules");
            }
            var courseDtos = _serviceManager.ActivityTypeService.FindAll(includeActivities);
            return Ok(courseDtos);
        }
        [HttpPost()]
        public async Task<ActionResult> CreateActivityTypeAsync(ActivityTypeCreateDto dto)
        {
            var createdActivityTypeDto = await _serviceManager.ActivityTypeService.CreateAsync(dto);
            return Created();
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchActivityTypeAsync(int id, JsonPatchDocument<ActivityTypeUpdateDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("No patch document found");
            }

            if (!await _serviceManager.ActivityTypeService.AnyAsync(id))
            {
                return NotFound($"There is no module with id: {id}");
            }

            var changedActivityType = await _serviceManager.ActivityTypeService.UpdateAsync(id, patchDocument);

            return Ok(changedActivityType);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteActivityTypeAsync(int id)
        {
            await _serviceManager.ActivityTypeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
