using Microsoft.AspNetCore.Identity;
using Streamy.Core.Contracts;
using Streamy.Core.Models;
using Streamy.Infrastructure.Data.Repositories;


namespace Streamy.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbRepository _repo;

        public UserService(IApplicationDbRepository repo)
        {
            _repo = repo;
        }


        public async Task DeleteUser(string id)
        {
            await _repo.DeleteAsync<IdentityUser>(id);
            await _repo.SaveChangesAsync();
        }
        public async Task UpdateUser(UserModel userModel)
        {
            if (userModel == null)
            {
                throw new ArgumentNullException("Invalid model.", nameof(userModel));
            }

            var user = _repo.All<IdentityUser>().FirstOrDefault(g => g.Id == userModel.Id);

            if (user == null)
            {
                throw new ArgumentNullException("User not found", nameof(user));
            }

            user.UserName = userModel.UserName;

            _repo.Update(user);
            _repo.SaveChanges();
        }


        public async Task<List<UserModel>> GetAll()
        {
            return _repo.All<IdentityUser>()
                .Select(u => new UserModel()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                })
                .ToList();
        }

        public async Task<IdentityUser> GetById(string id)
        {
            var user = _repo.All<IdentityUser>().FirstOrDefault(g => g.Id == id);

          return user;

        }
    }
}
