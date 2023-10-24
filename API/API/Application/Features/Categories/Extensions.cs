using API.Domain.Entities;
using API.Models;

namespace API.Application.Features.Categories;

public static class Extensions
{
    public static CategoryDto ProjectToCategoryDto(this Category category)
    {
        return new CategoryDto(category.Id, category.Libelle, category.CreatedAt, category.UpdatedAt);
    }
}
