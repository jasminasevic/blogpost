﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class ShowUserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string RoleName { get; set; }

        public IEnumerable<ShowPostDto> showPostDtos { get; set; }
    }
}
