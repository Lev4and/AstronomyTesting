using AstronomyTesting.Model.Entities;
using System.Linq;

namespace AstronomyTesting.Model.Repositories.Abstract
{
    public interface IAnswersRepository
    {
        bool AddAnswer(int questionId, string name, bool isCorrect);

        bool ContainsAnswer(int questionId, string name);

        bool ContainsCorrectedAnswersByQuestionId(int questionId);

        bool UpdateAnswerById(int id, int questionId, string name, bool isCorrect);

        Answer GetAnswerById(int id);

        IQueryable<Answer> GetAnswersByQuestionId(int questionId);

        void RemoveAnswerById(int id);
    }
}
