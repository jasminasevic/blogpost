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
        private readonly IAddTagCommand _addTag;

        public TagsController(IGetTagsCommand getTags,
                                IGetTagCommand getTag, 
                                IAddTagCommand addTag)
        {
            _getTags = getTags;
            _getTag = getTag;
            _addTag = addTag;
        }

        /// <summary>
        /// Returns all tags
        /// </summary>

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

        /// <summary>
        /// Returns one specific tag 
        /// </summary>

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
        public IActionResult Post([FromBody] ShowPostDtos dto)
        {
            try
            {
                _addTag.Execute(dto);
                return StatusCode(200);
            }
            catch (EntityAlreadyExistsException)
            {
                return StatusCode(422);
            }
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
