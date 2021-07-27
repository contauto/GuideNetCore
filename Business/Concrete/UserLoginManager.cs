﻿using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserLoginManager : IUserLoginService
    {
        IUserDal _userDal;

        public UserLoginManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public User GetUser(string username, string password)
        {
            return _userDal.Get(x => x.UserMail == username && x.UserPassword == password);
        }
    }
}
