namespace P03_FootballBetting.Data
{
    public static class ModelValidator
    {
        public static class TeamValidator 
        {
            public const int NameMaxLength = 20;

            public const int LogoUrlMaxLength = 40;

            public const int InitialsMaxLength = 5;
        }

        public static class ColorValidator 
        {
            public const int NameMaxLength = 15;
        }

        public static class TownValidator 
        {
            public const int NameMaxLength = 25;
        }

        public static class CountryValidator 
        {
            public const int NameMaxLength = 30;
        }

        public static class PlayerValidator 
        {
            public const int NameMaxLength = 30;
        }

        public static class PositionValidator 
        {
            public const int NameMaxLength = 15;
        }

        public static class BetValidator 
        {
            public const int PredictionMaxLength = 50;
        }

        public static class UserValidator 
        {
            public const int UsernameMaxLength = 30;

            public const int PasswordMaxLength = 16;

            public const int EmailMaxLength = 25;

            public const int NameMaxLength = 25;
        }
    }
}
