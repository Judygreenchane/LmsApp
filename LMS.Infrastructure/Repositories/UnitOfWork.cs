using Domain.Contracts;
using LMS.Infrastructure.Data;

namespace LMS.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly LmsContext context;
    private readonly Lazy<ICourseRepository> _courseRepository;
    private readonly Lazy<IModuleRepository> _moduleRepository;
    private readonly Lazy<IActivityRepository> _activityRepository;
    private readonly Lazy<IActivityTypeRepository> _activityTypeRepository;
    private readonly Lazy<IDocumentRepository> _documentRepository;

    public ICourseRepository CourseRepository => _courseRepository.Value;
    public IModuleRepository ModuleRepository => _moduleRepository.Value;
    public IActivityRepository ActivityRepository => _activityRepository.Value;
    public IActivityTypeRepository ActivityTypeRepository => _activityTypeRepository.Value;
    public IDocumentRepository DocumentRepository => _documentRepository.Value;
    public UnitOfWork(LmsContext context)
    {
        this.context = context;
        _courseRepository = new Lazy<ICourseRepository>(() => new CourseRepository(this.context));
        _moduleRepository = new Lazy<IModuleRepository>(() => new ModuleRepository(this.context));
        _activityRepository = new Lazy<IActivityRepository>(() => new ActivityRepository(this.context));
        _activityTypeRepository = new Lazy<IActivityTypeRepository>(() => new ActivityTypeRepository(this.context));
        _documentRepository = new Lazy<IDocumentRepository>(() => new DocumentRepository(this.context));
    }

    public async Task CompleteAsync()
    {
        await context.SaveChangesAsync();
    }
}
