using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Concrete;

namespace DataAccess.Abstract
{
   public interface IContextDal : IRepository<Context>
    {
    }
}
