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
using cloudscribe.Pagination.Models;
using Microsoft.EntityFrameworkCore;



namespace WebApplication.Controllers
{

    
    public class ModelController : Controller
    {
        public readonly IUnitOfWork uow;
        private readonly IWebHostEnvironment hostingEnvironment;

        [BindProperty]
        public AnimalViewModel ModelVM { get; set; }


        public ModelController(IUnitOfWork uow, IWebHostEnvironment h)
        {
            this.uow = uow;
            this.hostingEnvironment = h;
        }




        public IActionResult Index(string searchString,string sortOrder,int pageNumber = 1, int pageSize = 3)
        {

            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.CurrentFilter = searchString;
            ViewBag.AgeSortParam = String.IsNullOrEmpty(sortOrder)?"age_desc":"" ;

            int ExcludeRecords = (pageNumber * pageSize) - pageSize;

            var model = from a in uow.AnimalRepository.GetAll() select a;
            var animalCount = model.Count();

            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(a => a.Type.ToUpper().Contains(searchString.ToUpper()));
                animalCount = model.Count();
            }

            switch (sortOrder)
            {
                case "age_desc":
                    model = model.OrderByDescending(b => b.Age);
                    break;
                default:
                    model = model.OrderBy(b => b.Age);
                    break;
            }

            model = model.Skip(ExcludeRecords).Take(pageSize);

            List<AnimalViewModel> list = new List<AnimalViewModel>();
            list = model.Select(m => new AnimalViewModel()
            {
                Type = m.Type,
                Age = m.Age,
                AnimalId = m.Id,
                VetId = m.VetId,
                VetName = m.Vet.VName,
                ImagePath = m.ImagePath 
            }).ToList();

           

            var result = new PagedResult<AnimalViewModel>
            {
                Data = list,
                TotalItems = animalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };


            return View(result);
        }
       


        public IActionResult Create()
        {
            var vets = uow.VetRepository.GetAll();
            AnimalViewModel vm = new AnimalViewModel();

            vm.Vets = vets.Select(p => new SelectListItem(p.VName, p.VetId.ToString())).ToList();
            return View(vm);
        }
        [HttpPost,ActionName("Create")]
        public IActionResult CreatePost(AnimalViewModel v) 
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Animal a = new Animal() { Type = v.Type, Age = v.Age, VetId = v.VetId };

            uow.AnimalRepository.Add(a);
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

            ModelVM= new AnimalViewModel()
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

        [HttpPost, ActionName("Edit")]
        public IActionResult EditPost(AnimalViewModel v)
        {
            Animal a = uow.AnimalRepository.SearchById(ModelVM.AnimalId);
            
            if (!ModelState.IsValid)
            {
                return View();
            }

            a.Age = v.Age;
            a.Type = v.Type;
            a.VetId = v.VetId;

            UploadImageIfAvalible(a,v.AnimalId);

            uow.Save();
           
            return RedirectToAction("Index");
        }


        private void UploadImageIfAvalible(Animal a,int id)
        {
            var savedAnimal = uow.AnimalRepository.SearchById(id);
            var animalId = id;

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
                uow.AnimalRepository.Update(savedAnimal);
            }
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
