namespace CleanArch.Domain.Commands
{
    public class CreateCourseCommand : CourseCommand
    {
        public CreateCourseCommand(string name,string dsecription, string imageUrl)
        {
            base.Name = name;
            base.Dsecription = dsecription;
            base.ImageUrl = imageUrl;
        }
    }
}
