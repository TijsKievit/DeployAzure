using System.ComponentModel.DataAnnotations;

namespace BeestjeOpJeFeestje.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "De naam mag alleen letters hebben")]
        [StringLength(100, ErrorMessage = "De naam mag niet langer dan 100 letters")]
        public string Name { get; set; }
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Het type mag alleen letters hebben")]
        [StringLength(100, ErrorMessage = "Het type mag niet langer dan 100 letters")]
        public string Type { get; set; }
        public double Price { get; set; }
        public string ImageLink { get; set; }

    }
}
