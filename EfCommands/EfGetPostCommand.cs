using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public class EfGetPostCommand : EfBaseCommand, IGetPostCommand
    {
        public EfGetPostCommand(EfContext context) : base(context)
        {
        }

        public ShowPostDto Execute(int request)
        {
            var post = Context.Posts.Find(request);

            if (post == null)
                throw new NotFoundException();

            var dto = new ShowPostDto
            {
                Title = post.Title,
                Summary = post.Summary,
                Text = post.Text,
               // Category = Context.Categories.Find(post.CategoryId).Name,
               // FirstName = Context.Users.Find(post.Id).FirstName,
                //LastName = Context.Users.Find(post.Id).LastName
            };

            return dto;
        }
    }
}
