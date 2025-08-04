using Mango.Web.Models;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Web.Controllers
{
    //[Route("api/[controller]")]
    // [ApiController]
    public class BaseController : Controller
    {
        protected void SetTempDataMessage(ResponseDto response, ApiStaticUtility.TempDataTypes tempDataTypes)
        {
            if (response != null)
            {
                TempData[tempDataTypes.ToString()] = response.Message;
            }
            else
            {
                TempData[tempDataTypes.ToString()] = response?.Message ?? "An error occurred.";
            }
        }
        protected void SetTempDataMessage(string mesage, ApiStaticUtility.TempDataTypes tempDataTypes)
        {
            TempData[tempDataTypes.ToString()] = mesage;
        }
    }
}
