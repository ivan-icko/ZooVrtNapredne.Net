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
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace WebApplication.Controllers
{
    public class PackageController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;



        [BindProperty]
        public PackageViewModel PackageVm { get; set; }

        public PackageController(IUnitOfWork iow, IWebHostEnvironment web, IHttpContextAccessor accessor)
        {
            this.uow = iow;
            this.hostingEnvironment = web;
            this.httpContextAccessor = accessor;
        }

        public IActionResult Index2(string searchString, string sortOrder, string sortOrderDate, int pageNumber = 1, int pageSize = 3)
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
                ImagePath = m.ImagePath,
                Price = m.Price,
                FreePlaces = m.FreePlaces,
                DateTime = m.DateTime,
                Animals = m.Animals.Select(a => new SelectListItem(a.Type, a.Id.ToString())).ToList()

            }).ToList();





            var result = new PagedResult<PackageViewModel>
            {
                Data = list,
                TotalItems = packageCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };


            return View("Index", result);
        }

        public IActionResult Index(bool isChecked, string searchString, string checkBox, string sortOrder, string sortOrderDate, int pageNumber = 1, int pageSize = 3)
        {

            ViewBag.CurrentSortOrder = sortOrderDate;
            ViewBag.CurrentFilter = searchString;
            ViewBag.DateSortParam = String.IsNullOrEmpty(sortOrderDate) ? "date_asc" : "";
            ViewBag.CheckBox = string.IsNullOrEmpty(checkBox) ? "checked" : " ";

            int ExcludeRecords = (pageNumber * pageSize) - pageSize;

            var model = from a in uow.PackageRepository.GetAll() select a;
            var packageCount = model.Count();




            if (isChecked)
            {
                if (!string.IsNullOrEmpty(searchString))
                {
                    model = model.Where(a => a.Name.ToUpper().Contains(searchString.ToUpper()));
                    model = model.Where(a => a.FreePlaces > 0);
                    packageCount = model.Count();
                }
                else
                {
                    model = model.Where(a => a.FreePlaces > 0);
                    packageCount = model.Count();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(searchString))
                {
                    model = model.Where(a => a.Name.ToUpper().Contains(searchString.ToUpper()));
                    packageCount = model.Count();
                }
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(a => a.Name.ToUpper().Contains(searchString.ToUpper()));
                packageCount = model.Count();
            }

            switch (sortOrderDate)
            {
                case "date_asc":
                    model = model.OrderBy(b => b.DateTime);
                    break;
                default:
                    model = model.OrderByDescending(b => b.DateTime);
                    break;
            }

            model = model.Skip(ExcludeRecords).Take(pageSize);

            List<PackageViewModel> list = new List<PackageViewModel>();

            list = model.Select(m => new PackageViewModel()
            {
                Name = m.Name,
                Duration = m.DurationInHours,
                PackageId = m.PackageId,
                ImagePath = m.ImagePath,
                Price = m.Price,
                FreePlaces = m.FreePlaces,
                DateTime = m.DateTime,
                Animals = m.Animals.Select(a => new SelectListItem(a.Type, a.Id.ToString())).ToList()

            }).ToList();





            var result = new PagedResult<PackageViewModel>
            {
                Data = list,
                TotalItems = packageCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };


            return View("Index", result);
        }

        /* public IActionResult Index(string searchString, string sortOrder,string so, int pageNumber = 1, int pageSize = 3)
         {

             ViewBag.CurrentSortOrder = sortOrder;
             ViewBag.CurrentFilter = searchString;


             ViewBag.AgeSortParam = string.IsNullOrEmpty(sortOrder) ? "age_desc" : "";

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
                 ImagePath = m.ImagePath,
                 Price = m.Price,
                 FreePlaces = m.FreePlaces,
                 DateTime = m.DateTime,
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
         }*/

        /*  public IActionResult Index2(string searchString, string sortOrderDate, int pageNumber = 1, int pageSize = 3)
          {

              //ViewBag.CurrentSortOrder = sortOrder;
              ViewBag.CurrentFilter = searchString;



              ViewBag.DateSortParam = string.IsNullOrEmpty(sortOrderDate) ? "date_desc" : "";

              int ExcludeRecords = (pageNumber * pageSize) - pageSize;

              var model = from a in uow.PackageRepository.GetAll() select a;
              var packageCount = model.Count();



              if (!string.IsNullOrEmpty(searchString))
              {
                  model = model.Where(a => a.Name.ToUpper().Contains(searchString.ToUpper()));
                  packageCount = model.Count();
              }


              switch (sortOrderDate)
              {
                  case "date_desc":
                      model = model.OrderByDescending(b => b.DateTime);
                      break;
                  default:
                      model = model.OrderBy(b => b.DateTime);
                      break;
              }




              model = model.Skip(ExcludeRecords).Take(pageSize);

              List<PackageViewModel> list = new List<PackageViewModel>();

              list = model.Select(m => new PackageViewModel()
              {
                  Name = m.Name,
                  Duration = m.DurationInHours,
                  PackageId = m.PackageId,
                  ImagePath = m.ImagePath,
                  Price = m.Price,
                  FreePlaces = m.FreePlaces,
                  DateTime = m.DateTime,
                  Animals = m.Animals.Select(a => new SelectListItem(a.Type, a.Id.ToString())).ToList()

              }).ToList();





              var result = new PagedResult<PackageViewModel>
              {
                  Data = list,
                  TotalItems = packageCount,
                  PageNumber = pageNumber,
                  PageSize = pageSize
              };


              return RedirectToAction("Index",result);

          }
  */


        public IActionResult Create()
        {
            PackageViewModel vm = new PackageViewModel();
            var allAnimals = uow.AnimalRepository.GetAll();

            vm.Animals = new List<SelectListItem>();
            vm.OtherAnimals = allAnimals.Select(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.Type }).ToList();


            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(PackageViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                return Create();
            }

            Package p = new Package()
            {
                DurationInHours = vm.Duration,
                FreePlaces = vm.FreePlaces,
                Name = vm.Name,
                Price = vm.Price,
                DateTime = vm.DateTime
            };

            foreach (int num in vm.NewAnimalsInPackage)
            {
                p.Animals.Add(uow.AnimalRepository.SearchById(num));
            }

            uow.PackageRepository.Add(p);
            uow.Save();

            UploadImageIfAvalible(p, p.PackageId);
            uow.Save();

            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            Package p = uow.PackageRepository.SearchById(id);
            var animals = uow.AnimalRepository.GetAll();
            var otherAnimals = animals.Except(p.Animals);

            PackageVm = new PackageViewModel()
            {
                PackageId = id,
                Duration = p.DurationInHours,
                DateTime = p.DateTime,
                Name = p.Name,
                FreePlaces = p.FreePlaces,
                Price = p.Price,
                Animals = p.Animals.Select(p => new SelectListItem(p.Type, p.Id.ToString())).ToList(),
                OtherAnimals = otherAnimals.Select(p => new SelectListItem(p.Type, p.Id.ToString())).ToList()
            };


            if (PackageVm is null)
            {
                return NotFound();
            }
            return View(PackageVm);
        }
        [HttpPost, ActionName("Edit")]
        public IActionResult EditPost(PackageViewModel v)
        {
            Package p = uow.PackageRepository.SearchById(PackageVm.PackageId);

            if (!ModelState.IsValid)
            {
                return Edit(p.PackageId);
            }

            p.Name = v.Name;

            p.Price = v.Price;
            p.DurationInHours = v.Duration;
            p.FreePlaces = v.FreePlaces;
            p.DateTime = v.DateTime;
            //ovde ide punjenje zivotinja za paket
            List<Animal> animals = new List<Animal>();

            foreach (int num in v.NewAnimalsInPackage)
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
            var savedPackage = uow.PackageRepository.SearchById(id);
            var packageId = id;

            string wwwroothPath = hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            if (files.Count != 0)
            {
                var ImagePath = @"images\packages\";
                var Extension = Path.GetExtension(files[0].FileName);
                var RelativeImagePath = ImagePath + packageId + Extension;
                var AbsImagePath = Path.Combine(wwwroothPath, RelativeImagePath);

                using (var fileStream = new FileStream(AbsImagePath, FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                //sacuvaj imgPath u bazi

                savedPackage.ImagePath = RelativeImagePath;
                uow.PackageRepository.Update(savedPackage);
            }
        }


        public IActionResult Apply(int id)
        {

            Package p = uow.PackageRepository.SearchById(id);
            var userId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var user = uow.UserRepository.SearchById(userId);

            ApplyViewModel vm = new ApplyViewModel();
            vm.Name = p.Name;
            vm.PackageId = p.PackageId;
            vm.Price = p.Price;
            vm.FreePlaces = p.FreePlaces;
            vm.UserName = user.FirstName;
            vm.UserLastName = user.LastName;
            vm.Animals = p.Animals.Select(a => new SelectListItem() { Text = a.Type, Value = a.Id.ToString() }).ToList();

            return View(vm);
        }

        [HttpPost]
        public IActionResult Apply(ApplyViewModel vm)
        {
            Package p = uow.PackageRepository.SearchById(vm.PackageId);
            p.FreePlaces -= vm.NumerOfPersons;

            User u = uow.UserRepository.SearchById(int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value));

            u.PUs.Add(new PU() { Package = p, User = u, TimeOfReservation = DateTime.Now });



            uow.Save();


            return RedirectToAction("Index");
        }




        public IActionResult Weather()
        {
            return View();
        }

        public JsonResult GetWeather()
        {
            WeatherViewModel vm = new WeatherViewModel();
            var a = Json(vm.GetWeatherforecast());
            return a;
        }

    }
}
