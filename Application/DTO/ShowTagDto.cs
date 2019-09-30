using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class ShowTagDto
    {
        public string Name { get; set; }

        public IEnumerable<ShowPostInTagDto> ShowPostInTagDto { get; set; }
    }
}
