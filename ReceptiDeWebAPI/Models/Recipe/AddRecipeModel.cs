namespace ReceptiDeWebAPI.Models.Recipe
{
    public class AddRecipeModel
    {
        public string Title { get; set; } = String.Empty;

        public string Method { get; set; } = String.Empty;

        public CategoryModel Category { get; set; }

        public string CookTime { get; set; } = String.Empty;

        public string Serves { get; set; } = String.Empty;

        public string PictureUrl { get; set; } = String.Empty;

        public string VideoUrl { get; set; } = String.Empty;

        public bool IsDeleted { get; set; } = false;

        public CreatorModel User { get; set; }

        public List<IngredientModel> Ingredients { get; set; } = new List<IngredientModel>();
    }
}
