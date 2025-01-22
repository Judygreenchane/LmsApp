using AutoMapper;
using Domain.Contracts;
using Domain.Models.Entities;
using LMS.Shared.DTOs.ActivityType;
using Microsoft.AspNetCore.JsonPatch;
using Services.Contracts;

namespace LMS.Services
{
    public class ActivityTypeService : IActivityTypeService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public ActivityTypeService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<bool> AnyAsync()
        {
            return await uow.ActivityTypeRepository.AnyAsync();
        }

        public async Task<bool> AnyAsync(int Id)
        {
            return await uow.ActivityTypeRepository.AnyAsync(Id);
        }

        public async Task<ActivityTypeDto> CreateAsync(ActivityTypeCreateDto dto)
        {
            ActivityType activityType = mapper.Map<ActivityType>(dto);
            
            uow.ActivityTypeRepository.Create(activityType);
            await uow.CompleteAsync();

            return mapper.Map<ActivityTypeDto>(activityType);
        }

        public async Task DeleteAsync(int id)
        {
            ActivityType? activityTypeToDelete = await uow.ActivityTypeRepository.FindByIdAsync(id) 
                ?? throw new KeyNotFoundException($"ActivityType with id: {id} not found.");
            uow.ActivityTypeRepository.Delete(activityTypeToDelete);

            await uow.CompleteAsync();
        }

        public IEnumerable<ActivityTypeDto> FindAll(bool trackChanges = false)
        {
            var activityTypes = uow.ActivityTypeRepository.FindAll(trackChanges);
            return mapper.Map<IEnumerable<ActivityTypeDto>>(activityTypes);
        }

        public async Task<ActivityTypeDto> FindByIdAsync(int Id)
        {
            ActivityType? activityType = await uow.ActivityTypeRepository.FindByIdAsync(Id);
            return activityType == null 
                ? throw new KeyNotFoundException($"activityType with id: {Id} not found") : mapper.Map<ActivityTypeDto>(activityType);
        }

        public async Task<ActivityTypeDto> UpdateAsync(int id, JsonPatchDocument<ActivityTypeUpdateDto> patchDocument)
        {
            var activityTypeToPatch = await uow.ActivityTypeRepository.FindByIdAsync(id) 
                ?? throw new KeyNotFoundException($"{id} not found.");
            var activityType = mapper.Map<ActivityTypeUpdateDto>(activityTypeToPatch);
            patchDocument.ApplyTo(activityType);

            mapper.Map(activityType, activityTypeToPatch);
            await uow.CompleteAsync();

            return mapper.Map<ActivityTypeDto>(activityTypeToPatch);
        }
    }
}
