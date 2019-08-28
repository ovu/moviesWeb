using System.ComponentModel.DataAnnotations;

namespace moviesApp.DataTransfer
{
    public abstract class MovieDto
    {
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(100)]
        public string Director { get; set; }
        [StringLength(300)]
        public string Actors { get; set; }
        [Url]
        public string Image { get; set; }
        [Range(1900, 2050)]
        public int Year { get; set; }
    }
}
