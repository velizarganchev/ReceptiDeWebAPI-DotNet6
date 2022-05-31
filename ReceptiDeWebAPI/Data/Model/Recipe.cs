using System.ComponentModel.DataAnnotations;

namespace ReceptiDeWebAPI.Data.Model
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = String.Empty;

        [Required]
        public string Method { get; set; } = String.Empty;

        public Category Category { get; set; }

        public int CookTime { get; set; }

        public int Serves { get; set; }

        public string PictureUrl { get; set; } = String.Empty;

        public string VideoUrl { get; set; } = String.Empty;

        public bool IsDeleted { get; set; } = false;

        public User User { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; } = new HashSet<Ingredient>();

    }
}