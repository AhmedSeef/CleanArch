using CleanArch.Application.Interfaces;
using CleanArch.Application.ViewModels;
using CleanArch.Domain.Commands;
using CleanArch.Domain.Core.Bus;
using CleanArch.Domain.Interfaces;
using MediatR;

namespace CleanArch.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMediatorHandler _mediator;

        public CourseService(IMediatorHandler mediator)
        {
            _mediator = mediator;
        }

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public void CretaeCourse(CourseViewModel course)
        {
            var courseCommand = new CreateCourseCommand(
                course.Name,
                course.Dsecription,
                course.ImageUrl
                );
            _mediator.SendCommand(courseCommand);
        }

        public CourseViewModel GetCourses()
        {
            return new CourseViewModel()
            {
                Courses = _courseRepository.GetCourses(),
            };
        }
    }
}
