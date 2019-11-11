using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
using Application.Commands.CategoryCommands;
using Application.Commands.TagCommands;
using Application.Commands.UserCommands;
using Application.DTO;
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
        protected readonly IDeletePostCommand _deletePost;
        protected readonly IAddPostCommand _addPost;
        protected readonly IGetCategoriesWithoutPaginationCommand _getCategories;
        protected readonly IGetUsersWithoutPaginationCommand _getUsers;
        protected readonly IGetTagsWithoutPaginationCommand _getTags;

        public PostsController(IGetPostsCommand getPosts,
            IGetPostCommand getPost,
            IDeletePostCommand deletePost,
            IAddPostCommand addPost,
            IGetCategoriesWithoutPaginationCommand getCategories,
            IGetUsersWithoutPaginationCommand getUsers, 
            IGetTagsWithoutPaginationCommand getTags)
        {
            _getPosts = getPosts;
            _getPost = getPost;
            _deletePost = deletePost;
            _addPost = addPost;
            _getCategories = getCategories;
            _getUsers = getUsers;
            _getTags = getTags;
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
        public ActionResult Create([FromQuery] GeneralSearchQuery search)
        {
            ViewBag.Categories = _getCategories.Execute(search);
            ViewBag.Users = _getUsers.Execute(search);
            ViewBag.Tags = _getTags.Execute(search);
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostDto dto)
         {
            try
            {
                // TODO: Add insert logic here
                _addPost.Execute(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityAlreadyExistsException)
            {
                TempData["error"] = "Post with the same title already exists";
                return View();
            }
            catch (NotFoundException)
            {
                TempData["error"] = "The title has to be added";
                return View();
            }
            catch
            {
                TempData["error"] = "Something went wrong. Please try again.";
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
            try
            {
                // TODO: Add delete logic here
                _deletePost.Execute(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["error"] = "Some error occured. Please try again.";
                return View();
            }
        }

    }
}