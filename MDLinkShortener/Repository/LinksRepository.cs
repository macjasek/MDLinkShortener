using HashidsNet;
using MDLinkShortener.Interfaces;
using MDLinkShortener.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MDLinkShortener.Repository
{
    public class LinksRepository : ILinksRepository
    {
        
        private Hashids hashids = new Hashids("Akademia WebDev",4);
        private readonly LinkDbContext _context;
        private readonly int linksPerPage = 20;


        public LinksRepository(LinkDbContext context)
        {
            _context = context;
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
            _context.Entry(link).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public string RedirectLink(string id)
        {
            Link linkToRedirect = GetLinkFromShortLink(id);

            if (linkToRedirect != null)
            {
                return linkToRedirect.FullLink;
            }

            return "index";


        }

        private Link GetLinkFromShortLink(string id)
        {
            return _context.Links
                .Where(x => x.ShortLink == id)
                .FirstOrDefault();
        }

        public void Clear()
        {
            _context.RemoveRange(_context.Links);
        }

        public void SaveLinkClick(string id, string clientIpAddress)
        {
            var link = GetLinkFromShortLink(id);
            link.Clicks++;
            _context.Links.Attach(link);
            _context.Entry(link).State = EntityState.Modified;
            _context.SaveChanges();

            //IpAddress ipAddress = GetIpAddressFromClientIp(clientIpAddress);

            //if (ipAddress == null)
            //{
            //    link.UniqueClicks++;
            //    ipAddress = new IpAddress { ClientIp = clientIpAddress };
            //    IpLink ipLink = new IpLink { Link = link, IpAddress = ipAddress };
            //    _context.IpAddresses.Add(ipAddress);
            //    _context.IpLink.Add()
            //}

            




        }

        private IpAddress GetIpAddressFromClientIp(string clientIpAddress)
        {
            return _context.IpAddresses
                .Where(x => x.ClientIp == clientIpAddress)
                .FirstOrDefault();
        }
    }
}
