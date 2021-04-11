using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task26_1.Models
{
    public interface IRepository
    {
        IEnumerable<User> GetAll();
        User Get(int id);
        void Create(User user);
    }
}
