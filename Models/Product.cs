using System.ComponentModel.DataAnnotations;

namespace customercrud.Models
{
    public class Product 
    {
        [Key]
        public int Id { get; set; }

        //Title
        [Required(ErrorMessage = "Required field")]
        [MaxLength(60, ErrorMessage = "This field should have from 3 to 60 characters")]
        [MinLength(3, ErrorMessage = "This field should have from 3 to 60 characters")]
        public string Title { get; set; }

        //Description
        [Required(ErrorMessage = "Required field")]
        [MaxLength(1024, ErrorMessage = "This field should have from 3 to 1024 characters")]
        [MinLength(3, ErrorMessage = "This field should have from 3 to 1024 characters")]
        public string Description { get; set; }

        //Price
        [Required(ErrorMessage = "Required field")]
        [Range(1, int.MaxValue, ErrorMessage = "This field should be greater than 0")]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set;}
    }
}