namespace API.Models;

public record class SaleProductDto(Guid Id, int Price, double Quantity, ProductDto Product, DateTime? CreateDate, DateTime? UpdateDate)
{
}
