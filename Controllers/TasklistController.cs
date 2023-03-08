
using todolistapi.Services;
using todolistapi.Models.DataModels;
using Microsoft.AspNetCore.Mvc;
using todolistapi.Models;
using Microsoft.EntityFrameworkCore;
using todolistapi.Pagination;
using System.Text.Json;

namespace todolistapi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class TaskListController : ControllerBase
{

    //TODO Injeccions
    private readonly TaskListService _taskListService;
    //TODO for pagination
    TodolistContext _dbContext;
    public TaskListController(TaskListService taskListService,TodolistContext dbContext)
    {
        _taskListService = taskListService;
        _dbContext = dbContext;
    }

    //TODO GET ALL PAgination
    [HttpGet("newtasklist")]
    public async Task<IEnumerable<Tasklist>> GetByPagination([FromQuery] string title, [FromQuery] PaginationParams @params )
    {
        var employees = await _dbContext.Tasklists.Where(e => e.Title.ToLower().Trim().Contains(title.ToLower().Trim())).ToListAsync();

        var paginationMetadata = new PaginationMetadata(employees.Count() , @params.Page, @params.ItemsPerPage);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

        var items = employees.Skip((@params.Page - 1) * @params.ItemsPerPage).Take(@params.ItemsPerPage);

        return items;

        
    }


    //TODO GET ALL
    [HttpGet]
    public async Task<IEnumerable<Tasklist>> Get()
    {
        return await _taskListService.GetAll();
    }

    //TODO GET ALL by TITLE
    // http://localhost:5000/api/tasklist/?title=matematica
    [HttpGet("/{title}")]
    public async Task<IEnumerable<Tasklist>> GetByTitle( [FromRoute] string title)
    {
        return await _taskListService.GetAllByTitle(title);
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