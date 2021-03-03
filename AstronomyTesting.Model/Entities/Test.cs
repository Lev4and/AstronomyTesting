using System;
using System.Collections.Generic;

#nullable disable

namespace AstronomyTesting.Model.Entities
{
    public partial class Test
    {
        public Test()
        {
            Questions = new HashSet<Question>();
        }

        public int Id { get; set; }
        public int Name { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCreating { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int? Duration { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
