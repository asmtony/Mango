using static Mango.Web.Utility.ApiStaticUtility;

namespace Mango.Web.Models;

public class RequestDto
{
    public ApiTypes ApiType { get; set; } = ApiTypes.GET;
    public string URL { get; set; } = "";
    public object Data { get; set; }
    public string AccessToken { get; set; } = "";
}
