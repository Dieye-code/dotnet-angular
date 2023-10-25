namespace API.Domain.Entities;

public class Order : EntityBase
{

    public string SupplierName { get; set; } = string.Empty;
    public DateTime date { get; set; }

    public virtual List<OrderProduct> Products { get; set; } = new();

}
