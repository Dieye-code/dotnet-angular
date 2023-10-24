namespace API.Models;

public record class OrderProductDto(Guid Id, int Price, double Quantity, ProductDto Product, DateTime? CreatedAt, DateTime? UpdatedAt)
{
}
