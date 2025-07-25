namespace Mago.Services.AuthAPI.Models;

public class JwtOptions
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public int TokenExpirationInMinutes { get; set; } = 60;
    public JwtOptions() { }
    public JwtOptions(string issuer, string audience, string secretKey, int expirationInMinutes)
    {
        Issuer = issuer;
        Audience = audience;
        SecretKey = secretKey;
        TokenExpirationInMinutes = expirationInMinutes;
    }
}
