using System.Collections.Generic;
using System.Threading.Tasks;
using CoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreApi.Data
{
    public class AppRepository : IAppRepository
    {
        private readonly DataContext _context;
        public AppRepository(DataContext context)
        {
            _context =context;
        }
        public void Add<T>(T entity) where T : class
        {
           _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
           _context.Remove(entity);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.photos.FirstOrDefaultAsync(p => p.Id == id);
            return photo;
        }

        public async Task<User> GetUser(int id)
        {
            var users= await _context.users.Include(p=>p.Photos).FirstOrDefaultAsync(u=>u.Id==id);
            return users;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
           var users= await _context.users.Include(p=>p.Photos).ToListAsync();
           return users;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync()>0;
        }
    }
}