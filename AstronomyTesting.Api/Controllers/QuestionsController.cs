using AstronomyTesting.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstronomyTesting.Api.Controllers
{
    [ApiController]
    [Route("api/questions/")]
    [Produces("application/json")]
    public class QuestionsController : ControllerBase
    {
        private DataManager _dataManager;

        public QuestionsController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpGet("questionsByTestId")]
        [Route("questionsByTestId/testId={testId}")]
        public async Task<IActionResult> GetQuestionsByTestId(int testId)
        {
            try
            {
                return Ok(_dataManager.Questions.GetQuestionsByTestId(testId).ToList());
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("addQuestion")]
        public async Task<IActionResult> PostAddQuestion([FromBody]dynamic question)
        {
            try
            {
                if (_dataManager.Questions.AddQuestion(Convert.ToInt32(question.TestId), Convert.ToString(question.Name), Convert.FromBase64String(Convert.ToString(question.Photo))))
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

        [HttpGet("containsQuestion")]
        [Route("containsQuestion/testId={testId}&name={name}")]
        public async Task<IActionResult> GetContainsQuestion(int testId, string name)
        {
            try
            {
                if(_dataManager.Questions.ContainsQuestion(testId, name))
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
                return NotFound("");
            }
        }

        [HttpGet("questionById")]
        [Route("questionById/id={id}")]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            try
            {
                return Ok(_dataManager.Questions.GetQuestionById(id));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("removeQuestionById")]
        [Route("removeQuestionById/id={id}")]
        public async Task<IActionResult> DeleteQuestionById(int id)
        {
            try
            {
                _dataManager.Questions.RemoveQuestionById(id);

                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut("updateQuestionById")]
        public async Task<IActionResult> PutQuestionById([FromBody]dynamic question)
        {
            try
            {
                if(_dataManager.Questions.UpdateQuestionById(Convert.ToInt32(question.Id), Convert.ToInt32(question.TestId), Convert.ToString(question.Name), Convert.FromBase64String(Convert.ToString(question.Photo))))
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
