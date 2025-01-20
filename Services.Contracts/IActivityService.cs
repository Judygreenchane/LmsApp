using LMS.Shared.DTOs.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IActivityService : IServiceBase<ActivityDto, ActivityCreateDto, ActivityUpdateDto>
    {
        IEnumerable<ActivityDto> FindAll(bool includeDocuments = false, bool trackChanges = false);
    }
}
