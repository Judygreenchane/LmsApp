using Domain.Models.Entities;

namespace Domain.Contracts
{
    public interface IModuleRepository : IRepositoryBase<Module>
    {
        IQueryable<Module> FindAll(bool includeActivities = false, bool includeDocuments = false, bool trackChanges = false);
    }
}