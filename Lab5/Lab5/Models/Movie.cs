using System.ComponentModel.DataAnnotations;

namespace Lab5.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [UIHint("LongText")]
        public string Description { get; set; }

        [Range(0, 5)]
        [UIHint("Stars")]
        public int Rating { get; set; }

        [Url]
        public string TrailerLink { get; set; }
    }
}
