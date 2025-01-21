using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IRepositoryWithCollections<T> : IRepositoryBase<T> where T : BaseEntity
    {
        IQueryable<T> FindAll(bool includeCollection = false, bool includeDocuments = false, bool trackChanges = false);
    }
}
