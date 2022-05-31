using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReceptiDeWebAPI.Data.Model;
using ReceptiDeWebAPI.Models.Recipe;

namespace ReceptiDeWebAPI.Services.Recipes
{
    public class RecipeService : IRecipeservice
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RecipeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetRecipeModel>>> AddRecipe(AddRecipeModel request)
        {
            var serviceResponse = new ServiceResponse<List<GetRecipeModel>>();
            try
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

                var existUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.User.Id);
                if (existUser == null)
                {
                    recipe.User = new User { Id = request.User.Id };
                }
                else
                {
                    recipe.User = existUser;
                }

                var existCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Name == request.Category.Name);
                if (existCategory == null)
                {
                    recipe.Category = new Category { Name = request.Category.Name };
                }
                else
                {
                    recipe.Category = existCategory;
                }



                foreach (var ingredient in request.Ingredients)
                {
                    var existingredient = await _context.Ingredients.FirstOrDefaultAsync(x => x.Name == ingredient.Name);
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
                await _context.SaveChangesAsync();

                var dbRecipes = await _context.Recipes
                    .Include(x => x.Category)
                    .Include(x => x.User)
                    .Include(x => x.Ingredients)
                    .ToListAsync();

                serviceResponse.Data = dbRecipes.Select(r => _mapper.Map<GetRecipeModel>(r)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
      
            return serviceResponse;

        }

        public async Task<ServiceResponse<List<GetRecipeModel>>> UpdateRecipe(int id, UpdateRecipeModel request)
        {
            var serviceResponse = new ServiceResponse<List<GetRecipeModel>>();

            var recipeForUpdate = 
                await _context.Recipes
                .Include(x => x.Category)
                .Include(x => x.Ingredients)
                .FirstOrDefaultAsync(x => x.Id == id);

            recipeForUpdate.Title = request.Title;
            recipeForUpdate.Method = request.Method;
            recipeForUpdate.PictureUrl = request.PictureUrl;
            recipeForUpdate.VideoUrl = request.VideoUrl;
            recipeForUpdate.CookTime = int.Parse(request.CookTime);
            recipeForUpdate.Serves = int.Parse(request.Serves);
            recipeForUpdate.IsDeleted = request.IsDeleted;

            var existCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Name == request.Category);
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
                var existingredient = await _context.Ingredients.FirstOrDefaultAsync(x => x.Name == ingredient.Name);
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

            await _context.SaveChangesAsync();
            serviceResponse.Data =
                _context.Recipes
                .Include(x => x.Category)
                .Include(x => x.User)
                .Include(x => x.Ingredients)
                .Select(r => _mapper.Map<GetRecipeModel>(r)).ToList();

            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetRecipeModel>>> GetAllRecipes()
        {
            var serviceResponse = new ServiceResponse<List<GetRecipeModel>>();

            var dbRecipes = await _context.Recipes
                .Include(x => x.Category)
                .Include(x => x.User)
                .Include(x => x.Ingredients)
                .ToListAsync();
            serviceResponse.Data = dbRecipes.Select(r => _mapper.Map<GetRecipeModel>(r)).ToList();

            return serviceResponse;

        }

        public async Task<ServiceResponse<GetRecipeModel>> GetRecipe(int id)
        {
            var serviceResponse = new ServiceResponse<GetRecipeModel>();
            var dbRecipe = await _context.Recipes
                 .Include(x => x.Category)
                 .Include(x => x.User)
                 .Include(x => x.Ingredients)
                 .FirstOrDefaultAsync(x => x.Id == id);
            serviceResponse.Data = _mapper.Map<GetRecipeModel>(dbRecipe);

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetRecipeModel>>> DeleteRecipe(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetRecipeModel>>();

            var recipe = await _context.Recipes.FindAsync(id);

            recipe.IsDeleted = true;
            await _context.SaveChangesAsync();

            var dbRecipes = await _context.Recipes
                .Include(x => x.Category)
                .Include(x => x.User)
                .Include(x => x.Ingredients)
                .ToListAsync();

            serviceResponse.Data = dbRecipes.Select(r => _mapper.Map<GetRecipeModel>(r)).ToList();

            return serviceResponse;
        }
    }
}
