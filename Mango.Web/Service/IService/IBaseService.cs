using Mango.Web.Models;

namespace Mango.Web.Service.IService;

public interface IBaseService
{
    Task<ResponseDto?> SendAsyncCoupon(RequestDto requestDto);

    Task<ResponseDto?> SendAsyncAuth(RequestDto requestDto);
}


