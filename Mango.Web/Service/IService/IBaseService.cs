using Mango.Web.Models;

namespace Mango.Web.Service.IService;

public interface IBaseService
{
    Task<ResponseDto?> SendAsyncCoupon(RequestDto requestDto, bool withBearer = true);

    Task<ResponseDto?> SendAsyncAuth(RequestDto requestDto);
}


