using AstronomyTesting.Model.Entities;
using System.Linq;

namespace AstronomyTesting.Model.Repositories.Abstract
{
    public interface IQuestionsRepository
    {
        bool AddQuestion(int testId, string name, byte[] photo);

        bool ContainsQuestion(int testId, string name);

        bool UpdateQuestionById(int id, int testId, string name, byte[] photo);

        Question GetQuestionById(int id);

        IQueryable<Question> GetQuestionsByTestId(int testId);

        void RemoveQuestionById(int id);
    }
}
