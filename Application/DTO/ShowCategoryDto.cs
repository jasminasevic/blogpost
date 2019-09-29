using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class ShowCategoryDto
    {
        public string Name { get; set; }

        public IEnumerable<ShowPostInCategoryDto> ShowPostInCategoryDtos { get; set; }
    }
}
