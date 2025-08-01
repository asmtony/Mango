namespace Mango.Web.Utility;

public class ApiStaticUtility
{
    public const string RoleAdmin = "ADMIN";
    public const string RoleCustomer = "CUSTOMER";
    public const string TokenCookie = "JwtToken";
    public enum ApiTypes
    {
        GET,
        POST,
        PUT,
        DELETE
    }
}
