namespace Store.BusinessLogicLayer.Common.Constants.PasswordGenerator
{
    public partial class Constants
    {
        public class PasswordGeneration
        {
            public const string PasswordConfig = "PasswordConfig";
            public const string RequiredLength = "RequiredLength";
            public const string RequiredUniqueChars = "RequiredUniqueChars";
            public const string RequireDigit = "RequireDigit";
            public const string RequireLowercase = "RequireLowercase";
            public const string RequireNonAlphanumeric = "RequireNonAlphanumeric";
            public const string RequireUppercase = "RequireUppercase";
            public static readonly string[] AllChars = { "ABCDEFGHJKLMNOPQRSTUVWXYZ", "abcdefghijkmnopqrstuvwxyz", "0123456789", "!@$?_-" };
        }
    }
}
