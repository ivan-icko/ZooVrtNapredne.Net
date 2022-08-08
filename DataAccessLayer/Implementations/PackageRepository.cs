using DataAccessLayer.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Implementations
{
    public class PackageRepository : IpackageRepository
    {
        private readonly AnimalContext context;

        public PackageRepository(AnimalContext context)
        {
            this.context = context;
        }

        public void Add(Package entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Package entity)
        {
            throw new NotImplementedException();
        }

        public List<Package> GetAll()
        {
            return context.Packages.Include(p => p.Animals).ToList();
        }

        public List<Package> SearchBy(Expression<Func<Package, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Package SearchById(Package entity)
        {
            return context.Packages.Include(p => p.Animals).First();
        }

        public Package SearchById(int id)
        {
            return context.Packages.Include(p => p.Animals).First(p=>p.PackageId==id);
        }

        public void Update(Package entity)
        {
            context.Update(entity);
        }
    }
}
