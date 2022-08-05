﻿using DataAccessLayer.Interfaces;
using Domain;
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
            return context.Packages.ToList();
        }

        public List<Package> SearchBy(Expression<Func<Package, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Package SearchById(Package entity)
        {
            throw new NotImplementedException();
        }

        public Package SearchById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Package entity)
        {
            throw new NotImplementedException();
        }
    }
}
