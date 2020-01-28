namespace SoftJail
{
    public static class ModelValidator
    {
        public static class PrisonerValidator 
        {
            public const int FullNameMinLength = 3;
            public const int FullNameMaxLength = 20;

            public const string NicknamePattern = @"^The\ [A-Z][a-z]+$";

            public const int AgeMinRange = 18;
            public const int AgeMaxRange = 65;

            public const string BailMinRange = "0.00";
            public const string BailMaxRange = "79228162514264337593543950335";
        }

        public static class OfficerValidator 
        {
            public const int FullNameMinLength = 3;
            public const int FullNameMaxLength = 30;

            public const string SalaryMinRange = "0.00";
            public const string SalaryMaxRange = "79228162514264337593543950335";
        }

        public static class CellValidator 
        {
            public const int CellNumberMinRange = 1;
            public const int CellNumberMaxRange = 1000;
        }

        public static class MailValidator 
        {
            public const string AddressPattern = @"^[A-Za-z0-9\ ]+\ str\.$";
        }

        public static class DepartmentValidator 
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 25;
        }
    }
}
