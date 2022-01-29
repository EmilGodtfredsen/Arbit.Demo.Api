using Arbit.Demo.Api.Models;
using Arbit.Demo.Api.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arbit.Demo.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateUser(User user)
        {
            var createUser = await _userRepository.Create(user);
            return user;
        }
        public async Task<List<User>> GetAllUsers()
        {
            var users = await _userRepository.GetAll();
            return users;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _userRepository.GetById(id);
            return user;
        }

        public async Task<User> UpdateUser(int id, User user)
        {
            var updateUser = await _userRepository.Update(id, user);
            return updateUser;
        }

        public async Task<User> DeleteUser(int id)
        {
            var user = await _userRepository.Delete(id);
            return user;
        }

    }
}
