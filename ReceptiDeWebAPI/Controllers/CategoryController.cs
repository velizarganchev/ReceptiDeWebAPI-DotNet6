using Microsoft.AspNetCore.Mvc;
using ReceptiDeWebAPI.Data.Model;
using ReceptiDeWebAPI.Services.Categoies;

namespace ReceptiDeWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult<List<Category>> GetCategories()
        {
            var categories = _categoryService.GetCategories();
            return Ok(categories);
        }
    }
}
