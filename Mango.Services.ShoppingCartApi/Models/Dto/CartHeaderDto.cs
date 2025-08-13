namespace Mango.Services.ShoppingCartApi.Models.Dto;

public class CartHeaderDto
{
    public int CartHeaderId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string CouponCode { get; set; } = string.Empty;

    public double Discount { get; set; }
    public double CartTotal { get; set; }
}
