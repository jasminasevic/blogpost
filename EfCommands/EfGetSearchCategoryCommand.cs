using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Commands;
using Application.DTO;
using Application.Searches;
using EfDataAccess;

namespace EfCommands
{
    public class EfGetSearchCategoryCommand : EfBaseCommand, IGetSearchCategoriesCommand
    {
        public EfGetSearchCategoryCommand(EfContext context) : base(context)
        {
        }


        public IEnumerable<CategoryDto> Execute(CategorySearch request)
        {
            var query = Context.Categories.AsQueryable();

            if (request.Keyword != null)
            {
                query = query.Where(c => c.Name
                .ToLower()
                .Contains(request.Keyword.ToLower()));
            }

            return query.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            });
        }
    }
}
