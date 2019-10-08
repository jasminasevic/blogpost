using Application.Commands;
using Application.DTO;
using Application.Queries;
using Application.Responses;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfGetUsersCommand : EfBaseCommand, IGetUsersCommand
    {

        public EfGetUsersCommand(EfContext context) : base(context)
        {
        }

        public PageResponses<ShowUserDto> Execute(UserQuery request)
        {
            var users = Context.Users
                .Include(r => r.Role)
                .Include(p => p.Posts)
                .ThenInclude(pt => pt.PostTags)
                .AsQueryable();

            if (request.FirstName != null)
                users = users.Where(u => u.FirstName.ToLower().Contains(request.FirstName.ToLower()));

            if (request.LastName != null)
                users = users.Where(u => u.LastName.ToLower().Contains(request.LastName.ToLower()));

            if (request.Username != null)
                users = users.Where(u => u.Username.ToLower().Contains(request.Username.ToLower()));

            if (request.RoleName != null)
                users = users.Where(u => u.Role.Name.ToLower().Contains(request.RoleName.ToLower()));

            var totalCount = users.Count();

            users = users.Skip((request.PageNumber - 1) * request.PerPage).Take(request.PerPage);

            var pagesCount = (int)Math.Ceiling((double)totalCount / request.PerPage);

            return new PageResponses<ShowUserDto>
            {
                CurrentPage = request.PageNumber,
                PagesCount = pagesCount,
                TotalCount = totalCount,
                Data = users.Select(u => new ShowUserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Username = u.Username,
                    RoleName = u.Role.Name,
                    showPostDtos = u.Posts.Select(p => new ShowPostDto
                    {
                        Title = p.Title,
                        Summary = p.Summary,
                        Text = p.Text,
                        Category = p.Category.Name,
                        showTagDtos = p.PostTags.Select(t => new ShowTagDto
                        {
                            Name = t.Tag.Name
                        })
                    })
                })
            };
        }
    }
}
