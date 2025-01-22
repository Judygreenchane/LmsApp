using LMS.Infrastructure.Repositories;

namespace Domain.Contracts;

public interface IUnitOfWork
{
    ICourseRepository CourseRepository { get; }
    IModuleRepository ModuleRepository { get; }
    IActivityRepository ActivityRepository { get; }
    IActivityTypeRepository ActivityTypeRepository { get; }
    IDocumentRepository DocumentRepository { get; }
    Task CompleteAsync();
}