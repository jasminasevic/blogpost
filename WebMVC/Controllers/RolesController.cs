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

namespace WebMVC.Controllers
{
    public class RolesController : Controller
    {
        protected readonly IGetRolesCommand _getRoles;
        protected readonly IGetRoleCommand _getRole;
        protected readonly IAddRoleCommand _addRole;
        protected readonly IEditRoleCommand _editRole;

        public RolesController(IGetRolesCommand getRoles, IGetRoleCommand getRole, IAddRoleCommand addRole, IEditRoleCommand editRole)
        {
            _getRoles = getRoles;
            _getRole = getRole;
            _addRole = addRole;
            _editRole = editRole;
        }

        // GET: Roles
        public ActionResult Index(RoleQuery query)
        {
            var roles = _getRoles.Execute(query);
            return View(roles);
        }

        // GET: Roles/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var role = _getRole.Execute(id);
                return View(role);
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoleDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                // TODO: Add insert logic here
                _addRole.Execute(dto);
                return RedirectToAction(nameof(Index));
            }
            catch(EntityAlreadyExistsException)
            {
                TempData["error"] = "Role with the same name already exists";
            }
            catch (Exception)
            {
                TempData["error"] = "Some error occurred. Please try again.";
            }
            return View();
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var dto = _getRole.Execute(id);
                return View(dto);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }

        // POST: Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RoleDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                // TODO: Add update logic here
                _editRole.Execute(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return RedirectToAction(nameof(Index));
            }
            catch (EntityAlreadyExistsException)
            {
                TempData["error"] = "Role with the same name already exists.";
                return View(dto);
            }
            catch (Exception)
            {
                TempData["error"] = "Something went wrong. Please try again.";
                return View(dto);
            }
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Roles/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}