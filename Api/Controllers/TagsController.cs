using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using Application.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly IGetTagsCommand _getTags;
        private readonly IGetTagCommand _getTag;

        public TagsController(IGetTagsCommand getTags, 
                                IGetTagCommand getTag)
        {
            _getTags = getTags;
            _getTag = getTag;
        }

        // GET: api/Tags
        [HttpGet]
        public IActionResult Get(TagQuery query)
        {
            try
            {
                var tags = _getTags.Execute(query);
                return Ok(tags);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        // GET: api/Tags/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_getTag.Execute(id));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        // POST: api/Tags
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Tags/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
