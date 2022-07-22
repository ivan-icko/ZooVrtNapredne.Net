using DataAccessLayer.UnitOfWork;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ModelController : Controller
    {
        public readonly IUnitOfWork uow;

        [BindProperty]
        public VetViewModel ModelVM { get; set; }


        public ModelController(IUnitOfWork uow)
        {
            this.uow = uow;
        }


        

        public IActionResult Index()
        {
            var model = uow.AnimalRepository.GetAll();
            List<VetViewModel> list = new List<VetViewModel>();
            list = model.Select(m => new VetViewModel(){Type=m.Type,Age=m.Age,AnimalId=m.Id,VetId=m.VetId,VetName=m.Vet.VName}).ToList();
            return View(list);
        }


        public IActionResult Create()
        {
            var vets = uow.VetRepository.GetAll();
            VetViewModel vm = new VetViewModel();

            vm.Vets = vets.Select(p => new SelectListItem(p.VName, p.VetId.ToString())).ToList();
            return View(vm);
        }

        [HttpPost,ActionName("Create")]
        public IActionResult CreatePost(VetViewModel v) 
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            uow.AnimalRepository.Add(new Animal() { Type = v.Type,Age = v.Age, VetId = v.VetId}) ;
            uow.Save();

            return RedirectToAction("Index");
        }



        public IActionResult Edit(int id)
        {
            Animal a = uow.AnimalRepository.SearchById(new Animal() { Id = id });
            var vets = uow.VetRepository.GetAll();

            ModelVM= new VetViewModel()
            {
                AnimalId=id,
                Type=a.Type,
                Vets = vets.Select(p => new SelectListItem(p.VName,p.VetId.ToString())).ToList(),
                Age = a.Age,
                VetId = a.VetId
            };

           
            if(ModelVM is null)
            {
                return NotFound();
            }
            return View(ModelVM);
        }

        [HttpPost,ActionName("Edit")]
        public IActionResult EditPost()
        { 
            Animal a = uow.AnimalRepository.SearchById(ModelVM.AnimalId);

            a.Type = ModelVM.Type;
            a.Age = ModelVM.Age;
            a.VetId = ModelVM.VetId;

            uow.AnimalRepository.Update(a);
            uow.Save();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Animal a = uow.AnimalRepository.SearchById(id);

            if(a is null)
            {
                return NotFound();
            }

            uow.AnimalRepository.Delete(a);
            uow.Save();

            return RedirectToAction("Index");
        }

    }
}
