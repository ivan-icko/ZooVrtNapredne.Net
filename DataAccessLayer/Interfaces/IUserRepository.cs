using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccessLayer.Interfaces.IRepository;

namespace DataAccessLayer.Interfaces
{
    public interface IUserRepository:IRepository<User>
    {
    }
}
