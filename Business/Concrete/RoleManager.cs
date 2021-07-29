using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
  public  class RoleManager:IRoleService
    {
        IRoleDal _roleDal;

        public RoleManager(IRoleDal roledal)
        {
            _roleDal = roledal;
        }

        public List<Role> GetList()
        {
            return _roleDal.List();
        }
    }
}
