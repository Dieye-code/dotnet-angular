namespace API.Models;

public record class SaleDto(Guid Id, string Customername, DateTime date, List<SaleProductDto> Products, DateTime? CreatedAt,  DateTime? UpdatedAt)
{
}
