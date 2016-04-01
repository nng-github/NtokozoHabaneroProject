using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using LendingLibrary.Habanero.BO;
using LendingLibrary.Habanero.DB.Interfaces;
using PersonViewModel = LendingLibrary.Habanero.Web.Models.PersonViewModel;

namespace LendingLibrary.Habanero.Web.Controllers
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

        // GET: Person/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Person/Create
        [HttpPost]
        public ActionResult Create(PersonViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var person = _mappingEngine.Map<PersonViewModel, Person>(viewModel);
                _personRepository.Save(person);
                return RedirectToAction("Index");
            }
            return View("Create");
        }

        // GET: Person/Edit/5
        public ActionResult Edit(Guid id)
        {
            var person = _personRepository.GetPersonBy(id);
            var personViewModel = _mappingEngine.Map<Person, PersonViewModel>(person);
            return View(personViewModel);
        }

        // POST: Person/Edit/5
        [HttpPost]
        public ActionResult Edit(PersonViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var person = _mappingEngine.Map<PersonViewModel, Person>(viewModel);
                _personRepository.Update(person, viewModel.PersonId);
                return RedirectToAction("Index");
            }
            return View("Edit");
        }

        // GET: Person/Delete/5
        public ActionResult Delete(Guid id)
        {
            var person = _personRepository.GetPersonBy(id);
            if (person != null)
            {
                _personRepository.Delete(person);
                return RedirectToAction("Index");
            }
            return View("Delete");
        }
        
    }
}
