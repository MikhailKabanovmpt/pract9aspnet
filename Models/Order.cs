namespace OnlineStoreApi.Models;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; } = null!;
    public decimal TotalAmount { get; set; }

    public User User { get; set; } = null!;
    public List<OrderItem> Items { get; set; } = new();
}
