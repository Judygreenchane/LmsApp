using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Entities;
using LMS.Shared.DTOs.Module;
using Microsoft.AspNetCore.JsonPatch;
using Services.Contracts;

namespace LMS.Presemtation.Controllers
{
    [Route("api/module")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public ModuleController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ModuleDto>> GetModule(int id, bool includeActivities = false, bool includeDocuments = false, bool trackChanges = false)
        {
            if (!await _serviceManager.ModuleService.AnyAsync(id))
            {
                return NotFound($"There is no module with id: {id}");
            }
            var courseDto = await _serviceManager.ModuleService.FindByIdAsync(id, includeActivities, includeDocuments, trackChanges);
            return Ok(courseDto);
        }
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<ModuleDto>>> GetModulesAsync(bool includeActivities = false, bool includeDocuments = false, bool trackChanges = false)
        {
            if (!await _serviceManager.ModuleService.AnyAsync())
            {
                return NotFound($"There are no modules");
            }
            var courseDtos = _serviceManager.ModuleService.FindAll(includeActivities, includeDocuments, trackChanges);
            return Ok(courseDtos);
        }
        [HttpPost()]
        public async Task<ActionResult> CreateModuleAsync(ModuleCreateDto dto)
        {
            var createdModuleDto = await _serviceManager.ModuleService.CreateAsync(dto);
            return Created();
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchModuleAsync(int id, JsonPatchDocument<ModuleUpdateDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("No patch document found");
            }

            if (!await _serviceManager.ModuleService.AnyAsync(id))
            {
                return NotFound($"There is no module with id: {id}");
            }

            var changedModule = await _serviceManager.ModuleService.UpdateAsync(id, patchDocument);

            return Ok(changedModule);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutModuleAsync(int id, ModuleUpdateDto dto)
        {
            if (dto == null)
            {
                return BadRequest("No Input found");
            }

            if (!await _serviceManager.ModuleService.AnyAsync(id))
            {
                return NotFound($"There is no module with id: {id}");
            }

            var changedModule = await _serviceManager.ModuleService.PutAsync(id, dto);

            return Ok(changedModule);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteModuleAsync(int id)
        {
            await _serviceManager.ModuleService.DeleteAsync(id);
            return NoContent();
        }
    }
}
