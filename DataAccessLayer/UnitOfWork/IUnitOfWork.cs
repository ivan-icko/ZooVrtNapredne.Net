using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IAnimalRepository AnimalRepository { get; set; }
        public IVetRepository VetRepository { get; set; }
        public IPackageRepository PackageRepository { get; set; }
        public IUserRepository UserRepository { get; set; }


        public void Save();
    }
}
