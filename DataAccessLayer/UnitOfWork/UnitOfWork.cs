using DataAccessLayer.Implementations;
using DataAccessLayer.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AnimalContext context;

        public UnitOfWork(AnimalContext context)
        {
            this.context = context;
            AnimalRepository = new AnimalRepository(context);
            VetRepository = new VetRepository(context);
            PackageRepository = new PackageRepository(context);

            
        }
        public IAnimalRepository AnimalRepository { get; set; } 
        public IVetRepository VetRepository { get; set; }
        public IpackageRepository PackageRepository { get; set; }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
