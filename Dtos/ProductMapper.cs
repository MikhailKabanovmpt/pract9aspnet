using OnlineStoreApi.Models;

namespace OnlineStoreApi.Dtos;

public static class ProductMapper
{
    public static ProductDto ToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CategoryId = product.CategoryId,
            CreatedAt = product.CreatedAt
        };
    }
}
