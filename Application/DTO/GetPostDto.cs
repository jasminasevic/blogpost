using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO
{
    public class GetPostDto
    {
        public string Title { get; set; }

        public string Summary { get; set; }

        public string Text { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Category { get; set; }

        public int ImageId { get; set; }

        public IEnumerable<ShowTagInPosts> ShowTagInPosts { get; set; }

    }
}
