namespace PetStore.Data.Models
{
    public static class ModelValidator
    {
        public const int DescriptionMaxLength = 1000;
        public const int NameMaxLength = 50;

        public static class UserValidator 
        {
            public const int NameMaxLength = 30;
            public const int EmailMaxLength = 40;
        }
    }
}
