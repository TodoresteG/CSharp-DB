namespace ProductShop.Models
{
    public static class ModelValidator
    {
        public static class UserValidator 
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 25;
        }

        public static class ProductValidator 
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 150;
        }

        public static class CategoryValidator 
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 15;
        }
    }
}
