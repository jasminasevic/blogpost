using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Commands;
using Application.Exceptions;
using EfDataAccess;
using Application.DTO;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {

        //private readonly IGetRoleCommand _getRole;

        //public RolesController(IGetRoleCommand getRole)
        //{
        //    _getRole = getRole;
        //}


        private readonly EfContext _context;

        public RolesController()
        {
            _context = new EfContext();
        }

        // GET: api/Roles
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            //try
            //{
            //    return Ok(_getRole.Execute(id));
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            var role = _context.Roles.Find(id);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(new ShowRoleDto
            {
                Name = role.Name
            });
        }

        // POST: api/Roles
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Roles/5
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
