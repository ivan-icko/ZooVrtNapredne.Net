using DataAccessLayer.UnitOfWork;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Controllers
{

    [Authorize]
    public class MakeController : Controller
    {
        private readonly IUnitOfWork uow;

        public MakeController(IUnitOfWork iow)
        {
            this.uow = iow;
        }

        public IActionResult Index()
        {
            var model = uow.VetRepository.GetAll();
            List<AnimalVewModel> list = new List<AnimalVewModel>();
            list = model.Select(m => new AnimalVewModel() { VName = m.VName,Id = m.VetId }).ToList();
            return View(list);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AnimalVewModel vet)
        {
            if (!ModelState.IsValid)
            {
                return Create();
            }

            uow.VetRepository.Add(new Vet()
            {
                VName = vet.VName
            });

            uow.Save();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var vet = uow.VetRepository.SearchById(id);
            if (vet is null)
            {
                return NotFound();
            }

            uow.VetRepository.Delete(vet);
            uow.Save();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var vet = uow.VetRepository.SearchById(id);
            if (vet is null)
            {
                return NotFound();
            }
            AnimalVewModel model = new AnimalVewModel() { VName = vet.VName, Id = vet.VetId};
            return View(model);
        }


        [HttpPost]
        public IActionResult Edit(AnimalVewModel vet)
        {
            if (!ModelState.IsValid)
            {
                return View(vet);
            }
            Vet v = uow.VetRepository.SearchById(vet.Id);
            v.VName = vet.VName;

            uow.VetRepository.Update(v);
            uow.Save();
            return RedirectToAction("Index");

        }
    }

}
