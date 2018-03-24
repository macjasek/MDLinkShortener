using MDLinkShortener.Models;
using Microsoft.EntityFrameworkCore;

namespace MDLinkShortener
{
    public class LinkDbContext : DbContext
    {
        public LinkDbContext(DbContextOptions<LinkDbContext> options) : base(options)
        {
        }

        public DbSet<Link> Links { get; set; }

    }
}
