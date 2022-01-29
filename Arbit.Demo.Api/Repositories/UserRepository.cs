using Arbit.Demo.Api.Database;
using Arbit.Demo.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arbit.Demo.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MyWebApiContext _context;

        public UserRepository(MyWebApiContext context)
        {
            _context = context;
        }
        public async Task<User> Create(User user)
        {
            user.CreatedAt = DateTime.Now;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<List<User>> GetAll()
        {
            return await _context.Users
                .Where(u => u.DeletedAt == null)
                .ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users
                .Where(u => u.DeletedAt == null)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> Update(int id, User user)
        {
            var updateUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if(updateUser != null)
            {
                updateUser.UpdatedAt = DateTime.Now;
                updateUser.Name = user.Name;
                updateUser.Email = user.Email;
                _context.Users.Update(updateUser);
                await _context.SaveChangesAsync();
            }
            return updateUser;
        }


        public async Task<User> Delete(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                if(user != null)
                {
                    user.DeletedAt = DateTime.Now;
                    await _context.SaveChangesAsync();
                }
                return user;
        }
    }
}

