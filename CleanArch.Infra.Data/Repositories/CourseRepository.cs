using CleanArch.Domain.Interfaces;
using CleanArch.Domain.Models;
using CleanArch.Infra.Data.Context;

namespace CleanArch.Infra.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DomainDataContext _context;

        public CourseRepository(DomainDataContext context)
        {
            _context = context;
        }

        public void Add(Course course)
        {
            _context.Add(course);
            _context.SaveChanges();
        }

        public IEnumerable<Course> GetCourses()
        {
            return _context.Courses.ToList();
        }
    }
}
