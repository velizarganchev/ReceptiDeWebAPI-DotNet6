namespace ReceptiDeWebAPI.Models.Recipe
{
    public class RecipeModel
    {
        // ADD VALIDATION !!!!!!!!!!!!
        public string Title { get; set; } = String.Empty;

        public string Method { get; set; } = String.Empty;

        public string Category { get; set; } = String.Empty;

        public string CookTime { get; set; } = String.Empty;

        public string Serves { get; set; } = String.Empty;

        public string PictureUrl { get; set; } = String.Empty;

        public string VideoUrl { get; set; } = String.Empty;

        public bool IsDeleted { get; set; }

        public string CreatorId { get; set; } = String.Empty;

        public List<IngredientModel> Ingredients { get; set; } = new List<IngredientModel>();
    }
}
