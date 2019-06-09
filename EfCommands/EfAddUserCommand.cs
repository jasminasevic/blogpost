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
    public class EfAddUserCommand : EfBaseCommand, IAddUserCommand
    {
        public EfAddUserCommand(EfContext context) : base(context)
        {
        }

        public void Execute(UserDto request)
        {
            if (Context.Users.Any(u => u.Username == request.Username))
                throw new EntityAlreadyExistsException();

            Context.Users.Add(new Domain.User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Password = request.Password,
                RoleId = request.RoleId
            });

            Context.SaveChanges();
        }
    }
    
}
