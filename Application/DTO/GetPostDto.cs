using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO
{
    public class GetPostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Summary { get; set; }

        public string Text { get; set; }

        [Display(Name="User")]
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Display(Name="Category")]
        public int CategoryId { get; set; }

        public string Category { get; set; }

        [Display(Name = "Image")]
        public int ImageId { get; set; }

        public string Image { get; set; }

        public IFormFile ImageFile { get; set; }

        [Display(Name = "Tags")]
        public IEnumerable<ShowTagInPosts> ShowTagInPosts { get; set; }

    }
}
