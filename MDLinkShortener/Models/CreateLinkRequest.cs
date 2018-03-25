using System.ComponentModel.DataAnnotations;

namespace MDLinkShortener.Models
{
    public class CreateLinkRequest
    {
        [Url]
        [Required]
        public string FullLink { get; set; }

        public Link GetLink()
        {
            var link = new Link
            {
                FullLink = this.FullLink
            };

            return link;
        }
    }
}
