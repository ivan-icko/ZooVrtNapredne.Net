﻿using cloudscribe.Pagination.Models;
using DataAccessLayer.UnitOfWork;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class PackageController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IWebHostEnvironment hostingEnvironment;

        [BindProperty]
        public PackageViewModel ModelVM { get; set; }

        public PackageController(IUnitOfWork iow,IWebHostEnvironment web)
        {
            this.uow = iow;
            this.hostingEnvironment = web;
        }



        public IActionResult Index(string searchString, string sortOrder, int pageNumber = 1, int pageSize = 3)
        {

            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.CurrentFilter = searchString;
            ViewBag.AgeSortParam = String.IsNullOrEmpty(sortOrder) ? "age_desc" : "";

            int ExcludeRecords = (pageNumber * pageSize) - pageSize;

            var model = from a in uow.PackageRepository.GetAll() select a;
            var packageCount = model.Count();

            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(a => a.Name.ToUpper().Contains(searchString.ToUpper()));
                packageCount = model.Count();
            }

            switch (sortOrder)
            {
                case "age_desc":
                    model = model.OrderByDescending(b => b.Price);
                    break;
                default:
                    model = model.OrderBy(b => b.Price);
                    break;
            }

            model = model.Skip(ExcludeRecords).Take(pageSize);

            List<PackageViewModel> list = new List<PackageViewModel>();

            list = model.Select(m => new PackageViewModel()
            {
                Name = m.Name,
                Duration = m.DurationInHours,
                PackageId = m.PackageId,
                Price = m.Price,
                Animals = m.Animals.Select(a => new SelectListItem(a.Type, a.Id.ToString())).ToList()

            }).ToList();





            var result = new PagedResult<PackageViewModel>
            {
                Data = list,
                TotalItems = packageCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };


            return View(result);
        }





        public IActionResult Edit(int id)
        {
            Package p = uow.PackageRepository.SearchById(new Package() { PackageId = id });
            var animals = uow.AnimalRepository.GetAll();
            var otherAnimals = animals.Except(p.Animals);

            ModelVM = new PackageViewModel()
            {
                PackageId = id,
                Duration = p.DurationInHours,
                Name = p.Name,
                Price = p.Price,
                Animals = p.Animals.Select(p => new SelectListItem(p.Type, p.Id.ToString())).ToList(),
                OtherAnimals = otherAnimals.Select(p => new SelectListItem(p.Type, p.Id.ToString())).ToList()
            };


            if (ModelVM is null)
            {
                return NotFound();
            }
            return View(ModelVM);
        }
        [HttpPost, ActionName("Edit")]
        public IActionResult EditPost(PackageViewModel v)
        {
            Package p = uow.PackageRepository.SearchById(ModelVM.PackageId);

            if (!ModelState.IsValid)
            {
                return View();
            }

            p.Name = v.Name;
            p.Price = v.Price;
            p.DurationInHours = v.Duration;
            //ovde ide punjenje zivotinja za paket
            List<Animal> animals = new List<Animal>();
           foreach(int num in v.NewAnimalsInPackage)
            {
                animals.Add(uow.AnimalRepository.SearchById(num));
            }
            p.Animals = null;
            p.Animals = animals;


            UploadImageIfAvalible(p, v.PackageId);

            uow.Save();

            return RedirectToAction("Index");
        }


        private void UploadImageIfAvalible(Package a, int id)
        {
            var savedAnimal = uow.PackageRepository.SearchById(id);
            var animalId = id;

            string wwwroothPath = hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            if (files.Count != 0)
            {
                var ImagePath = @"images\packages\";
                var Extension = Path.GetExtension(files[0].FileName);
                var RelativeImagePath = ImagePath + animalId + Extension;
                var AbsImagePath = Path.Combine(wwwroothPath, RelativeImagePath);

                using (var fileStream = new FileStream(AbsImagePath, FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                //sacuvaj imgPath u bazi

                savedAnimal.ImagePath = RelativeImagePath;
                uow.PackageRepository.Update(savedAnimal);
            }
        }
    }
}
