using AutoMapper;
using Domain.Contracts;
using Domain.Models.Entities;
using LMS.Shared.DTOs.Activity;
using Microsoft.AspNetCore.JsonPatch;
using Services.Contracts;

namespace LMS.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public ActivityService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<bool> AnyAsync()
        {
            return await uow.ActivityRepository.AnyAsync();
        }

        public async Task<bool> AnyAsync(int Id)
        {
            return await uow.ActivityRepository.AnyAsync(Id);
        }

        public async Task<ActivityDto> CreateAsync(ActivityCreateDto dto)
        {
            Activity activity = mapper.Map<Activity>(dto);
            
            uow.ActivityRepository.Create(activity);
            await uow.CompleteAsync();

            return mapper.Map<ActivityDto>(activity);
        }

        public async Task DeleteAsync(int id)
        {
            Activity? activityToDelete = await uow.ActivityRepository.FindByIdAsync(id) 
                ?? throw new KeyNotFoundException($"Activity with id: {id} not found.");
            uow.ActivityRepository.Delete(activityToDelete);

            await uow.CompleteAsync();
        }

        public IEnumerable<ActivityDto> FindAll(bool includeDocuments = false, bool trackChanges = false)
        {
            var activitys = uow.ActivityRepository.FindAll(includeDocuments, trackChanges);
            return mapper.Map<IEnumerable<ActivityDto>>(activitys);
        }

        public async Task<ActivityDto> FindByIdAsync(int Id, bool includeDocuments = false, bool trackChanges = false)
        {
            Activity? activity = await uow.ActivityRepository.FindByIdAsync(Id, includeDocuments, trackChanges);
            return activity == null 
                ? throw new KeyNotFoundException($"activity with id: {Id} not found") : mapper.Map<ActivityDto>(activity);
        }

        public async Task<ActivityDto> FindByIdAsync(int Id)
        {
            Activity? activity = await uow.ActivityRepository.FindByIdAsync(Id);
            return activity == null
                ? throw new KeyNotFoundException($"activity with id: {Id} not found") : mapper.Map<ActivityDto>(activity);
        }

        public async Task<ActivityDto> UpdateAsync(int id, JsonPatchDocument<ActivityUpdateDto> patchDocument)
        {
            var activityToPatch = await uow.ActivityRepository.FindByIdAsync(id) 
                ?? throw new KeyNotFoundException($"{id} not found.");
            var activity = mapper.Map<ActivityUpdateDto>(activityToPatch);
            patchDocument.ApplyTo(activity);

            mapper.Map(activity, activityToPatch);
            await uow.CompleteAsync();

            return mapper.Map<ActivityDto>(activityToPatch);
        }
    }
}
