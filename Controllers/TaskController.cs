using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskData _taskData;

        public TaskController(TaskData taskData)
        {
            _taskData = taskData;
        }

        [HttpGet]
        public async Task<IActionResult> ListTask()
        {
            List<TaskM> listT =  await _taskData.ListTask();
            return StatusCode(StatusCodes.Status200OK, listT);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            TaskM listT = await _taskData.GetTask(id);
            return StatusCode(StatusCodes.Status200OK, listT);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody]TaskM taskm)
        {
            bool result = await _taskData.CreateTask(taskm);
            return StatusCode(StatusCodes.Status200OK, new {isSuccess = result});
        }

        [HttpPut]
        public async Task<IActionResult> EditTask([FromBody] TaskM taskm)
        {
            bool result = await _taskData.EditTask(taskm);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = result });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletTask(int id)
        {
            bool result = await _taskData.DeleteTask(id);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = result });
        }
    }
}
