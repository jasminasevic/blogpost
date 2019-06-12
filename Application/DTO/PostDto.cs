using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class PostDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string Text { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Category { get; set; }

        public int ImageId { get; set; }

        public int CategoryId { get; set; }

        public int UserId { get; set; }
    }
}
