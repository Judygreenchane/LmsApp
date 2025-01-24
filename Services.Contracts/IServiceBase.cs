using Domain.Models.Entities;
using LMS.Shared.DTOs.BaseDtos;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceBase<TDto, TCreateDto, TUpdateDto> where TDto : BaseDto where TCreateDto : BaseCreateDto where TUpdateDto : BaseUpdateDto
    {
        Task<TDto> FindByIdAsync(int id);
        Task<bool> AnyAsync();
        Task<bool> AnyAsync(int Id);
        Task<TDto> CreateAsync(TCreateDto dto);
        Task<TDto> UpdateAsync(int id, JsonPatchDocument<TUpdateDto> patchDocument);
        Task<TDto> PutAsync(int id, TUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
