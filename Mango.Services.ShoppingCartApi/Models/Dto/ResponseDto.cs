namespace Mango.Services.ShoppingCartApi.Models.Dto;

public class ResponseDto<T>
{
    public T? Result { get; set; }
    public bool IsSuccess { get; set; } = true;
    public string UserMessage { get; set; } = "";
    public string LogMessage { get; set; } = "";
}
