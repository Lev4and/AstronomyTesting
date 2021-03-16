using AstronomyTesting.Model.Entities;
using AstronomyTesting.Model.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstronomyTesting.Model.Repositories.EntityFramework
{
    public class EFQuestionsRepository : IQuestionsRepository
    {
        private AstronomyTestingContext _context;

        public EFQuestionsRepository(AstronomyTestingContext context)
        {
            _context = context;
        }

        public bool AddQuestion(int testId, string name, byte[] photo)
        {
            if(!ContainsQuestion(testId, name))
            {
                _context.Questions.Add(new Question
                {
                    TestId = testId,
                    Name = name,
                    Photo = photo
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool ContainsQuestion(int testId, string name)
        {
            return _context.Questions.SingleOrDefault(q => q.TestId == testId && EF.Functions.Like(q.Name, name)) != null;
        }

        public Question GetQuestionById(int id)
        {
            return _context.Questions.SingleOrDefault(q => q.Id == id);
        }

        public IQueryable<Question> GetQuestionsByTestId(int testId)
        {
            return _context.Questions.Where(q => q.TestId == testId).AsNoTracking();
        }

        public void RemoveQuestionById(int id)
        {
            _context.Questions.Remove(GetQuestionById(id));
            _context.SaveChanges();
        }

        public bool UpdateQuestionById(int id, int testId, string name, byte[] photo)
        {
            var question = GetQuestionById(id);

            if (question.Name != name || question.TestId != testId ? !ContainsQuestion(testId, name) : true)
            {
                question.TestId = testId;
                question.Name = name;
                question.Photo = photo;

                _context.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
