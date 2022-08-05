using cloudscribe.Pagination.Models;
using DataAccessLayer.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
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

        public PackageController(IUnitOfWork iow)
        {
            this.uow = iow;
        }
      


        public IActionResult Index(string searchString, string sortOrder, int pageNumber = 1, int pageSize = 3)
        {

            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.CurrentFilter = searchString;
            ViewBag.AgeSortParam = String.IsNullOrEmpty(sortOrder) ? "age_desc" : "";

            int ExcludeRecords = (pageNumber * pageSize) - pageSize;

            var model = from a in uow.PackageRepository.GetAll() select a;
            var animalCount = model.Count();

            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(a => a.Name.ToUpper().Contains(searchString.ToUpper()));
                animalCount = model.Count();
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
                Name=m.Name,
                Duration=m.DurationInHours,
                PackageId=m.PackageId
            }).ToList();



            var result = new PagedResult<PackageViewModel>
            {
                Data = list,
                TotalItems = animalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };


            return View(result);
        }
    }
}
