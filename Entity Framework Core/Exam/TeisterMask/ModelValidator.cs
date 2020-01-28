namespace TeisterMask
{
    public static class ModelValidator
    {
        public static class EmployeeValidator 
        {
            public const int UsernameMinLength = 3;
            public const int UsernameMaxLength = 40;

            public const string UsernamePattern = @"^[A-Za-z0-9]+$";

            public const string PhoneNumberPattern = @"^[0-9]{3}\-[0-9]{3}\-[0-9]{4}$";
        }

        public static class ProjectValidator 
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 40;
        }

        public static class TaskValidator 
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 40;
        }
    }
}
