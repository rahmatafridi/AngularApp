using System.Threading.Tasks;
using System.Collections.Generic;
using CoreApi.Models;

namespace CoreApi.Data
{
    public interface IAppRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T:class;
         Task<bool> SaveAll();
         Task<IEnumerable<User>> GetUsers();
         Task<User> GetUser(int id);

        Task<Photo> GetPhoto(int id);
    }
}