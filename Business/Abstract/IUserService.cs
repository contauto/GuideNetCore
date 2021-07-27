using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
   public interface IUserService
    {
        List<User> GetList();
        void UserAdd(User user);
        void UserDelete(User user);
        void UserUpdate(User user);
        User GetById(int id);
    }
}
