namespace P01_StudentSystem.Data
{
    public static class ModelValidator
    {
        //TODO Check Kenov please
        public static class StudentValidator
        {
            public const int NameMaxLength = 100;
        }

        public class CourseValidator
        {
            public const int NameMaxLenght = 80;

            public const int DescriptionMaxLenght = 250;
        }

        public class ResourceValidator
        {
            public const int NameMaxLength = 50;

            public const int UrlMaxLenght = 50;
        }

        public class HomeworkValidator
        {
            public const int ContentMaxLength = 40;
        }
    }
}
