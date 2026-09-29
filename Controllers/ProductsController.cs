using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStoreApi.Data;
using OnlineStoreApi.Dtos;
using OnlineStoreApi.Models;

namespace OnlineStoreApi.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly OnlineStoreApiContext _context;

    public ProductsController(OnlineStoreApiContext context)
    {
        _context = context;
    }

    
    [HttpGet]
    public ActionResult<List<ProductDto>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        if (page < 1)
        {
            page = 1;
        }

        if (pageSize < 1)
        {
            pageSize = 10;
        }

        var products = _context.Products
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return Ok(products.Select(ProductMapper.ToDto).ToList());
    }


    [HttpGet("{id:int}")]
    public ActionResult<ProductDto> GetById(int id)
    {
        var product = _context.Products.AsNoTracking().FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(ProductMapper.ToDto(product));
    }

    
    [HttpPost]
    public ActionResult<ProductDto> Create(CreateProductDto dto)
    {
        if (!CategoryExists(dto.CategoryId))
        {
            return BadRequest($"Категория с Id = {dto.CategoryId} не существует.");
        }

        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            CategoryId = dto.CategoryId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Products.Add(product);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, ProductMapper.ToDto(product));
    }

    
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, UpdateProductDto dto)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound();
        }

        if (!CategoryExists(dto.CategoryId!.Value))
        {
            return BadRequest($"Категория с Id = {dto.CategoryId} не существует.");
        }

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price!.Value;
        product.CategoryId = dto.CategoryId.Value;

        _context.SaveChanges();

        return NoContent();
    }

    
    [HttpPatch("{id:int}")]
    public IActionResult Patch(int id, PatchProductDto dto)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound();
        }

        if (dto.CategoryId != null && !CategoryExists(dto.CategoryId.Value))
        {
            return BadRequest($"Категория с Id = {dto.CategoryId} не существует.");
        }

        if (dto.Name != null)
        {
            product.Name = dto.Name;
        }

        if (dto.Description != null)
        {
            product.Description = dto.Description;
        }

        if (dto.Price != null)
        {
            product.Price = dto.Price.Value;
        }

        if (dto.CategoryId != null)
        {
            product.CategoryId = dto.CategoryId.Value;
        }

        _context.SaveChanges();

        return NoContent();
    }

    
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateException)
        {
          
            return Conflict("Товар нельзя удалить: он используется в корзинах или заказах.");
        }

        return NoContent();
    }

    private bool CategoryExists(int categoryId)
    {
        return _context.Categories.Any(c => c.Id == categoryId);
    }
}
