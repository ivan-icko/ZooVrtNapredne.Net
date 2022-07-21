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
        public ModelViewModel ModelVM { get; set; }


        public ModelController(IUnitOfWork uow)
        {
            this.uow = uow;
        }


        

        public IActionResult Index()
        {
            var model = uow.AnimalRepository.GetAll();
            List<ModelViewModel> list = new List<ModelViewModel>();
            list = model.Select(m => new ModelViewModel(){Type=m.Type,Age=m.Age,Id=m.Id,VetId=m.VetId,VetName=m.Vet.VName}).ToList();
            return View(list);
        }


        public IActionResult Create()
        {
            VetViewModel vm = new VetViewModel(){ };
            var vets = uow.AnimalRepository.GetAll();

            vm.Animals = vets.Select(p => new SelectListItem(p.Type, p.Id.ToString())).ToList();
            return View(vm);
        }

        [HttpPost,ActionName("Create")]
        public IActionResult CreatePost(VetViewModel v) 
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            uow.VetRepository.Add(new Vet() { VName = v.VName,Animals=null }) ;
            uow.Save();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Animal a = uow.AnimalRepository.SearchById(new Animal() { Id = id });
            var animals = uow.AnimalRepository.GetAll();

            VetViewModel model = new VetViewModel()
            {
                Animal = a,
                Animals = animals.Select(p => new SelectListItem(p.Type,p.Id.ToString())).ToList(),
                VName = a.Vet.VName
            };

            if(model is null)
            {
                return NotFound();
            }
            return View(model);
        }

    }
}
