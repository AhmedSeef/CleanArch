using CleanArch.Domain.Commands;
using CleanArch.Domain.Interfaces;
using CleanArch.Domain.Models;
using MediatR;

namespace CleanArch.Domain.CommandHandlers
{
    public class CourseCommandHandler : IRequestHandler<CreateCourseCommand, bool>
    {
        private readonly ICourseRepository _courseRepository;

        public CourseCommandHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public Task<bool> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = new Course()
            {
                Dsecription = request.Dsecription,
                Name = request.Name,
                ImageUrl = request.ImageUrl
            };

            _courseRepository.Add(course);
            return Task.FromResult(true);
        }
    }
}
