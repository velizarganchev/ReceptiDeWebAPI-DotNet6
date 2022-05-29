namespace ReceptiDeWebAPI.Data.Model
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Quantity { get; set; } = String.Empty;

        public ICollection<Recipe> Recipes { get; set; } = new HashSet<Recipe>();
    }
}
