using System;
using System.Collections.Generic;

#nullable disable

namespace AstronomyTesting.Model.Entities
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
        }

        public int Id { get; set; }
        public int TestId { get; set; }
        public string Name { get; set; }
        public byte[] Photo { get; set; }

        public virtual Test Test { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
