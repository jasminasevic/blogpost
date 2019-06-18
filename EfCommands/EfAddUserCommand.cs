using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using Application.Interfaces;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfAddUserCommand : EfBaseCommand, IAddUserCommand
    {
        private readonly IEmailSender _emailSender;

        public EfAddUserCommand(EfContext context, IEmailSender emailSender) : base(context)
        {
            _emailSender = emailSender;
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

            _emailSender.Subject = "Uspesna registracija";
            _emailSender.Body = "Uspesno ste se registrovali.";
            _emailSender.ToEmail = request.Username;
            _emailSender.Send();

        }
    }
    
}
