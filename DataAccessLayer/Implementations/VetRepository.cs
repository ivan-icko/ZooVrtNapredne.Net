﻿using DataAccessLayer.Interfaces;
using DataAccessLayer.UnitOfWork;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Implementations
{
    public class VetRepository : IVetRepository
    {
        private AnimalContext context;

        public VetRepository(AnimalContext context)
        {
            this.context = context;
        }

        public void Add(Vet entity)
        {
            context.Vets.Add(entity);
        }

        public void Delete(Vet entity)
        {
            context.Vets.Remove(entity);
        }

        public List<Vet> GetAll()
        {
            return context.Vets.ToList();
        }

        public List<Vet> SearchBy(Expression<Func<Vet, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Vet SearchById(Vet entity)
        {
            return context.Vets.Find(entity);
        }

        public Vet SearchById(int id)
        {
            return context.Vets.First(v => v.VetId == id);
        }

        public void Update(Vet entity)
        {
            context.Vets.Update(entity);
        }
    }
}
