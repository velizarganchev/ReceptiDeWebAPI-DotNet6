using ReceptiDeWebAPI.Data.Model;

namespace ReceptiDeWebAPI.Services.Categoies
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService( AppDbContext context)
        {
            _context = context;
        }

        public List<Category> GetCategories() => _context.Categories.ToList();
    }
}
