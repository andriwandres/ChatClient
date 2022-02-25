namespace Core.Domain.Options
{
    public class JwtOptions
    {
        public const string ConfigurationKey = "JsonWebToken";
        public string Secret { get; set; }
    }
}
