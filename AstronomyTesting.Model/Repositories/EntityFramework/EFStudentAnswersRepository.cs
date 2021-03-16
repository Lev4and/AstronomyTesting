using AstronomyTesting.Model.Entities;
using AstronomyTesting.Model.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AstronomyTesting.Model.Repositories.EntityFramework
{
    public class EFStudentAnswersRepository : IStudentAnswersRepository
    {
        private AstronomyTestingContext _context;

        public EFStudentAnswersRepository(AstronomyTestingContext context)
        {
            _context = context;
        }

        public bool AddStudentAnswer(int studentId, int answerId)
        {
            if(!ContainsStudentAnswer(studentId, answerId))
            {
                _context.StudentAnswers.Add(new StudentAnswer
                {
                    StudentId = studentId,
                    AnswerId = answerId,
                    Date = DateTime.Now
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool ContainsStudentAnswer(int studentId, int answerId)
        {
            return _context.StudentAnswers.SingleOrDefault(sa => sa.StudentId == studentId && sa.AnswerId == answerId) != null;
        }

        public StudentAnswer GetStudentAnswerByAnswerId(int studentId, int answerId)
        {
            return _context.StudentAnswers.SingleOrDefault(sa => sa.StudentId == studentId && sa.AnswerId == answerId);
        }

        public StudentAnswer GetStudentAnswerByQuestionId(int studentId, int questionId)
        {
            return _context.StudentAnswers.Include(sa => sa.Answer).SingleOrDefault(sa => sa.StudentId == studentId && sa.Answer.QuestionId == questionId);
        }

        public IQueryable<StudentAnswer> GetStudentAnswers(int studentId, int testId)
        {
            return _context.StudentAnswers.Include(sa => sa.Answer).Include(sa => sa.Answer.Question).Include(sa => sa.Answer.Question.Test).Where(sa => sa.StudentId == studentId && sa.Answer.Question.Test.Id == testId);
        }

        public void RemoveStudentAnswer(int studentId, int answerId)
        {
            _context.StudentAnswers.Remove(GetStudentAnswerByAnswerId(studentId, answerId));
            _context.SaveChanges();
        }
    }
}
