using MDLinkShortener.Models;
using System.Collections.Generic;

namespace MDLinkShortener.Interfaces
{
    public interface ILinksRepository
    {
        List<Link> GetLinks();
        void AddLink(Link link);
        void Delete(int linkId);
        void Update(Link link);
        string RedirectLink(string id);
        void Clear();
    }
}
