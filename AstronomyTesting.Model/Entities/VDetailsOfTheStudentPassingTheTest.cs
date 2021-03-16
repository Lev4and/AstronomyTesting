using System;
using System.Collections.Generic;

#nullable disable

namespace AstronomyTesting.Model.Entities
{
    public partial class VDetailsOfTheStudentPassingTheTest
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int QuestionId { get; set; }
        public string QuestionName { get; set; }
        public int AnswerId { get; set; }
        public string AnswerName { get; set; }
        public bool AnswerIsCorrect { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
