using AutoMapper;
using Domain.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services;
public class ServiceManager : IServiceManager
{

    private readonly Lazy<IAuthService> authService;
    private readonly Lazy<ICourseService> courseService;

    public IAuthService AuthService => authService.Value;
    public ICourseService CourseService => courseService.Value;

    public ServiceManager(Lazy<IAuthService> authService, IUnitOfWork uow, IMapper mapper)
    {
        this.authService = authService;
        courseService = new Lazy<ICourseService>(() => new CourseService(uow, mapper));
    }
}