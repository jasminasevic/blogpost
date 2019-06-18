﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="This field is required")]
        [RegularExpression("^[A-Z][a-z]{2,20}$", ErrorMessage = "First name is not in a good format")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("^[A-Z][a-z]{2,20}$", ErrorMessage = "Last name is not in a good format")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("^[A-z0-9._%+-]+@[A-z0-9.-]+.[A-z]{2,4}$", ErrorMessage = "Username (email) is not in a good format")]
        public string Username { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }

        public string RoleName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public int RoleId { get; set; }
    }
}
