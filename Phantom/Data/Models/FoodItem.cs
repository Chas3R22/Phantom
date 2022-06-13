using System.ComponentModel.DataAnnotations;

namespace Phantom.Data.Models
{
    public class FoodItem
    {
        public FoodItem()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
