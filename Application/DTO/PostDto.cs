﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO
{
    public class PostDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MinLength(3, ErrorMessage = "Post title must have at least 3 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Text { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Category { get; set; }
        
        public int ImageId { get; set; }

        public IEnumerable<TagDto> TagDtos { get; set; }

        [Required(ErrorMessage = "This fiels is required")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public int UserId { get; set; }
    }
}
