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
    public class EfAddPostCommand : EfBaseCommand, IAddPostCommand
    {
        public EfAddPostCommand(EfContext context) : base(context)
        {
        }

        public void Execute(PostDto request)
        {
            if (Context.Posts.Any(p => p.Title == request.Title))
                throw new EntityAlreadyExistsException();

            Context.Posts.Add(new Domain.Post
            {
                Title = request.Title,
                Summary = request.Summary,
                Text = request.Text,
                CategoryId = request.CategoryId,
                UserId = request.UserId,
                ImageId = request.ImageId,
            });

            Context.SaveChanges();
        }
    }
}
