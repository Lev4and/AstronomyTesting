using AstronomyTesting.Model.Entities;
using System.Linq;

namespace AstronomyTesting.Model.Repositories.Abstract
{
    public interface IStudentAnswersRepository
    {
        bool AddStudentAnswer(int studentId, int answerId);

        bool ContainsStudentAnswer(int studentId, int answerId);

        StudentAnswer GetStudentAnswerByAnswerId(int studentId, int answerId);

        StudentAnswer GetStudentAnswerByQuestionId(int studentId, int questionId);

        IQueryable<StudentAnswer> GetStudentAnswers(int studentId, int testId);

        void RemoveStudentAnswer(int studentId, int answerId);
    }
}
