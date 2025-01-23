using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Entities;
using LMS.Shared.DTOs.Document;
using Microsoft.AspNetCore.JsonPatch;
using Services.Contracts;

namespace LMS.Presemtation.Controllers
{
    [Route("api/document")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public DocumentController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentDto>> GetDocument(int id)
        {
            if (!await _serviceManager.DocumentService.AnyAsync(id))
            {
                return NotFound($"There is no document with id: {id}");
            }
            var documentDto = await _serviceManager.DocumentService.FindByIdAsync(id);
            return Ok(documentDto);
        }
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<DocumentDto>>> GetDocumentsAsync(bool trackChanges = false)
        {
            if (!await _serviceManager.DocumentService.AnyAsync())
            {
                return NotFound($"There are no documents");
            }
            var documentDtos = _serviceManager.DocumentService.FindAll(trackChanges);
            return Ok(documentDtos);
        }
        [HttpPost()]
        public async Task<ActionResult> CreateDocumentAsync(DocumentCreateDto dto)
        {
            var createdDocumentDto = await _serviceManager.DocumentService.CreateAsync(dto);
            return Created();
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchDocumentAsync(int id, JsonPatchDocument<DocumentUpdateDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("No patch document found");
            }

            if (!await _serviceManager.DocumentService.AnyAsync(id))
            {
                return NotFound($"There is no document with id: {id}");
            }

            var changedDocument = await _serviceManager.DocumentService.UpdateAsync(id, patchDocument);

            return Ok(changedDocument);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDocumentAsync(int id)
        {
            await _serviceManager.DocumentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
