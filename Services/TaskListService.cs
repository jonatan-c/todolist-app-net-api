
using todolistapi.Models;
using todolistapi.Interfaces;
using todolistapi.Models.DataModels;
using todolistapi.Repositories;

namespace todolistapi.Services

{




public class TaskListService
{
    private readonly IRepository<Tasklist> _taskListRepository;
    public TaskListService(IRepository<Tasklist> taskListRepository)
    {
        _taskListRepository = taskListRepository;
    }

    //TODO POST
    public async Task<Tasklist> Create(Tasklist _object)
    {
        return await _taskListRepository.Create(_object);
    }

    //TODO DELETE
    public async Task Delete(int id)
    {

        await _taskListRepository.Delete(id);

    }

    public async Task<IEnumerable<Tasklist>> GetAll()
    {
        return await _taskListRepository.GetAll();
    }

    public async Task<Tasklist> GetById(int Id)
    {
        return await _taskListRepository.GetById(Id);
    }

    public async Task Update(int id, Tasklist _object)
    {
        await _taskListRepository.Update(id, _object);
    }
}

}