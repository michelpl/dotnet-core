using System.ComponentModel.DataAnnotations;

namespace customercrud.Models
{
    public class Category 
    {
        [Key]
        public int Id { get; set; }

        //Title 
        [Required(ErrorMessage = "Required field")]
        [MaxLength(60, ErrorMessage = "This field should have from 3 to 60 characters")]
        [MinLength(3, ErrorMessage = "This field should have from 3 to 60 characters")]
        public string Title { get; set; }
    }
}