using System.ComponentModel.DataAnnotations;

namespace MDLinkShortener.Models
{
    public class Link
    {
        public int Id { get; set; }
        [Required]
        [Url]
        public string FullLink { get; set; }
        public string ShortLink { get; set; }
    }
}
