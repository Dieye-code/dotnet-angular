using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain.Entities;

public class SaleProduct : EntityBase
{
    public int Price { get; set; }
    public double Quantity { get; set; }

    public Guid ProductId { get; set; }
    public Guid SaleId { get; set; }

    [ForeignKey(nameof(ProductId))]
    public virtual Product Product { get; set; } = new Product();

    [ForeignKey(nameof(SaleId))]
    public virtual Sale Sale { get; set; } = new Sale();
}
