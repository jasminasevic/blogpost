﻿using System;
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
    public class UsersController : ControllerBase
    {
        private readonly IGetUserCommand _getUser;
        private readonly IGetUsersCommand _getUsers;
        private readonly IAddUserCommand _addUser;
        private readonly IEditUserCommand _editUser;
        private readonly IDeleteUserCommand _deleteUser;

        public UsersController(IGetUserCommand getUser,
                               IGetUsersCommand getUsers,
                               IAddUserCommand addUser,
                               IEditUserCommand editUser,
                               IDeleteUserCommand deleteUser)
        {
            _getUser = getUser;
            _getUsers = getUsers;
            _addUser = addUser;
            _editUser = editUser;
            _deleteUser = deleteUser;
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
        public IActionResult Post([FromBody] UserDto query)
        {
            try
            {
                _addUser.Execute(query);
                return StatusCode(204);
            }
            catch(EntityAlreadyExistsException)
            {
                return StatusCode(422);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserDto dto)
        {
            try
            {
                dto.Id = id;
                _editUser.Execute(dto);
                return StatusCode(204);
               
            }
            catch(NotFoundException)
            {
                return NotFound();
            }
            catch(EntityAlreadyExistsException)
            {
                return StatusCode(422);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _deleteUser.Execute(id);
                return StatusCode(204);
            }
            catch(NotFoundException)
            {
                return NotFound();
            }

        }
    }
}
