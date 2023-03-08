
using todolistapi.Models;
using todolistapi.Models.DataModels;
using todolistapi.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace todolistapi.Repositories

{



public class TaskListRepository : IRepository<Tasklist>
{
    TodolistContext _dbContext;
    public TaskListRepository(TodolistContext dbContext)
    {
        _dbContext = dbContext;
    }

    //TODO GET ALL
      public async Task<IEnumerable<Tasklist>> GetAll()
    {
        // var sql = "SELECT * FROM tasklist";
        // var result = await _dbContext.Tasklists.FromSqlRaw(sql).ToListAsync();
        // return result;
        return await _dbContext.Tasklists.ToListAsync();
    }

     //TODO GET ALL by TITLE
    // http://localhost:5000/api/tasklist/?title=matematica
    public async Task<IEnumerable<Tasklist>> GetAllByTitle(string title)
    {
        var sql = $"SELECT * FROM tasklist WHERE title LIKE '%{title}%'";
        var result = await _dbContext.Tasklists.FromSqlRaw(sql).ToListAsync();
        return result;
    }

    //TODO GET BY ID
     public async Task<Tasklist> GetById(int Id)
    {
        var tarea =   await _dbContext.Tasklists.FindAsync(Id);
        return tarea;
    }


    //TODO POST
    public async Task<Tasklist> Create(Tasklist _object)
    {
        // make this but wiht SQL sentence
        var obj = await _dbContext.Tasklists.AddAsync(_object);
        await  _dbContext.SaveChangesAsync();
        return obj.Entity;
    }

    //TODO DELETE 
    public async Task Delete(int id)
    {
        var search = await _dbContext.Tasklists.FindAsync(id);
        if (search == null)
        {
            return;
        }

        _dbContext.Tasklists.Remove(search);
        await _dbContext.SaveChangesAsync();

    }

  
   
    //TODO PUT
    public async Task Update(int id, Tasklist _object)
    {
        var tasklistsearch = await _dbContext.Tasklists.FindAsync(id);

        if (tasklistsearch == null)
        {
            return;
        }

        tasklistsearch.Title = _object.Title;
        tasklistsearch.Description = _object.Description;
        tasklistsearch.Priority = _object.Priority;
        tasklistsearch.Status = _object.Status;


        await _dbContext.SaveChangesAsync();
    }
}

}