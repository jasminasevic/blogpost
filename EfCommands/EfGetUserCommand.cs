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

        public UserDto Execute(int request)
        {
            var user = Context.Users.Find(request);

            if (user == null)
                throw new NotFoundException();

            var dto = new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Password = user.Password,
                RoleId = user.RoleId
            };

            return dto;
        }
    }
}
