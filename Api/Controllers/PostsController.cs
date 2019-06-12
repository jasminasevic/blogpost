using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
using Application.Exceptions;
using Application.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {

        private readonly IGetPostCommand _getPost;
        private readonly IGetPostsCommand _getPosts;

        public PostsController(IGetPostCommand getPost,
                               IGetPostsCommand getPosts)
        {
            _getPost = getPost;
            _getPosts = getPosts;
        }

        // GET: api/Posts
        [HttpGet]
        public IActionResult Get([FromQuery]PostQuery query)
        {
            try
            {
                return Ok(_getPosts.Execute(query));
            }
            catch(NotFoundException)
            {
                return NotFound();
            }
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_getPost.Execute(id));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        // POST: api/Posts
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Posts/5
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
