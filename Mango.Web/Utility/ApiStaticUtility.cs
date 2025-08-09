namespace Mango.Web.Utility;

public class ApiStaticUtility
{
    public static string HttpCouponName = "MangoCoupon";
    public static string HttpProductName = "MangoProduct";
    public static string HttpAuthtName = "MangoAuth";

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

    public enum TempDataTypes
    {
        Success,
        Error,
        Warning
    }
}
