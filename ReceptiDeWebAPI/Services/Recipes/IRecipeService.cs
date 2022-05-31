using ReceptiDeWebAPI.Models.Recipe;

namespace ReceptiDeWebAPI.Services.Recipes
{
    public interface IRecipeservice
    {
        public Task<ServiceResponse<List<GetRecipeModel>>> GetAllRecipes();
        public Task<ServiceResponse<GetRecipeModel>> GetRecipe(int id);
        public Task<ServiceResponse<List<GetRecipeModel>>> AddRecipe(AddRecipeModel recipe); 
        public Task<ServiceResponse<List<GetRecipeModel>>> UpdateRecipe(int id, UpdateRecipeModel recipe);
        public Task<ServiceResponse<List<GetRecipeModel>>> DeleteRecipe(int id);


    }
}
