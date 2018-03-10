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

        public LinksRepository()
        {
            _links = new List<Link>
            {
                new Link
                {
                    Id = 0,
                    FullLink = "https://ilovebluesguitar.com/",
                    ShortLink = hashids.Encode(timeSinceMidnight())
                },
                new Link
                {
                    Id = 1,
                    FullLink = "https://www.pgs-soft.com/pl/",
                    ShortLink = hashids.Encode(timeSinceMidnight())
                }
            };

        }

        private static int timeSinceMidnight()
        {
            DateTime dateTime = DateTime.Now;
            int timeMsSinceMidnight = (int)dateTime.TimeOfDay.TotalMilliseconds;
            return timeMsSinceMidnight;
        }

        public List<Link> GetLinks()
        {
            return _links;
        }

        public void AddLink(Link link)
        {
            if (_links.Count > 0)
            {
                link.Id = _links[_links.Count - 1].Id + 1;
            }
            else
            {
                link.Id = 0;
            }
            

            link.ShortLink = hashids.Encode(timeSinceMidnight());
            _links.Add(link);
        }

        public void Delete(int linkId)
        {
            var linkToDelete = _links.SingleOrDefault(element => element.Id == linkId);
            if (linkToDelete !=null)
            {
                _links.Remove(linkToDelete);
            }
            
        }

        public void Update(Link link)
        {
            var linkToUpdateIndex = _links.FindIndex(element => element.Id == link.Id);
            if(linkToUpdateIndex != -1)
            {
                _links[linkToUpdateIndex] = link;
            }
        }

        public string RedirectLink(string id)
        {
            var linkToRedirectIndex = _links.FindIndex(element => element.ShortLink == id);
            if (linkToRedirectIndex != -1)
            {
                return _links[linkToRedirectIndex].FullLink;
            }

            return "Index";
                
        }

        public void Clear()
        {
            _links.Clear();
        }
    }
}
