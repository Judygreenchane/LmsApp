using LMS.Shared.DTOs.Module;
using Microsoft.AspNetCore.JsonPatch;

namespace Services.Contracts
{
    public interface IModuleService : IServiceWithCollection<ModuleDto, ModuleCreateDto, ModuleUpdateDto>
    {
    }
}