namespace FutureCodr.UI.Controllers
{
    using FutureCodr.Data.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using FutureCodr.Models;

    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IBootcampRepository _bootcampRepo;

        public AdminController(IBootcampRepository bootcampRepo)
        {
            _bootcampRepo = bootcampRepo;
        }

        public ActionResult BootcampLocations()
        {
            return View();
        }

        public ActionResult Bootcamps()
        {
            return View();
        }

        public ActionResult BootcampTechnology()
        {
            return View();
        }

        public ActionResult ContactForms()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Links()
        {
            List<Bootcamp> allBootcamps = _bootcampRepo.GetAllBootcamps();
            return View(allBootcamps);
        }

        public ActionResult Locations()
        {
            return View();
        }

        public ActionResult Sessions()
        {
            return View();
        }

        public ActionResult Sites()
        {
            return View();
        }

        public ActionResult Technology()
        {
            return View();
        }

        public ActionResult Users()
        {
            return View();
        }
    }
}
