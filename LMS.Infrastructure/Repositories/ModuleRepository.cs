﻿using Domain.Contracts;
using Domain.Models.Entities;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Repositories
{
    public class ModuleRepository : RepositoryBase<Module>, IModuleRepository
    {
        public ModuleRepository(LmsContext context) : base(context)
        {
        }

        public IQueryable<Module> FindAll(bool includeActivities = false, bool includeDocuments = false, bool trackChanges = false)
        {
            if (includeActivities && includeDocuments) return base.FindAll(trackChanges).Include(m => m.Activities).Include(m => m.Documents);
            
            if(includeActivities)return base.FindAll(trackChanges).Include(m => m.Activities);

            if(includeDocuments)return base.FindAll(trackChanges).Include(m => m.Documents);

            return base.FindAll(trackChanges);
        }

    }
}
