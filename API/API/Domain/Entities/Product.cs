using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain.Entities;

public class Product : EntityBase
{
    public string Libelle { get; set; } = string.Empty;
    public string PhotoUrl { get; set; } = string.Empty;
    public string PhotoLocation { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Price { get; set; }
    public double Quantity { get; set; }

    public Guid CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; } = new Category();

}
