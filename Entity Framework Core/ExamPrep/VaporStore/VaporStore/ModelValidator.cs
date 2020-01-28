namespace VaporStore
{
    public static class ModelValidator
    {
        public static class GameValidator 
        {
            public const string PriceMinRange = "0.00";
            public const string PriceMaxRange = "79228162514264337593543950335";
        }

        public static class UserValidator 
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 20;

            public const string FullNamePattern = @"^[A-Z][a-z]+\ [A-Z][a-z]+$";

            public const int AgeMinRange = 3;
            public const int AgeMaxRange = 103;
        }

        public static class CardValidator 
        {
            public const string NumberPattern = @"^[0-9]{4}\ [0-9]{4}\ [0-9]{4}\ [0-9]{4}$";
            public const string CvcPattern = @"^[0-9]{3}$";
        }

        public static class PurchaseValidator 
        {
            public const string ProductKeyPattern = @"^[A-Z0-9]{4}\-[A-Z0-9]{4}\-[A-Z0-9]{4}$";
        }
    }
}
