using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using Application.Helpers;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.IO;
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

            var ext = Path.GetExtension(request.Image.FileName);
            if (!FileUpload.AllowedExtensions.Contains(ext))
            {
                throw new Exception("Extension not ok");
            }

            try
            {
                var newFileName = Guid.NewGuid().ToString() + "_" + request.Image.FileName;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", newFileName);
                request.Image.CopyTo(new FileStream(filePath, FileMode.Create));

                Context.Images.Add(new Domain.Image
                {
                    Alt = request.Title,
                    Path = newFileName
                });

                Context.SaveChanges();
            }

            catch (Exception)
            {
                throw new Exception("File upload failed");
            }


            Context.Posts.Add(new Domain.Post
            {
                Title = request.Title,
                Summary = request.Summary,
                Text = request.Text,
                CategoryId = request.CategoryId,
                UserId = request.UserId,
                ImageId = Context.Images.Max(i => i.Id)
            });

            Context.SaveChanges();

            foreach(var tag in request.AddTagsInPost)
            {
                Context.PostTags.Add(new Domain.PostTag
                {
                    PostId = Context.Posts.Max(p => p.Id),
                    TagId = tag.Id
                });
            }
           
            Context.SaveChanges();
        }

    }
}
