namespace Store.BusinessLogicLayer.Common.OptionsModel
{
    public class JwtOptionsModel
    {
        public string SigningSecurityKey { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public bool ValidateIssuer { get; set; }
        public string ValidIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public string ValidAudience { get; set; }
        public bool ValidateLifetime { get; set; }
    }
}
