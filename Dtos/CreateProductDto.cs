using System.ComponentModel.DataAnnotations;

namespace OnlineStoreApi.Dtos;


public class CreateProductDto
{
    [Required]
    [StringLength(150, MinimumLength = 1)]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    [Range(0, 99999999.99)]
    public decimal Price { get; set; }

    [Range(1, int.MaxValue)]
    public int CategoryId { get; set; }
}
