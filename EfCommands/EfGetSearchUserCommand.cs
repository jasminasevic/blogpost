using Application.Commands;
using Application.DTO;
using Application.Searches;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfGetSearchUserCommand : EfBaseCommand, IGetSearchUsersCommand
    {
        public EfGetSearchUserCommand(EfContext context) : base(context)
        {
        }

        public IEnumerable<UserDto> Execute(UserSearch request)
        {
            var query = Context.Users.AsQueryable();

            if (request.Keyword != null)
            {
                query = query.Where(u => u.FirstName
                .ToLower()
                .Contains(request.Keyword.ToLower()));
            }

            return query.Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Username = u.Username,
                RoleId = u.RoleId
            });
        }
    }
}
