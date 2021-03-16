using System;
using System.Collections.Generic;

#nullable disable

namespace AstronomyTesting.Model.Entities
{
    public partial class VDetailsOfTheTest
    {
        public int TestId { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public DateTime? TestDateTime { get; set; }
        public int? CountQuestions { get; set; }
        public int MaximumNumberOfQuestions { get; set; }
        public int? CountAnswers { get; set; }
        public int? CountCorrectedAnswers { get; set; }
    }
}
