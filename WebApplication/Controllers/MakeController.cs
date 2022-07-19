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
            model.Select(m=> new AnimalVewModel() {Type=m.Type});
            return View(list);
        }
    }
}
