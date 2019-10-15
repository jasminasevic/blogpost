using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
using Application.Exceptions;
using Application.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class PostsController : Controller
    {
        protected readonly IGetPostsCommand _getPosts;
        protected readonly IGetPostCommand _getPost;

        public PostsController(IGetPostsCommand getPosts, IGetPostCommand getPost)
        {
            _getPosts = getPosts;
            _getPost = getPost;
        }

        // GET: Posts
        public ActionResult Index(PostQuery search)
        {
            var posts = _getPosts.Execute(search);
            return View(posts);
        }

        // GET: Posts/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var dto = _getPost.Execute(id);
                return View(dto);
            }
            catch (NotFoundException)
            {
                TempData["error"] = "There is no post with that id.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["error"] = "Something went wrong. Please try again.";
                return View();
            }
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
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

        // GET: Posts/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Posts/Edit/5
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

        // GET: Posts/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

    }
}