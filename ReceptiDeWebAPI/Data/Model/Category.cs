namespace ReceptiDeWebAPI.Data.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public ICollection<Recipe> Recipes { get; set; } = new HashSet<Recipe>();
    }
}
