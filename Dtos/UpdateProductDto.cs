using System.ComponentModel.DataAnnotations;

namespace OnlineStoreApi.Dtos;


public class UpdateProductDto
{
    [Required]
    [StringLength(150, MinimumLength = 1)]
    public string Name { get; set; } = null!;

    [Required]
    public string? Description { get; set; }

    [Required]
    [Range(0, 99999999.99)]
    public decimal? Price { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int? CategoryId { get; set; }
}
