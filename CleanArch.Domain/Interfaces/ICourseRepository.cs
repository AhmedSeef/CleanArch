using CleanArch.Domain.Models;

namespace CleanArch.Domain.Interfaces
{
    public interface ICourseRepository
    {
        void Add(Course course);
        IEnumerable<Course> GetCourses();
    }
}
