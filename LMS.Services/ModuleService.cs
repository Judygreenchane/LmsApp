using AutoMapper;
using Domain.Contracts;
using Domain.Models.Entities;
using LMS.Shared.DTOs.Course;
using LMS.Shared.DTOs.Module;
using Microsoft.AspNetCore.JsonPatch;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services
{
    public class ModuleService : IModuleService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public ModuleService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<bool> AnyAsync()
        {
            return await uow.ModuleRepository.AnyAsync();
        }

        public async Task<bool> AnyAsync(int Id)
        {
            return await uow.ModuleRepository.AnyAsync(Id);
        }

        public async Task<ModuleDto> CreateAsync(ModuleCreateDto dto)
        {
            Module module = mapper.Map<Module>(dto);

            uow.ModuleRepository.Create(module);
            await uow.CompleteAsync();

            return mapper.Map<ModuleDto>(module);
        }

        public async Task DeleteAsync(int id)
        {
            Module? moduleToDelete = await uow.ModuleRepository.FindByIdAsync(id)
                ?? throw new KeyNotFoundException($"Module with id: {id} not found.");
            uow.ModuleRepository.Delete(moduleToDelete);

            await uow.CompleteAsync();
        }

        public IEnumerable<ModuleDto> FindAll(bool includeActivities = false, bool includeDocuments = false, bool trackChanges = false)
        {
            var modules = uow.ModuleRepository.FindAll(includeActivities, includeDocuments, trackChanges);
            return mapper.Map<IEnumerable<ModuleDto>>(modules);
        }

        public async Task<ModuleDto> FindByIdAsync(int Id)
        {
            Module? module = await uow.ModuleRepository.FindByIdAsync(Id);
            return module == null 
                ? throw new KeyNotFoundException($"module with id: {Id} not found") : mapper.Map<ModuleDto>(module);
        }

        public async Task<ModuleDto> UpdateAsync(int id, JsonPatchDocument<ModuleUpdateDto> patchDocument)
        {
            var moduleToPatch = await uow.ModuleRepository.FindByIdAsync(id) 
                ?? throw new KeyNotFoundException($"{id} not found.");
            var module = mapper.Map<ModuleUpdateDto>(moduleToPatch);
            patchDocument.ApplyTo(module);

            mapper.Map(module, moduleToPatch);
            await uow.CompleteAsync();

            return mapper.Map<ModuleDto>(moduleToPatch);
        }
    }
}
