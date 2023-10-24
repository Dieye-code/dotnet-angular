namespace API.Models;

public record class OrderDto(Guid Id, string SupplierName, DateTime date, List<OrderProductDto> Products, DateTime? CreatedAt, DateTime? UpdatedAt)
{
}
