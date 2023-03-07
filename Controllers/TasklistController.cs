
using todolistapi.Services;
using todolistapi.Models.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace todolistapi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class TaskListController : ControllerBase
{

    //TODO Injeccions
    private readonly TaskListService _taskListService;
    public TaskListController(TaskListService taskListService)
    {
        _taskListService = taskListService;
    }


    //TODO GET ALL
    [HttpGet]
    public async Task<IEnumerable<Tasklist>> Get()
    {
        return await _taskListService.GetAll();
    }

    //TODO GET BY ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Tasklist>> GetById(int id)
    {
        var tasklistselected = await _taskListService.GetById(id);

        if (tasklistselected == null)
        {
            return NotFound(new { message = $"El task con ID = {id} no existe." });
        }

        return tasklistselected;

    }

    //TODO POST
    [HttpPost]
    public async Task<IActionResult> Create(Tasklist taskParam)
    {
        var newTask = await _taskListService.Create(taskParam);

        return CreatedAtAction(nameof(GetById), new { id = newTask.Id }, newTask);
    }

    //TODO PUT
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Tasklist taskParam)
    {
        if (id != taskParam.Id)
        {
            return BadRequest(new { message = $"El ID({id}) de la URL no coincide con el ID({taskParam.Id} del cuerpo de la solicitud. )" });
        }

        var clientToUpdate = await _taskListService.GetById(id);

        if (clientToUpdate == null)
        {
            // return ClientNotFound(id);
            return NotFound(new { message = $"El taks con ID = {id} no existe." });
        }

        await _taskListService.Update(id, taskParam);
        return NoContent();

    }


    //TODO DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var clientToDelete = await _taskListService.GetById(id);

        if (clientToDelete == null)
        {
            // return ClientNotFound(id);
            return NotFound(new { message = $"El task con ID = {id} no existe." });
        }

        await _taskListService.Delete(id);
        return Ok();
    }

}