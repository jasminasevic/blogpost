﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using Application.Searches;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IGetUserCommand _getUser;
        private readonly IGetSearchUsersCommand _getSearchUsers;
        private readonly IAddUserCommand _addUser;
        private readonly IEditUserCommand _editUser;
        private readonly IDeleteUserCommand _deleteUser;

        public UsersController(IGetUserCommand getUser,
                               IGetSearchUsersCommand getSearchUsers,
                               IAddUserCommand addUser, 
                               IEditUserCommand editUser,
                               IDeleteUserCommand deleteUser)
        {
            _getUser = getUser;
            _getSearchUsers = getSearchUsers;
            _addUser = addUser;
            _editUser = editUser;
            _deleteUser = deleteUser;
        }

        // GET: Users
        public ActionResult Index(UserSearch search)
        {
            var users = _getSearchUsers.Execute(search);
            return View(users);
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var dto = _getUser.Execute(id);
                return View(dto);
            }
            catch(Exception)
            {
                return View();
            }
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserDto dto)
        {
           if(!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                _addUser.Execute(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityAlreadyExistsException)
            {
                TempData["error"] = "User with the same name already exists.";
            }
            catch (Exception)
            {
                TempData["error"] = "Some error occurred. Please try again.";
            }
            return View();
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var user = _getUser.Execute(id);
                return View(user);
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                _editUser.Execute(dto);
                return RedirectToAction(nameof(Index));
            }
            catch(NotFoundException)
            {
                return RedirectToAction(nameof(Index));
            }
            catch (EntityAlreadyExistsException)
            {
                TempData["error"] = "User with the same username already exists.";
                return View(dto);
            }
            catch(Exception)
            {
                TempData["error"] = "Role Id is not valid.";
                return View(dto);
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                _deleteUser.Execute(id);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception)
            {
                TempData["error"] = "Some error occurred. Please try again.";
                return RedirectToAction(nameof(Index));
            }
        }

    }
}