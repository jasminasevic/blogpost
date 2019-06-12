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
    public class PostsController : ControllerBase
    {

        private readonly IGetPostCommand _getPost;
        private readonly IGetPostsCommand _getPosts;
        private readonly IAddPostCommand _addPost;
        private readonly IEditPostCommand _editPost;

        public PostsController(IGetPostCommand getPost,
                               IGetPostsCommand getPosts,
                               IAddPostCommand addPost,
                               IEditPostCommand editPost)
        {
            _getPost = getPost;
            _getPosts = getPosts;
            _addPost = addPost;
            _editPost = editPost;
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
        public IActionResult Post([FromBody] PostDto dto)
        {
            try
            {
                _addPost.Execute(dto);
                return StatusCode(204);
            }
            catch(EntityAlreadyExistsException)
            {
                return StatusCode(422);
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        // PUT: api/Posts/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PostDto dto)
        {
            try
            {
                dto.Id = id;
                _editPost.Execute(dto);
                return StatusCode(204);
            }
            catch(NotFoundException)
            {
                return NotFound();
            }
            //catch(EntityAlreadyExistsException)
            //{
            //    return StatusCode(422);
            //}
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
