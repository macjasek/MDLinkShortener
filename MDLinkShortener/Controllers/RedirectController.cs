using MDLinkShortener.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MDLinkShortener.Controllers
{
    public class RedirectController : Controller
    {

        private ILinksRepository _repository;

        public RedirectController(ILinksRepository linksRepository)
        {
            _repository = linksRepository;
        } 

        [HttpGet("/{id}")]
        public IActionResult Index(string id)
        {
            var linkToRedirect = _repository.RedirectLink(id);
            return Redirect(linkToRedirect);
        }
    }
}