using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfEditPostCommand : EfBaseCommand, IEditPostCommand
    {
        public EfEditPostCommand(EfContext context) : base(context)
        {
        }

        public void Execute(PostDto request)
        {
            var post = Context.Posts.Find(request.Id);

            if (post == null)
                throw new NotFoundException();

            //if (request.Title != post.Title && Context.Posts.Any(p => p.Title == request.Title))
            //    throw new EntityAlreadyExistsException();

            post.Title = request.Title;
            post.Summary = request.Summary;
            post.Text = request.Text;
            post.ImageId = request.ImageId;
            post.UserId = request.UserId;
            post.CategoryId = request.CategoryId;

            Context.SaveChanges();
        }
    }
}
