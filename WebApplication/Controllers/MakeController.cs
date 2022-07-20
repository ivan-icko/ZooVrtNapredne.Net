using DataAccessLayer.UnitOfWork;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class MakeController : Controller
    {
        private readonly IUnitOfWork uow;

        public MakeController(IUnitOfWork iow)
        {
            this.uow = iow;
        }

        public IActionResult Index()
        {
            var model = uow.AnimalRepository.GetAll();
            List<AnimalVewModel> list = new List<AnimalVewModel>();
            list = model.Select(m => new AnimalVewModel() { Type = m.Type, Age = m.Age, Id = m.Id }).ToList();
            return View(list);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AnimalVewModel Animal)
        {
            if (!ModelState.IsValid)
            {
                return Create();
            }

            uow.AnimalRepository.Add(new Animal()
            {
                Type = Animal.Type,
                Age = Animal.Age
            });

            uow.Save();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var animal = uow.AnimalRepository.SearchById(id);
            if (animal is null)
            {
                return NotFound();
            }

            uow.AnimalRepository.Delete(animal);
            uow.Save();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var animal = uow.AnimalRepository.SearchById(id);
            if (animal is null)
            {
                return NotFound();
            }
            AnimalVewModel model = new AnimalVewModel() { Id = animal.Id, Age = animal.Age, Type = animal.Type };
            return View(model);
        }


        [HttpPost]
        public IActionResult Edit(AnimalVewModel animal)
        {
            if (!ModelState.IsValid)
            {
                return View(animal);
            }

            uow.AnimalRepository.Update(new Animal() {Id=animal.Id,Type=animal.Type, Age=animal.Age});
            uow.Save();
            return RedirectToAction("Index");

        }
    }

}
