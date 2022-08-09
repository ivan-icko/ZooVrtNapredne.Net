using DataAccessLayer.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly AnimalContext context;

        public UserRepository(AnimalContext context)
        {
            this.context = context;
        }

        public void Add(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            return context.Users.ToList();
        }

        public List<User> SearchBy(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public User SearchById(User entity)
        {
            throw new NotImplementedException();
        }

        public User SearchById(int id)
        {
            return context.Users.First(a => a.Id == id);
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
