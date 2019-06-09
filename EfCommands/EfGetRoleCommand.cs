using Application.Commands;
using Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using EfDataAccess;
using Application.Exceptions;

namespace EfCommands
{
    public class EfGetRoleCommand : EfBaseCommand, IGetRoleCommand
    {
        public EfGetRoleCommand(EfContext context) : base(context)
        {
        }

        public ShowRoleDto Execute(int request)
        {
            var role = Context.Roles.Find(request);

            if (role == null)
                throw new NotFoundException();

            return new ShowRoleDto
            {
                Name = role.Name
            };

        }
    }
}
