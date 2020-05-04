namespace Store.BusinessLogicLayer.Common.Constants.Jwt
{
    public partial class Constants
    {
        public class JwtConstants
        {
            public const string SigningSecurityKey = "JwtConfig:SigningSecurityKey";
            public const string JwtConfig = "JwtConfig";
            public const string ValidateIssuerSigningKey = "JwtConfig:ValidateIssuerSigningKey";
            public const string ValidateIssuer = "JwtConfig:ValidateIssuer";
            public const string JwtSchemeName = "JwtBearer";
            public const string ValidIssuer = "JwtConfig:ValidIssuer";
            public const string ValidAudience = "JwtConfig:ValidAudience";
            public const string ValidateAudience = "JwtConfig:ValidateAudience";
            public const string ValidateLifetime = "JwtConfig:ValidateLifetime";
            public const string AccessToken = "AccessToken";
            public const string RefreshToken = "refreshtoken";

        }
    }
}
