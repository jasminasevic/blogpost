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
    public class CategoriesController : ControllerBase
    {

        private readonly IGetCategoryCommand _getCategory;
        private readonly IGetCategoriesCommand _getCategories;
        private readonly IAddCategoryCommand _addCategory;
        private readonly IEditCategoryCommand _editCategory;
        private readonly IDeleteCategoryCommand _deleteCategory;
        public CategoriesController(IGetCategoryCommand getCategory,
                                    IGetCategoriesCommand getCategories,
                                    IAddCategoryCommand addCategory,
                                    IEditCategoryCommand editCategory,
                                    IDeleteCategoryCommand deleteCategory)
        {
            _getCategory = getCategory;
            _getCategories = getCategories;
            _addCategory = addCategory;
            _editCategory = editCategory;
            _deleteCategory = deleteCategory;
        }



        // GET: api/Categories
        [HttpGet]
        public IActionResult Get([FromQuery]CategoryQuery query)
        {
            try
            {
                return Ok(_getCategories.Execute(query));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        // GET: api/Categories/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_getCategory.Execute(id));
            }
            catch(NotFoundException)
            {
                return NotFound();
            }
        }

        // POST: api/Categories
        [HttpPost]
        public IActionResult Post([FromBody] CategoryDto dto)
        {
            try
            {
                _addCategory.Execute(dto);
                return StatusCode(200);

            }
            catch(EntityAlreadyExistsException)
            {
                return NotFound();
            }
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CategoryDto dto)
        {
            try
            {
                dto.Id = id;
                _editCategory.Execute(dto);
                return StatusCode(422);

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
                _deleteCategory.Execute(id);
                return StatusCode(204);
            }
            catch(NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
