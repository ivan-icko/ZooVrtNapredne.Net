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
    public class AnimalRepository : IAnimalRepository
    {
        private readonly AnimalContext context;

        public AnimalRepository(AnimalContext context)
        {
            this.context = context;
        }

        public void Add(Animal entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Animal entity)
        {
            throw new NotImplementedException();
        }

        public List<Animal> GetAll()
        {
            List<Animal> animals = context.Animals.ToList();
            return context.Animals.ToList();
        }

        public List<Animal> SearchBy(Expression<Func<Animal, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Animal SearchById(Animal entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Animal entity)
        {
            throw new NotImplementedException();
        }
    }
}
