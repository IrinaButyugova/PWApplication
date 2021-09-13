using Microsoft.EntityFrameworkCore;
using PWApplication.DAL.Data;
using PWApplication.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PWApplication.DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly ApplicationContext _appContext;

        public UserRepository(ApplicationContext appContext)
        {
            _appContext = appContext;
        }

        public IEnumerable<User> GetAll()
        {
            return _appContext.Users
                .Include(x => x.Transactions)
                .ThenInclude(x => x.Correspondent);
        }

        public User Get(string id)
        {
            return _appContext.Users.Where(x => x.Id == id).FirstOrDefault();
        }

        public User Get(int id)
        {
            return Get(id.ToString());
        }

        public User GetByUserName(string name)
        {
           return GetAllByUserName(name).FirstOrDefault();
        }
        
        public IEnumerable<User> GetAllByUserName(string name)
        {
            return _appContext.Users.Where(x => x.UserName == name);
        }

        public void Add(User entity)
        {
        }

        public void AddRange(params User[] entities)
        {
        }
    }
}
