using Application.Commands;
using Application.DTO;
using Application.Interfaces;
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
    public class EfGetTagsCommand : EfBaseCommand, IGetTagsCommand
    {
        public EfGetTagsCommand(EfContext context) : base(context)
        {
        }

        public IEnumerable<ShowPostDtos> Execute(TagQuery request)
        {
            var tags = Context.Tags.AsQueryable();

            if (request.Name != null)
                tags = tags.Where(t => t.Name.ToLower().Contains(request.Name.ToLower()));

            return tags
                .Include(t => t.PostTags)
                .ThenInclude(pt => pt.Post)
                .Select(t => new ShowPostDtos
                {
                    Name = t.Name,
                    Title = t.PostTags.Select(pt => pt.Post.Title).ToList()
                });

        }

    }
}
