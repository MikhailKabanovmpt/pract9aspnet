namespace OnlineStoreApi.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public DateTime CreatedAt { get; set; }

    public Category Category { get; set; } = null!;
    public List<CartItem> CartItems { get; set; } = new();
    public List<OrderItem> OrderItems { get; set; } = new();
}
