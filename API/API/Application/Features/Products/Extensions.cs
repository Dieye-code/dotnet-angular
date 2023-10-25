using API.Application.Features.Categories;
using API.Domain.Entities;
using API.Models;

namespace API.Application.Features.Products;

public static class Extensions
{
    public static ProductDto ProjectToProductDto(this Product product)
    {
        return new ProductDto(product.Id, product.Libelle, product.Description, product.Photo, product.Price, product.Quantity, product.Category?.ProjectToCategoryDto(), product.CreatedAt, product.UpdatedAt);
    }
}
