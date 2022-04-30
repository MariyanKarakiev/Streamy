using Microsoft.AspNetCore.Identity;
using Streamy.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streamy.Core.Contracts
{
    public interface IUserService
    {
        Task UpdateUser(UserModel userModel);
        Task DeleteUser(string id);


        Task<List<UserModel>> GetAll();
        Task<IdentityUser> GetById(string id);

    }
}
