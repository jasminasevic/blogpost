using Application.Commands;
using Application.DTO;
using Application.Queries;
using Application.Responses;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfGetPostsCommand : EfBaseCommand, IGetPostsCommand
    {
        public EfGetPostsCommand(EfContext context) : base(context)
        {
        }

        public PageResponses<PostDto> Execute(PostQuery request)
        {
            var posts = Context.Posts.AsQueryable();

            if (request.Title != null)
                posts = posts.Where(p => p.Title.ToLower().Contains(request.Title.ToLower()));

            var totalCount = posts.Count();

            posts = posts.Skip((request.PageNumber - 1) * request.PerPage).Take(request.PerPage);

            var pagesCount = (int)Math.Ceiling((double)totalCount / request.PerPage);

            return new PageResponses<PostDto>
            {
                CurrentPage = request.PageNumber,
                PagesCount = pagesCount,
                TotalCount = totalCount,
                Data = posts.Select(p => new PostDto
                {
                    Title = p.Title,
                    Summary = p.Summary,
                    Text = p.Text,
                    FirstName = p.User.FirstName,
                    LastName = p.User.LastName,
                    Category = p.Category.Name
                })
            };
        }
    }
}
