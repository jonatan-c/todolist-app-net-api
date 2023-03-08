using Microsoft.AspNetCore.Mvc;
using todolistapi.Models.DataModels;
  
namespace todolistapi.Interfaces
{



    public interface IRepository<Tasklist>  
    {  
        public Task<IEnumerable<Tasklist>> GetAll();

        public Task<IEnumerable<Tasklist>> GetAllByTitle(string title);
  
        public Task<Tasklist> GetById(int Id);

        public Task<Tasklist> Create(Tasklist _object);

        public Task Update(int id, Tasklist _object);
  

        public Task Delete(int id);
    }  
}