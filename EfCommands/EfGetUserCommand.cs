using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public class EfGetUserCommand : EfBaseCommand, IGetUserCommand
    {
        public EfGetUserCommand(EfContext context) : base(context)
        {
        }

        public ShowUserDto Execute(int request)
        {
            var user = Context.Users.Find(request);

            if (user == null)
                throw new NotFoundException();

            var dto = new ShowUserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                RoleName = Context.Roles.Find(user.RoleId).Name
            };

            return dto;
        }
    }
}
