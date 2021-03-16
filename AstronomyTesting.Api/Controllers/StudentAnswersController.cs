using AstronomyTesting.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AstronomyTesting.Api.Controllers
{
    [ApiController]
    [Route("api/studentAnswers/")]
    [Produces("application/json")]
    public class StudentAnswersController : ControllerBase
    {
        private DataManager _dataManager;

        public StudentAnswersController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpPost("addStudentAnswer")]
        public async Task<IActionResult> PostAddStudentAnswer([FromBody]dynamic studentAnswer)
        {
            try
            {
                if(_dataManager.StudentAnswers.AddStudentAnswer(Convert.ToInt32(studentAnswer.StudentId), Convert.ToInt32(studentAnswer.AnswerId)))
                {
                    return Ok(true);
                }
                else
                {
                    return Conflict();
                }
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("containsStudentAnswer")]
        [Route("containsStudentAnswer/studentId={studentId}&answerId={answerId}")]
        public async Task<IActionResult> GetContainsStudentAnswer(int studentId, int answerId)
        {
            try
            {
                if(_dataManager.StudentAnswers.ContainsStudentAnswer(studentId, answerId))
                {
                    return Ok(true);
                }
                else
                {
                    return Conflict();
                }
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("studentAnswerByAnswerId")]
        [Route("studentAnswerByAnswerId/studentId={studentId}&answerId={answerId}")]
        public async Task<IActionResult> GetStudentAnswerByAnswerId(int studentId, int answerId)
        {
            try
            {
                return Ok(_dataManager.StudentAnswers.GetStudentAnswerByAnswerId(studentId, answerId));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("studentAnswerByQuestionId")]
        [Route("studentAnswerByQuestionId/studentId={studentId}&questionId={questionId}")]
        public async Task<IActionResult> GetStudentAnswerByQuestionId(int studentId, int questionId)
        {
            try
            {
                return Ok(_dataManager.StudentAnswers.GetStudentAnswerByQuestionId(studentId, questionId));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("studentAnswers")]
        [Route("studentAnswers/studentId={studentId}&testId={testId}")]
        public async Task<IActionResult> GetStudentAnswers(int studentId, int testId)
        {
            try
            {
                return Ok(_dataManager.StudentAnswers.GetStudentAnswers(studentId, testId).ToList());
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("removeStudentAnswer")]
        [Route("removeStudentAnswer/studentId={studentId}&answerId={answerId}")]
        public async Task<IActionResult> DeleteStudentAnswer(int studentId, int answerId)
        {
            try
            {
                _dataManager.StudentAnswers.RemoveStudentAnswer(studentId, answerId);

                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
