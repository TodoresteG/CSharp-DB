using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub
{
    public static class ModelValidator
    {
        public static class SongValidator 
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 20;

            public const string PriceMinRange = "0.00";
            public const string PriceMaxRange = "79228162514264337593543950335";
        }

        public static class AlbumValidator 
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 40;
        }

        public static class PerformerValidator 
        {
            public const int FirstNameMinLength = 3;
            public const int FirstNameMaxLength = 20;

            public const int LastNameMinLength = 3;
            public const int LastNameMaxLength = 20;

            public const int AgeMinRange = 18;
            public const int AgeMaxRange = 70;

            public const string NetWorthMinRange = "0.00";
            public const string NetWorthMaxRange = "79228162514264337593543950335";
        }

        public static class ProducerValidator 
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 30;

            public const string PseudonymPattern = @"^[A-Z][a-z]+\ [A-Z][a-z]+$";
            public const string PhoneNumberPattern = @"^\+359\ [0-9]{3}\ [0-9]{3}\ [0-9]{3}$";
        }

        public static class WriterValidator 
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 20;

            public const string PseudonymPattern = @"^[A-Z][a-z]+\ [A-Z][a-z]+$";
        }
    }
}
