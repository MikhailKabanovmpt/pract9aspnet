namespace OnlineStoreApi.Models;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public int RoleId { get; set; }
    public DateTime CreatedAt { get; set; }

    public Role Role { get; set; } = null!;
    public Cart? Cart { get; set; }
    public List<Order> Orders { get; set; } = new();
}
