using Domain.Contracts;
using LMS.Infrastructure.Data;

namespace LMS.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly LmsContext context;
    private readonly Lazy<ICourseRepository> _courseRepository;
    private readonly Lazy<IModuleRepository> _moduleRepository;
    private readonly Lazy<IActivityRepository> _activityRepository;

    public ICourseRepository CourseRepository => _courseRepository.Value;
    public IModuleRepository ModuleRepository => _moduleRepository.Value;
    public IActivityRepository ActivityRepository => _activityRepository.Value;
    public UnitOfWork(LmsContext context)
    {
        this.context = context;
        _courseRepository = new Lazy<ICourseRepository>(() => new CourseRepository(this.context));
        _moduleRepository = new Lazy<IModuleRepository>(() => new ModuleRepository(this.context));
        _activityRepository = new Lazy<IActivityRepository>(() => new ActivityRepository(this.context));
    }

    public async Task CompleteAsync()
    {
        await context.SaveChangesAsync();
    }
}
