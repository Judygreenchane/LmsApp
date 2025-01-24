﻿using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ICourseRepository : IRepositoryWithCollections<Course>
    {
        Task<Course?> GetCourseByUserIdAsync(string userId, bool trackChanges = false);
    }
}
