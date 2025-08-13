namespace Mango.Services.ShoppingCartApi.Models.Dto;

public class CartDetailsDto
{
    public int CartDetailsId { get; set; }
    public int CartHeaderId { get; set; }
    public CartHeaderDto? CartHeader { get; set; }
    public int ProductId { get; set; }
    public ProductDto ProductDto { get; set; } = new ProductDto();
    public int Count { get; set; }
}
