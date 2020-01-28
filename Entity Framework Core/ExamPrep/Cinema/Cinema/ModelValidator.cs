namespace Cinema
{
    public static class ModelValidator
    {
        public static class MovieValidator 
        {
            public const int TitleMinLength = 3;
            public const int TitleMaxLength = 20;

            public const double RatingMinRange = 1;
            public const double RatingMaxRange = 10;

            public const int DirectorMinLength = 3;
            public const int DirectorMaxLength = 20;
        }

        public static class HallValidator 
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 20;
        }

        public static class CustomerValidator 
        {
            public const int FirstNameMinLength = 3;
            public const int FirstNameMaxLength = 20;

            public const int LastNameMinLength = 3;
            public const int LastNameMaxLength = 20;

            public const int AgeMinRange = 12;
            public const int AgeMaxRange = 110;

            public const double BalanceMinRange = 0.01;
            public const double BalanceMaxRange = 792281625142643;
        }

        public static class TicketValidator 
        {
            public const double PriceMinRange = 0.01;
            public const double PriceMaxRange = 792281625142643;
        }
    }
}
