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
    public class UsersController : ControllerBase
    {
        private readonly IGetUserCommand _getUser;
        private readonly IGetUsersCommand _getUsers;

        public UsersController(IGetUserCommand getUser,
                               IGetUsersCommand getUsers)
        {
            _getUser = getUser;
            _getUsers = getUsers;
        }


        // GET: api/Users
        [HttpGet]
        public IActionResult Get([FromQuery]UserQuery query)
        {
            try
            {
                return Ok(_getUsers.Execute(query));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_getUser.Execute(id));
            }
            catch(NotFoundException)
            {
                return NotFound();
            }
        }

        // POST: api/Users
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Users/5
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
