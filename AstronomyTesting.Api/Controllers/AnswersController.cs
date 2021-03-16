using AstronomyTesting.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstronomyTesting.Api.Controllers
{
    [ApiController]
    [Route("api/answers/")]
    [Produces("application/json")]
    public class AnswersController  : ControllerBase
    {
        private DataManager _dataManager;

        public AnswersController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpGet("answersByQuestionId")]
        [Route("answersByQuestionId/questionId={questionId}")]
        public async Task<IActionResult> GetAnswersByQuestionId(int questionId)
        {
            try
            {
                return Ok(_dataManager.Answers.GetAnswersByQuestionId(questionId).ToList());
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("addAnswer")]
        public async Task<IActionResult> PostAddAnswer([FromBody]dynamic answer)
        {
            try
            {
                if(_dataManager.Answers.AddAnswer(Convert.ToInt32(answer.QuestionId), Convert.ToString(answer.Name), Convert.ToBoolean(answer.IsCorrect)))
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

        [HttpGet("containsAnswer")]
        [Route("containsAnswer/questionId={questionId}&name={name}")]
        public async Task<IActionResult> GetContainsAnswer(int questionId, string name)
        {
            try
            {
                if(_dataManager.Answers.ContainsAnswer(questionId, name))
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

        [HttpGet("getContainsCorrectedAnswersByQuestionId")]
        [Route("getContainsCorrectedAnswersByQuestionId/questionId={questionId}")]
        public async Task<IActionResult> GetContainsCorrectedAnswersByQuestionId(int questionId)
        {
            try
            {
                if (_dataManager.Answers.ContainsCorrectedAnswersByQuestionId(questionId))
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

        [HttpGet("answerById")]
        [Route("answerById/id={id}")]
        public async Task<IActionResult> GetAnswerById(int id)
        {
            try
            {
                return Ok(_dataManager.Answers.GetAnswerById(id));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("removeAnswerById")]
        [Route("removeAnswerById/id={id}")]
        public async Task<IActionResult> DeleteAnswerById(int id)
        {
            try
            {
                _dataManager.Answers.RemoveAnswerById(id);

                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut("updateAnswerById")]
        public async Task<IActionResult> PutAnswerById([FromBody]dynamic answer)
        {
            try
            {
                if (_dataManager.Answers.UpdateAnswerById(Convert.ToInt32(answer.Id), Convert.ToInt32(answer.QuestionId), Convert.ToString(answer.Name), Convert.ToBoolean(answer.IsCorrect)))
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
    }
}
