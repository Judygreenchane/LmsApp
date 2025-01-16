using Domain.Contracts;
using LMS.Infrastructure.Data;

namespace LMS.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly LmsContext context;
    private readonly Lazy<ICourseRepository> _courseRepository;

    public ICourseRepository CourseRepository => _courseRepository.Value;
    public UnitOfWork(LmsContext context)
    {
        this.context = context;
        _courseRepository = new Lazy<ICourseRepository>(() => new CourseRepository(this.context));
    }

    public async Task CompleteAsync()
    {
        await context.SaveChangesAsync();
    }
}
