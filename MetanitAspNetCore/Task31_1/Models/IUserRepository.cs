using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task31_1.Models
{
    public interface IUserRepository
    {
        void Create(User user);
        void Delete(int id);
        User Get(int id);
        List<User> GetUsers();
        void Update(User user);
    }
}
