using System;
using System.Collections.Generic;
using System.Text;
using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using EfDataAccess;

namespace EfCommands
{
    public class EfGetTagCommand : EfBaseCommand, IGetTagCommand
    {
        public EfGetTagCommand(EfContext context) : base(context)
        {
        }

        public ShowTagDto Execute(int request)
        {
            var tag = Context.Tags.Find(request);

            if (tag == null)
                throw new NotFoundException();

            return new ShowTagDto
            {
                Name = tag.Name
            };
        }
    }
}
