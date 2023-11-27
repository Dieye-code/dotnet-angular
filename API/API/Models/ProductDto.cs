using System.ComponentModel.DataAnnotations;

namespace API.Models;

public record class ProductDto(Guid Id, string Libelle, string Description, string? Photo, string? PhotoLocation, int Price, double Quantity, CategoryDto? Category, DateTime? CreatedAt, DateTime? UpdatedAt) { }
