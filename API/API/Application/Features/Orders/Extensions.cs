using API.Application.Features.Products;
using API.Domain.Entities;
using API.Models;

namespace API.Application.Features.Orders;

public static class Extensions
{

    public static OrderDto ProjectToOrderDto(this Order order)
    {
        return new OrderDto(order.Id, order.SupplierName, order.Date, order.Products.Select(c => c.ProjectToDto()).ToList(), order.CreatedAt,order.UpdatedAt );
    }


    public static OrderProductDto ProjectToDto(this OrderProduct order)
    {
        return new OrderProductDto(order.Id, order.Price, order.Quantity, order.Product.ProjectToProductDto(), order.CreatedAt, order.UpdatedAt);
    }

}
