using AutoMapper;
using Domain.Contracts;
using Domain.Models.Entities;
using LMS.Shared.DTOs.Course;
using LMS.Shared.DTOs.Document;
using Microsoft.AspNetCore.JsonPatch;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public DocumentService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<DocumentDto> CreateAsync(DocumentCreateDto dto)
        {
            Document document = mapper.Map<Document>(dto);

            uow.DocumentRepository.Create(document);
            await uow.CompleteAsync();

            return mapper.Map<DocumentDto>(document);
        }

        public async Task DeleteAsync(int id)
        {
            Document? documentToDelete = await uow.DocumentRepository.FindByIdAsync(id)
                ?? throw new KeyNotFoundException($"Document with id: {id} not found.");
            uow.DocumentRepository.Delete(documentToDelete);

            await uow.CompleteAsync();
        }

        public IEnumerable<DocumentDto> FindAll(bool trackChanges = false)
        {
            var documents = uow.DocumentRepository.FindAll(trackChanges);
            return mapper.Map<IEnumerable<DocumentDto>>(documents);
        }

        public async Task<DocumentDto> FindByIdAsync(int Id)
        {
            Document? document = await uow.DocumentRepository.FindByIdAsync(Id);
            return document == null 
                ? throw new KeyNotFoundException($"document with id: {Id} not found") : mapper.Map<DocumentDto>(document);
        }

        public async Task<DocumentDto> UpdateAsync(int id, JsonPatchDocument<DocumentUpdateDto> patchDocument)
        {
            var documentToPatch = await uow.DocumentRepository.FindByIdAsync(id) 
                ?? throw new KeyNotFoundException($"{id} not found.");
            var document = mapper.Map<DocumentUpdateDto>(documentToPatch);
            patchDocument.ApplyTo(document);

            mapper.Map(document, documentToPatch);
            await uow.CompleteAsync();

            return mapper.Map<DocumentDto>(documentToPatch);
        }
    }
}
