using AstronomyTesting.Model.Entities;
using AstronomyTesting.Model.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AstronomyTesting.Model.Repositories.EntityFramework
{
    public class EFAnswersRepository : IAnswersRepository
    {
        private AstronomyTestingContext _context;

        public EFAnswersRepository(AstronomyTestingContext context)
        {
            _context = context;
        }

        public bool AddAnswer(int questionId, string name, bool isCorrect)
        {
            if(!ContainsAnswer(questionId, name))
            {
                if (isCorrect)
                {
                    if (ContainsCorrectedAnswersByQuestionId(questionId))
                    {
                        return false;
                    }
                }

                _context.Answers.Add(new Answer
                {
                    QuestionId = questionId,
                    Name = name,
                    IsCorrect = isCorrect
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool ContainsAnswer(int questionId, string name)
        {
            return _context.Answers.SingleOrDefault(a => a.QuestionId == questionId && a.Name == name) != null;
        }

        public bool ContainsCorrectedAnswersByQuestionId(int questionId)
        {
            return _context.Answers.Any(a => a.QuestionId == questionId && a.IsCorrect == true);
        }

        public Answer GetAnswerById(int id)
        {
            return _context.Answers.SingleOrDefault(a => a.Id == id);
        }

        public IQueryable<Answer> GetAnswersByQuestionId(int questionId)
        {
            return _context.Answers.Where(a => a.QuestionId == questionId).AsNoTracking();
        }

        public void RemoveAnswerById(int id)
        {
            _context.Answers.Remove(GetAnswerById(id));
            _context.SaveChanges();
        }

        public bool UpdateAnswerById(int id, int questionId, string name, bool isCorrect)
        {
            var answer = GetAnswerById(id);

            if(answer.QuestionId != questionId || answer.Name != name || answer.IsCorrect != isCorrect ? (answer.QuestionId != questionId || answer.Name != name ? !ContainsAnswer(questionId, name) : true) : true)
            {
                if (answer.IsCorrect != isCorrect && isCorrect)
                {
                    if (ContainsCorrectedAnswersByQuestionId(questionId))
                    {
                        return false;
                    }
                }

                answer.QuestionId = questionId;
                answer.Name = name;
                answer.IsCorrect = isCorrect;

                _context.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
