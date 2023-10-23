using System.ComponentModel.DataAnnotations;

namespace API.Models;

public record class CategoryDto (Guid Id, string Libelle, DateTime? CreatedAt, DateTime? UpdatedAt, bool IsDeleted, DateTime? DeletedAt) { }
