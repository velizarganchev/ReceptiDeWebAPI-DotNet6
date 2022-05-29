using ReceptiDeWebAPI.Data.Model;
using ReceptiDeWebAPI.Models.Recipe;

namespace ReceptiDeWebAPI.Services.Recipes
{
    public interface IRecipeservice
    {
        public List<Recipe> GetAllRecipes();
        public Recipe? GetRecipe(int id);
        public List<Recipe> AddRecipe(RecipeModel recipe); 
        public List<Recipe> UpdateRecipe(int id,RecipeModel recipe);
        public List<Recipe> DeleteRecipe(int id);


    }
}
