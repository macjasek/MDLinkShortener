using MDLinkShortener.Interfaces;
using MDLinkShortener.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MDLinkShortener.Controllers
{
    public class LinkController : Controller
    {
        private ILinksRepository _repository;

        public LinkController(ILinksRepository linksRepository)
        {
            _repository = linksRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var (links, count) = _repository.Get(0);
            return View(links.Select(x => x).ToList());
        }

        [HttpPost]
        public IActionResult Create(Link link)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("Index");
            }

            _repository.AddLink(link);
            return Redirect("Index");
        }

        [HttpGet]
        public IActionResult Delete(Link link)
        {
            var linkId = link.Id;
            _repository.Delete(linkId);
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(Link link)
        {
            return View(link);
        }

        [HttpPost]
        public IActionResult Update(Link link)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(link);
            }

            return Redirect("Index");
        }

        [HttpGet]
        public IActionResult Clear()
        {
            _repository.Clear();
            return Redirect("Index");
        }
    }
}