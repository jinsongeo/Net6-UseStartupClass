namespace Net6_UseStartupClass.Code
{
    public class AppSettings
    {
        public const string Key = "AppSettings";

        public string ApplicationId { get; set; } = string.Empty;
        public string AuthCookieName { get; set; } = string.Empty;
        public string AuthenticationUrl { get; set; } = string.Empty;
    }
}
