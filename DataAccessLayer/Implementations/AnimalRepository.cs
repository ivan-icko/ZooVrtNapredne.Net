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
            context.Add(entity);
        }

        public void Delete(Animal entity)
        {
            context.Remove(entity);
        }

       

        public List<Animal> GetAll()
        {
            List<Animal> animals = context.Animals.ToList();
            return context.Animals.Include(v => v.Vet).ToList();
        }

        public List<Animal> SearchBy(Expression<Func<Animal, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Animal SearchById(Animal entity)
        {
            return context.Animals.Include(v=>v.Vet).First(a => a.Id == entity.Id);
        }

        public Animal SearchById(int id)
        {
            return context.Animals.First(a => a.Id == id);
        }

        public void Update(Animal entity)
        {
            context.Update(entity);
        }
    }
}
