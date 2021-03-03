using System;
using System.Collections.Generic;

#nullable disable

namespace AstronomyTesting.Model.Entities
{
    public partial class StudentAnswer
    {
        public int StudentId { get; set; }
        public int AnswerId { get; set; }
        public DateTime Date { get; set; }

        public virtual Answer Answer { get; set; }
        public virtual Student Student { get; set; }
    }
}
