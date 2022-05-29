using ReceptiDeWebAPI.Data.Model;
using ReceptiDeWebAPI.Models.Recipe;

namespace ReceptiDeWebAPI.Services.Recipes
{
    public class RecipeService : IRecipeservice
    {
        private readonly AppDbContext _context;

        public RecipeService(AppDbContext context)
        {
            _context = context;
        }

        public List<Recipe> AddRecipe(RecipeModel request)
        {
            var recipe = new Recipe
            {
                Title = request.Title,
                Method = request.Method,
                PictureUrl = request.PictureUrl,
                VideoUrl = request.VideoUrl,
                CookTime = int.Parse(request.CookTime),
                Serves = int.Parse(request.Serves),
                IsDeleted = request.IsDeleted,
            };

            var existUser = _context.Users.FirstOrDefault(x => x.Id == int.Parse(request.CreatorId));
            if (existUser == null)
            {
                recipe.User = new User { Id = int.Parse(request.CreatorId) };
            }
            else
            {
                recipe.User = existUser;
            }

            var existCategory = _context.Categories.FirstOrDefault(x => x.Name == request.Category);
            if (existCategory == null)
            {
                recipe.Category = new Category { Name = request.Category };
            }
            recipe.Category = existCategory;


            foreach (var ingredient in request.Ingredients)
            {
                var existingredient = _context.Ingredients.FirstOrDefault(x => x.Name == ingredient.Name);
                if (existingredient == null)
                {
                    recipe.Ingredients.Add(new Ingredient
                    {
                        Name = ingredient.Name,
                        Quantity = ingredient.Quantity,
                    });
                }
                else
                {
                    recipe.Ingredients.Add(existingredient);
                }
            }

            _context.Recipes.Add(recipe);
            _context.SaveChanges();

            return _context.Recipes.ToList();

        }

        public List<Recipe> UpdateRecipe(int id, RecipeModel request)
        {
            var recipeForUpdate = _context.Recipes.Find(id);

            recipeForUpdate.Title = request.Title;
            recipeForUpdate.Method = request.Method;
            recipeForUpdate.User.Id = int.Parse(request.CreatorId);
            recipeForUpdate.PictureUrl = request.PictureUrl;
            recipeForUpdate.VideoUrl = request.VideoUrl;
            recipeForUpdate.CookTime = int.Parse(request.CookTime);
            recipeForUpdate.Serves = int.Parse(request.Serves);
            recipeForUpdate.IsDeleted = request.IsDeleted;

            var existCategory = _context.Categories.FirstOrDefault(x => x.Name == request.Category);
            if (existCategory == null)
            {
                recipeForUpdate.Category = new Category { Name = request.Category };
            }
            else
            {
                recipeForUpdate.Category = existCategory;
            }

            foreach (var ingredient in request.Ingredients)
            {
                var existingredient = _context.Ingredients.FirstOrDefault(x => x.Name == ingredient.Name);
                if (existingredient == null)
                {
                    recipeForUpdate.Ingredients.Add(new Ingredient
                    {
                        Name = ingredient.Name,
                        Quantity = ingredient.Quantity,
                    });
                }
                else
                {
                    recipeForUpdate.Ingredients.Add(existingredient);
                }
            }

            _context.SaveChanges();

            return _context.Recipes.ToList();
        }
        public List<Recipe> GetAllRecipes() => _context.Recipes.ToList();

        public Recipe? GetRecipe(int id) => _context.Recipes.Find(id);

        public List<Recipe> DeleteRecipe(int id)
        {
            var recipe = _context.Recipes.Find(id);

            recipe.IsDeleted = true;
            _context.SaveChanges();

            return _context.Recipes.ToList();
        }
    }
}
