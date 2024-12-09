using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodomodellNew.Data;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/categories
    [HttpGet]
    public ActionResult<IEnumerable<Category>> GetCategories()
    {
        var categories = _context.Categories.ToList();
        return Ok(categories);
    }

    // GET: api/categories/{id}
    [HttpGet("{id}")]
    public ActionResult<Category> GetCategoryById(int id)
    {
        var category = _context.Categories.Find(id);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }

    // POST: api/categories
    [HttpPost]
    public ActionResult<Category> CreateCategory(Category category)
    {
        _context.Categories.Add(category);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
    }

    // PUT: api/categories/{id}
    [HttpPut("{id}")]
    public ActionResult UpdateCategory(int id, Category updatedCategory)
    {
        if (id != updatedCategory.Id)
        {
            return BadRequest();
        }

        _context.Entry(updatedCategory).State = EntityState.Modified;
        _context.SaveChanges();

        return NoContent();
    }

    // DELETE: api/categories/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteCategory(int id)
    {
        var category = _context.Categories.Find(id);
        if (category == null)
        {
            return NotFound();
        }

        _context.Categories.Remove(category);
        _context.SaveChanges();

        return NoContent();
    }
}