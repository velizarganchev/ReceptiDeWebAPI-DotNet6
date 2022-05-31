using Microsoft.AspNetCore.Mvc;
using ReceptiDeWebAPI.Data.Model;
using ReceptiDeWebAPI.Models.Recipe;
using ReceptiDeWebAPI.Services;
using ReceptiDeWebAPI.Services.Recipes;

namespace ReceptiDeWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeservice _recipeService;

        public RecipeController(IRecipeservice recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<ServiceResponse<List<GetRecipeModel>>>> Get()
        {
            var recipes = await _recipeService.GetAllRecipes();
            if (recipes == null)
                return BadRequest("No Recipes in database.");

            return await _recipeService.GetAllRecipes();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetRecipeModel>>> Get(int id)
        {
            var recipe = await _recipeService.GetRecipe(id);
            if (recipe.Data == null)
                return BadRequest("Recipe not found.");

            return Ok(recipe);
        }

        [HttpPost("addRecipe")]
        public async Task<ActionResult<ServiceResponse<List<GetRecipeModel>>>> AddRecipe(AddRecipeModel request)
        {
            var recipes = await _recipeService.AddRecipe(request);
            if (recipes == null)
                return BadRequest("No Recipes in database.");
            return Ok(recipes);
        }

        [Route("updateRecipe/{id:int}")]
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetRecipeModel>>>> updateRecipe(int id, UpdateRecipeModel request)
        {
            var recipes = await _recipeService.UpdateRecipe(id, request);
            if (recipes == null)
                return BadRequest("No Recipes in database.");
            return Ok(recipes);
        }

        [HttpDelete("deleteRecipe/{id:int}")]
        public async Task<ActionResult<ServiceResponse<List<GetRecipeModel>>>> Delete(int id)
        {
            var recipes = await _recipeService.DeleteRecipe(id);
            if (recipes == null)
                return BadRequest("No Recipes in database.");

            return Ok(recipes);
        }

    }
}
