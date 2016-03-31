using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using NtokozoHabanero.BO;
using NtokozoHabanero.DB.Interfaces;
using PersonViewModel = NtokozoHabanero.Web.Models.PersonViewModel;

namespace NtokozoHabanero.Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMappingEngine _mappingEngine;

        public PersonController(IPersonRepository personRepository, IMappingEngine mappingEngine)
        {
            if (personRepository == null)
            {
                throw new ArgumentNullException(nameof(personRepository));
            }
            if (mappingEngine == null)
            {
                throw new ArgumentNullException(nameof(mappingEngine));
            }
            _personRepository = personRepository;
            _mappingEngine = mappingEngine;
        }

        // GET: Person
        public ActionResult Index()
        {
            var people = _personRepository.GetAllPeople();
            var viewModel = _mappingEngine.Map<IEnumerable<PersonViewModel>>(people);
            return View(viewModel);
        }

        // GET: Person/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Person/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Person/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Person/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Person/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Person/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
