﻿using Domain.Contracts;
using Domain.Models.Entities;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LMS.Infrastructure.Repositories;
public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
{
    protected LmsContext Context { get; }
    protected DbSet<T> DbSet { get; }

    public RepositoryBase(LmsContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    public IQueryable<T> FindAll(bool trackChanges = false) =>
                        trackChanges ? DbSet :
                                       DbSet.AsNoTracking();

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false) =>
                         trackChanges ? DbSet.Where(expression) :
                                        DbSet.Where(expression).AsNoTracking();
    
    public IQueryable<T> FindByName(string name, bool trackChanges = false) =>
                         trackChanges ? DbSet.Where(t => t.Name == name) :
                                        DbSet.Where(t => t.Name == name).AsNoTracking();

    public async Task<T?> FindByIdAsync(int Id) => await DbSet.FirstOrDefaultAsync(t => t.Id == Id);

    public async Task<bool> AnyAsync() => await DbSet.AnyAsync();
    public async Task<bool> AnyAsync(int Id) => await DbSet.AnyAsync(t => t.Id == Id);

    public void Create(T entity) => DbSet.Add(entity);
    public void Delete(T entity) => DbSet.Remove(entity);
    public void Update(T entity) => DbSet.Update(entity);

}
