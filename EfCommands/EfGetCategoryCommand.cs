using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public class EfGetCategoryCommand : EfBaseCommand, IGetCategoryCommand
    {
        public EfGetCategoryCommand(EfContext context) : base(context)
        {
        }

        public ShowCategoryDto Execute(int request)
        {
            var category = Context.Categories.Find(request);

            if (category == null)
                throw new NotFoundException();

            return new ShowCategoryDto
            {
                Name = category.Name
            };
        }
    }
}
