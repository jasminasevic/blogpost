﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Commands;
using Application.Exceptions;
using EfDataAccess;
using Application.DTO;
using Application.Queries;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {

        private readonly IGetRoleCommand _getRole;
        private readonly IGetRolesCommand _getRoles;
        private readonly IAddRoleCommand _addRole;

        public RolesController(IGetRoleCommand getRole, IGetRolesCommand getRoles, IAddRoleCommand addRole)
        {
            _getRole = getRole;
            _getRoles = getRoles;
            _addRole = addRole;
        }


        // GET: api/Roles
        [HttpGet]
        public IActionResult Get([FromQuery]RoleQuery query)
        {
            try
            {
                return Ok(_getRoles.Execute(query));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_getRole.Execute(id));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

        }

        // POST: api/Roles
        [HttpPost]
        public IActionResult Post([FromBody] RoleDto dto)
        {
            try
            {
                _addRole.Execute(dto);
                return StatusCode(200);
            }
            catch(EntityAlreadyExistsException)
            {
                return StatusCode(422);
            }

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
