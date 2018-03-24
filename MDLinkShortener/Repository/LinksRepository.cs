using HashidsNet;
using MDLinkShortener.Interfaces;
using MDLinkShortener.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MDLinkShortener.Repository
{
    public class LinksRepository : ILinksRepository
    {
        private List<Link> _links;
        private Hashids hashids = new Hashids("Akademia WebDev",4);
        private readonly LinkDbContext _context;
        private readonly int linksPerPage = 20;


        public LinksRepository(LinkDbContext context)
        {
            _context = context;
            _links = new List<Link>
            {
                new Link
                {
                    Id = 0,
                    FullLink = "https://ilovebluesguitar.com/",
                    ShortLink = hashids.Encode(TimeSinceMidnight())
                },
                new Link
                {
                    Id = 1,
                    FullLink = "https://www.pgs-soft.com/pl/",
                    ShortLink = hashids.Encode(TimeSinceMidnight())
                }
            };

        }

        private static int TimeSinceMidnight()
        {
            DateTime dateTime = DateTime.Now;
            int timeMsSinceMidnight = (int)dateTime.TimeOfDay.TotalMilliseconds;
            return timeMsSinceMidnight;
        }

        public (IEnumerable<Link>, int) Get(int skip)
        {
            var links = _context.Links;
            var linksCount = links.Count();

            var paginatedLinks = links
                .OrderBy(x => x.Id)
                .Skip(skip)
                .Take(linksPerPage);

            return (paginatedLinks, linksCount);
        }

        public void AddLink(Link link)
        {
            link.ShortLink = hashids.Encode(TimeSinceMidnight());
            _context.Links.Add(link);
            _context.SaveChanges();
        }

        public void Delete(int linkId)
        {
            Link linkEntity = _context.Links.Find(linkId);
            _context.Links.Remove(linkEntity);
            _context.SaveChanges();
            
        }

        public void Update(Link link)
        {
            _context.Links.Attach(link);
            _context.SaveChanges();
        }

        public string RedirectLink(string id)
        {
            var linkToRedirect = _context.Links
                .Where(x => x.ShortLink == id)
                .FirstOrDefault();

            if (linkToRedirect != null)
            {
                return linkToRedirect.FullLink;
            }

            return "index";

                
        }

        public void Clear()
        {
            _context.RemoveRange(_context.Links);
        }


    }
}
