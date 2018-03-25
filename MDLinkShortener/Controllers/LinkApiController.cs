using MDLinkShortener.Interfaces;
using MDLinkShortener.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MDLinkShortener.Controllers
{


    [Produces("application/json")]
    [Route("api/LinkApi")]
    public class LinkApiController : Controller
    {
        private ILinksRepository _repository;
        private int itemPerPage = 10;

        public LinkApiController(ILinksRepository linksRepository)
        {
            _repository = linksRepository;
        }

        // GET: api/LinkApi/?page={int}
        [HttpGet]
        public IActionResult Get([FromQuery] int page = 1)
        {
            var (links, count) = _repository
                                 .Get((page-1) * itemPerPage);

            var result = new QueryResult
            {

                PageInfo = new PageInfo
                {
                    CurrentPage = page,
                    MaxPage = count % itemPerPage == 0 ? count / itemPerPage : count / itemPerPage + 1
                },
                Items = links.Select(x => new LinkResult(x))
            };

            return Ok(result);
        }
        
        // POST: api/LinkApi
        [HttpPost]
        public IActionResult Post([FromBody]CreateLinkRequest createLink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(@"Link url format is not correct. Use full link started from http:// or https://");
            }

            _repository.AddLink(createLink.GetLink());
            return Ok();
        }
        
        // PUT: api/LinkApi/5
        [HttpPut]
        public IActionResult Put([FromBody]Link link)
        {
            _repository.Update(link);
            return Ok();
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return Ok();
        }
    }
}
