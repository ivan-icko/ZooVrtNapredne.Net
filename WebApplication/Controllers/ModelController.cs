using DataAccessLayer.UnitOfWork;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace WebApplication.Controllers
{

    [Authorize]
    public class ModelController : Controller
    {
        public readonly IUnitOfWork uow;
        private readonly IWebHostEnvironment hostingEnvironment;

        [BindProperty]
        public VetViewModel ModelVM { get; set; }


        public ModelController(IUnitOfWork uow, IWebHostEnvironment h)
        {
            this.uow = uow;
            this.hostingEnvironment = h;
        }


        

        public IActionResult Index()
        {
            var model = uow.AnimalRepository.GetAll();
            List<VetViewModel> list = new List<VetViewModel>();
            list = model.Select(m => new VetViewModel()
            {
                Type=m.Type,
                Age=m.Age,
                AnimalId=m.Id,
                VetId=m.VetId,
                VetName=m.Vet.VName,
                ImagePath=m.ImagePath}).ToList();

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

            Animal a = new Animal() { Type = v.Type, Age = v.Age, VetId = v.VetId };

            uow.AnimalRepository.Add(a) ;
            uow.Save();

            var savedAnimal = uow.AnimalRepository.SearchBy(animal => animal == a).FirstOrDefault();
            var animalId = savedAnimal.Id;

            string wwwroothPath = hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;



            if (files.Count != 0)
            {
                var ImagePath = @"images\animals\";
                var Extension = Path.GetExtension(files[0].FileName);
                var RelativeImagePath = ImagePath + animalId + Extension;
                var AbsImagePath = Path.Combine(wwwroothPath, RelativeImagePath);


                using (var fileStream = new FileStream(AbsImagePath, FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                //sacuvaj imgPath u bazi

                savedAnimal.ImagePath = RelativeImagePath;
                uow.Save();

            }


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
