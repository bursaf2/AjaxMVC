using AjaxMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ProductsController : Controller
{
    private readonly ProductDbContext _context;

    public ProductsController(ProductDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();  
    }

    [HttpGet]
    public async Task<IActionResult> Search(string term)
    {
        if (string.IsNullOrWhiteSpace(term))
        {
            return Json(new { success = false, message = "Search term is empty", data = new List<object>() });
        }

        var products = await _context.Products
            .Where(p => p.Name.Contains(term))
            .Select(p => new { p.Id, p.Name, p.Price })
            .ToListAsync();

        return Json(new { success = true, data = products });
    }


}
