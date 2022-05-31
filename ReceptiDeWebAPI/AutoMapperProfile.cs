using AutoMapper;
using ReceptiDeWebAPI.Data.Model;
using ReceptiDeWebAPI.Models.Recipe;

namespace ReceptiDeWebAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Recipe, GetRecipeModel>();
            CreateMap<Ingredient, IngredientModel>();
            CreateMap<Category, CategoryModel>();
            CreateMap<User, CreatorModel>();
        }
    }
}
