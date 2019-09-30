using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreatedAt = DateTime.Now;

        public DateTime? ModifiedAt { get; set; }

        public bool IsActive = true;

        public bool IsDeleted = false;
    }
}
