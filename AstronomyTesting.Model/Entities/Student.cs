using System;
using System.Collections.Generic;

#nullable disable

namespace AstronomyTesting.Model.Entities
{
    public partial class Student
    {
        public Student()
        {
            StudentAnswers = new HashSet<StudentAnswer>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }

        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<StudentAnswer> StudentAnswers { get; set; }
    }
}
