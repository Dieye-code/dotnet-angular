namespace API.Domain.Entities;

public class Sale : EntityBase
{
    public string CustomerName { get; set; } = string.Empty;
    public DateTime Date { get; set; }

    public virtual List<SaleProduct> Products { get; set; } = new();
}
