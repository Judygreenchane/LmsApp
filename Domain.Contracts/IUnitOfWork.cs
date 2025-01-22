using LMS.Infrastructure.Repositories;

namespace Domain.Contracts;

public interface IUnitOfWork
{
    ICourseRepository CourseRepository { get; }
    IModuleRepository ModuleRepository { get; }
    IActivityRepository ActivityRepository { get; }
    IDocumentRepository DocumentRepository { get; }
    Task CompleteAsync();
}