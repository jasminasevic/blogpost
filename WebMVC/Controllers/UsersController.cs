using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
using Application.Searches;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IGetUserCommand _getUser;
        private readonly IGetSearchUsersCommand _getSearchUsers;

        public UsersController(IGetUserCommand getUser, 
                               IGetSearchUsersCommand getSearchUsers)
        {
            _getUser = getUser;
            _getSearchUsers = getSearchUsers;
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
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Users/Delete/5
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