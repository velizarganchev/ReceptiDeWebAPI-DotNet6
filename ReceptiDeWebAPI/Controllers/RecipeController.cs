using Microsoft.AspNetCore.Mvc;
using ReceptiDeWebAPI.Data.Model;
using ReceptiDeWebAPI.Models.Recipe;
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
        public ActionResult<List<Recipe>> Get()
        {
            var recipes = _recipeService.GetAllRecipes();
            if (recipes == null)
            {
                return BadRequest("No Recipes in database.");
            }
            return _recipeService.GetAllRecipes();
        }

        [HttpGet("{id}")]
        public ActionResult<Recipe> Get(int id)
        {
            var recipe = _recipeService.GetRecipe(id);
            if (recipe == null)
                return BadRequest("Recipe not found.");

            return Ok(recipe);
        }

        [HttpPost("addRecipe")]
        public ActionResult<List<Recipe>> AddRecipe(RecipeModel request)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest("Model is not correct!");
            }

            var recipes = _recipeService.AddRecipe(request);
            if (recipes == null)
            {
                return BadRequest("No Recipes in database.");
            }
            return Ok(recipes);
        }

        [Route("updateRecipe/{id:int}")]
        [HttpPut]
        public ActionResult<List<Recipe>> updateRecipe(int id, RecipeModel request)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest("Model is not correct!");
            }

            var recipes = _recipeService.UpdateRecipe(id, request);
            if (recipes == null)
            {
                return BadRequest("No Recipes in database.");
            }
            return Ok(recipes);
        }

        [HttpDelete("deleteRecipe/{id:int}")]
        public ActionResult<List<Recipe>> Delete(int id)
        {
            var recipes = _recipeService.DeleteRecipe(id);
            if (recipes == null)
                return BadRequest("No Recipes in database.");

            return Ok(recipes);
        }

    }
}
