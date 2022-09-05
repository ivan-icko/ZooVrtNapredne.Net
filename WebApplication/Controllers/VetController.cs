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
    public class VetController : Controller
    {
        private readonly IUnitOfWork uow;

        public VetController(IUnitOfWork iow)
        {
            this.uow = iow;
        }

        public IActionResult Index()
        {
            var model = uow.VetRepository.GetAll();
            List<VetViewModel> list = new List<VetViewModel>();
            list = model.Select(m => new VetViewModel() 
            { VName = m.VName,
                Id = m.VetId,
                PhoneNumber=m.PhoneNumber,
                JMBG = m.JMBG,
                DateOfBirth = m.DateOfBIrth
            }).ToList();
            return View(list);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(VetViewModel vet)
        {
            if (!ModelState.IsValid)
            {
                return Create();
            }

            uow.VetRepository.Add(new Vet()
            {
                VName = vet.VName,
                JMBG = vet.JMBG,
                DateOfBIrth = vet.DateOfBirth,
                PhoneNumber = vet.PhoneNumber,
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
            VetViewModel model = new VetViewModel() { VName = vet.VName, Id = vet.VetId,
                PhoneNumber = vet.PhoneNumber,
                JMBG = vet.JMBG,
                DateOfBirth = vet.DateOfBIrth
            };
            return View(model);
        }


        [HttpPost]
        public IActionResult Edit(VetViewModel vet)
        {
            if (!ModelState.IsValid)
            {
                return View(vet);
            }
            Vet v = uow.VetRepository.SearchById(vet.Id);

            v.VName = vet.VName;
            v.JMBG = vet.JMBG;
            v.PhoneNumber = vet.PhoneNumber;
            v.DateOfBIrth = vet.DateOfBirth;

            uow.VetRepository.Update(v);
            uow.Save();
            return RedirectToAction("Index");

        }
    }

}
