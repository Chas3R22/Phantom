using System.ComponentModel.DataAnnotations;

namespace Phantom.Data.Models
{
    public class Category
    {
        public Category()
        {
            this.Id = Guid.NewGuid();
            this.FoodItems = new HashSet<FoodItem>();
        }
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<FoodItem> FoodItems { get; set; }
    }
}
