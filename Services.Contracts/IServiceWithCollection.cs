using LMS.Shared.DTOs.BaseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceWithCollection<TDto, TCreateDto, TUpdateDto> : IServiceBase<TDto, TCreateDto, TUpdateDto> where TDto : BaseDto where TCreateDto : BaseCreateDto where TUpdateDto : BaseUpdateDto
    {
        IEnumerable<TDto> FindAll(bool includeCollection = false, bool includeDocuments = false, bool trackChanges = false);
    }
}
