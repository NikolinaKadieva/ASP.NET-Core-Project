namespace EstateRentingSystem.Data
{
    public class DataConstants
    {
        public class User
        {
            public const int FullNameMinLength = 5;
            public const int FullNameMaxLength = 40;
            public const int PasswordMinLegth = 6;
            public const int PasswordMaxLength = 100;
        }
        public class Estate
        {
            public const int TypeMinLength = 2;
            public const int TypeMaxLength = 20;
            public const int TypeOfConstructionMinLength = 2;
            public const int TypeOfConstructionMaxLength = 30;
            public const int DescriptionMinLength = 10;
            public const int YearOfConstructionMinValue = 1980;
            public const int YearOfConstructionMaxValue = 2050;
            public const int SquaringMinValue = 25;
            public const int SquaringMaxValue = 500;
        }

        public class Category
        {
            public const int NameMaxLength = 25;
        }

        public class Dealer
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 25;
            public const int PhoneNumberMinLength = 6;
            public const int PhoneNumberMaxLength = 30;
        }
        
        
    }
}
