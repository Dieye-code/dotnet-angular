using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain.Entities;

public class OrderProduct : EntityBase
{
    public int Price { get; set; }
    public double Quantity { get; set; }

    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }

    [ForeignKey(nameof(OrderId))]
    public virtual Order Order { get; set; } = new Order();

    [ForeignKey(nameof(ProductId))]
    public virtual Product Product { get; set; }
}
