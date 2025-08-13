using Mango.Services.ShoppingCartApi.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.ShoppingCartApi.Models.DbModels;

public class CartDetails
{
    [Key]
    public int CartDetailsId { get; set; }
    public int CartHeaderId { get; set; }
    [ForeignKey("CartHeaderId")]
    public CartHeader CartHeader { get; set; } = new CartHeader();

    public int ProductId { get; set; }

    [NotMapped]
    public ProductDto ProductDto { get; set; } = new ProductDto();

    public int Count { get; set; } = 1;
}
