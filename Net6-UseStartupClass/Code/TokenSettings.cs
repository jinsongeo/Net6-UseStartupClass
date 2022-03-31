namespace Net6_UseStartupClass.Code
{
    public class TokenSettings
    {
        public const string Key = "TokenSettings";
        public bool ValidateIssuerSigningKey { get; set; } = true;
        
        public string IssuerSigningKey { get; set; } = String.Empty;

        public bool ValidateIssuer { get; set; } = false;

        public string ValidIssuer { get; set; } = String.Empty;

        public bool ValidateAudience { get; set; } = true;

        public string ValidAudience { get; set; } = String.Empty;

        public bool RequireExpirationTime { get; set; } = true;

        public bool ValidateLifetime { get; set; } = true;

    }
}
