using System;
using System.Collections.Generic;

#nullable disable

namespace AstronomyTesting.Model.Entities
{
    public partial class Answer
    {
        public Answer()
        {
            StudentAnswers = new HashSet<StudentAnswer>();
        }

        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Name { get; set; }
        public bool IsCorrect { get; set; }

        public virtual Question Question { get; set; }
        public virtual ICollection<StudentAnswer> StudentAnswers { get; set; }
    }
}
