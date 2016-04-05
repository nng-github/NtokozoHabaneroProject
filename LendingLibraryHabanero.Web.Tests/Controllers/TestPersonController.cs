using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Habanero.BO;
using LendingLibrary.Habanero.BO;
using LendingLibrary.Habanero.DB.Interfaces;
using LendingLibrary.Habanero.DB.Tests;
using LendingLibrary.Habanero.Tests.Common.Builders;
using LendingLibrary.Habanero.Web.Bootstrap.Mappings;
using LendingLibrary.Habanero.Web.Controllers;
using LendingLibrary.Habanero.Web.Models;
using NSubstitute;
using NUnit.Framework;

using BORegistry = LendingLibrary.Habanero.BO.BORegistry;

namespace LendingLibrary.Habanero.Web.Tests.Controllers
{
    [TestFixture]
    public class TestPersonController
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            TestUtils.SetupFixture();
        }

        [SetUp]
        public void Setup()
        {
            BORegistry.DataAccessor = new DataAccessorInMemory();
        }

        [Test]
        public void Construct()
        {
            Assert.DoesNotThrow(() => new PersonController(Substitute.For<IPersonRepository>(), Substitute.For<IMappingEngine>()));
        }

        [Test]
        public void Constructor_GivenIPersonRepositoryIsNull_ShouldThrow()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => new PersonController(null, Substitute.For<IMappingEngine>()));
            //---------------Test Result -----------------------
            Assert.AreEqual("personRepository", ex.ParamName);
        }

        [Test]
        public void Constructor_GivenIMappingEngineIsNull_ShouldThrow()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<ArgumentNullException>(() => new PersonController(Substitute.For<IPersonRepository>(), null));
            //---------------Test Result -----------------------
            Assert.AreEqual("mappingEngine", ex.ParamName);
        }

        [Test]
        public void Index_ShouldRenderView()
        {
            //---------------Set up test pack-------------------
            var controller = CreatePersonController().Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var viewResult = controller.Index() as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(viewResult);
        }

        [Test]
        public void Index_GivenRepositoryReturnedValidPeople_ShouldReturnViewListWithPeople()
        {
            //---------------Set up test pack-------------------
            var repository = Substitute.For<IPersonRepository>();
            var people = new List<Person> { CreatePerson() };
            repository.GetAllPeople().Returns(people);
            var mappingEngine = ResolveMapper();
            var controller = CreatePersonController()
                .WithPersonRepository(repository)
                .WithMappingEngine(mappingEngine)
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var viewResult = controller.Index() as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(viewResult);
            Assert.IsNotNull(viewResult.Model as IEnumerable);
        }

        [Test]
        public void Index_ShouldCallGetAllPeopleFromRepo()
        {
            //---------------Set up test pack-------------------
            var repository = Substitute.For<IPersonRepository>();
            var personController = CreatePersonController()
                .WithPersonRepository(repository)
                .Build();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            personController.Index();
            //---------------Test Result -----------------------
            repository.Received().GetAllPeople();
        }

        [Test]
        public void Index_ShouldCallMappingEngine()
        {
            //---------------Set up test pack-------------------
            var mapEngine = Substitute.For<IMappingEngine>();
            var repository = Substitute.For<IPersonRepository>();
            var personController = CreatePersonController()
                .WithPersonRepository(repository)
                .WithMappingEngine(mapEngine)
                .Build();
            var people = new List<Person> { CreatePerson() };
            repository.GetAllPeople().Returns(people);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            personController.Index();
            //---------------Test Result -----------------------
            mapEngine.Received().Map<IEnumerable<PersonViewModel>>(people);
        }

        [Test]
        public void Create_ShouldReturnView()
        {
            //---------------Set up test pack-------------------
            var personController = CreatePersonController().Build();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var results = personController.Create() as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(results);
            Assert.AreEqual("Create", results.ViewName);
        }

        [Test]
        public void Create_POST_GivenValidViewModel_ShouldCallMappingEngine()
        {
            //---------------Set up test pack-------------------
            var viewModel = CreatePersonViewModel();
            var mappingEngine = Substitute.For<IMappingEngine>();
            var personController = CreatePersonController()
                .WithMappingEngine(mappingEngine)
                .Build();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var results = personController.Create(viewModel);
            //---------------Test Result -----------------------
            mappingEngine.Received().Map<PersonViewModel, Person>(viewModel);
        }

        [Test]
        public void Create_POST_GivenPerson_ShouldCallSaveOnPersonRepo()
        {
            //---------------Set up test pack-------------------
            var person = CreatePerson();
            var personViewModel = CreatePersonViewModel();
            var personRepository = Substitute.For<IPersonRepository>();
            var mappingEngine = Substitute.For<IMappingEngine>();
            mappingEngine.Map<PersonViewModel, Person>(personViewModel).Returns(person);
            var personController = CreatePersonController()
                .WithPersonRepository(personRepository)
                .WithMappingEngine(mappingEngine)
                .Build();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var results = personController.Create(personViewModel);
            //---------------Test Result -----------------------
            personRepository.Received().Save(person);
        }

        [Test]
        public void Create_Post_GivenValidViewModel_ShouldRedirectToActionIndex()
        {
            //---------------Set up test pack-------------------
            var personController = CreatePersonController()
                .Build();
            var viewModel = CreatePersonViewModel();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var results = personController.Create(viewModel) as RedirectToRouteResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(results);
            var routeValue = results.RouteValues["action"];
            Assert.AreEqual("Index", routeValue);
        }

        [Test]
        public void Create_POST_GivenInvalidPersonViewModel_ShouldReturnView()
        {
            //---------------Set up test pack-------------------
            var personController = CreatePersonController().Build();
            personController.ModelState.AddModelError("key", "value");
            //---------------Assert Precondition----------------
            Assert.IsFalse(personController.ModelState.IsValid);
            //---------------Execute Test ----------------------
            var results = personController.Create(new PersonViewModel()) as ViewResult;
            //---------------Test Result -----------------------
            Assert.AreEqual("Create", results.ViewName);
        }

        [Test]
        public void Edit_GivenValidPersonId_ShouldCallGetPersonByFromRepo()
        {
            //---------------Set up test pack-------------------
            var personId = Guid.NewGuid();
            var repository = Substitute.For<IPersonRepository>();
            var personController = CreatePersonController()
                .WithPersonRepository(repository)
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            personController.Edit(personId);
            //---------------Test Result -----------------------
            repository.Received().GetPersonBy(personId);
        }

        [Test]
        public void Edit_ShouldCallMappingEngine()
        {
            //---------------Set up test pack-------------------
            var person = CreatePerson();
            var repository = Substitute.For<IPersonRepository>();
            var mappingEngine = Substitute.For<IMappingEngine>();
            repository.GetPersonBy(person.PersonId).Returns(person);
            var personController = CreatePersonController()
                .WithPersonRepository(repository)
                .WithMappingEngine(mappingEngine)
                .Build();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var results = personController.Edit(person.PersonId);
            //---------------Test Result -----------------------
            mappingEngine.Received().Map<Person, PersonViewModel>(person);
        }

        [Test]
        public void Edit_GivenValidPersonId_ShouldReturnViewWithViewModel()
        {
            //---------------Set up test pack-------------------
            var person = CreatePerson();
            var repository = Substitute.For<IPersonRepository>();
            var mappingEngine = Substitute.For<IMappingEngine>();
            mappingEngine.Map<Person, PersonViewModel>(person).Returns(new PersonViewModel());
            repository.GetPersonBy(person.PersonId).Returns(person);
            var personController = CreatePersonController()
                .WithPersonRepository(repository)
                .WithMappingEngine(mappingEngine)
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = personController.Edit(person.PersonId) as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(results);
            var model = results.Model;
            Assert.IsInstanceOf<PersonViewModel>(model);
        }

        [Test]
        public void Edit_POST_GivenModelStateIsValid_ShouldCallMappingEngine()
        {
            //---------------Set up test pack-------------------
            var personViewModel = CreatePersonViewModel();
            var mappingEngine = Substitute.For<IMappingEngine>();
            var personController = CreatePersonController()
                .WithMappingEngine(mappingEngine)
                .Build();
            //---------------Assert Precondition----------------
            Assert.IsTrue(personController.ModelState.IsValid);
            //---------------Execute Test ----------------------
            var results = personController.Edit(personViewModel);
            //---------------Test Result -----------------------
            mappingEngine.Received().Map<PersonViewModel, Person>(personViewModel);
        }

        [Test]
        public void Edit_POST_GivenValidMapping_ShouldCallUpdate()
        {
            //---------------Set up test pack-------------------
            var personViewModel = CreatePersonViewModel();
            var person = CreatePerson();
            var mappingEngine = Substitute.For<IMappingEngine>();
            mappingEngine.Map<PersonViewModel, Person>(personViewModel).Returns(person);
            var repository = Substitute.For<IPersonRepository>();
            var personController = CreatePersonController()
                .WithPersonRepository(repository)
                .WithMappingEngine(mappingEngine)
                .Build();
            //---------------Assert Precondition----------------
            Assert.IsTrue(personController.ModelState.IsValid);
            //---------------Execute Test ----------------------
            var results = personController.Edit(personViewModel);
            //---------------Test Result -----------------------
            repository.Received().Update(person, Arg.Any<Guid>());
        }

        [Test]
        public void Edit_GivenModelStateIsInvalid_ShouldReturnView()
        {
            //---------------Set up test pack-------------------
            var personViewModel = CreatePersonViewModel();
            var personController = CreatePersonController().Build();
            personController.ModelState.AddModelError("key", "error");
            //---------------Assert Precondition----------------
            Assert.IsFalse(personController.ModelState.IsValid);
            //---------------Execute Test ----------------------
            var results = personController.Edit(personViewModel) as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(results);
            Assert.AreEqual("Edit", results.ViewName);
        }

        [Test] public void Delete_GivenValidId_ShouldCallGetByFromRepo()
        {
            //---------------Set up test pack-------------------
            var personId = Guid.NewGuid();
            var personRepository = Substitute.For<IPersonRepository>();
            var personController = CreatePersonController()
                .WithPersonRepository(personRepository)
                .Build();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var results = personController.Delete(personId);
            //---------------Test Result -----------------------
            personRepository.Received().GetPersonBy(personId);
        }

        [Test]
        public void Delete_GivenValidPerson_ShouldCallDeleteFromRepo()
        {
            //---------------Set up test pack-------------------
            var person = CreatePerson();
            var repository = Substitute.For<IPersonRepository>();
            repository.GetPersonBy(person.PersonId).Returns(person);
            var personController = CreatePersonController()
                .WithPersonRepository(repository)
                .Build();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = personController.Delete(person.PersonId) as ViewResult;
            //---------------Test Result -----------------------
           repository.Received().Delete(person);
        }

        [Test]
        public void Delete_GivenSuccessfullDelete_ShouldRedirectToActionIndex()
        {
            //---------------Set up test pack-------------------
            var person = CreatePerson();
            var repository = Substitute.For<IPersonRepository>();
            repository.GetPersonBy(person.PersonId).Returns(person);
            var personController = CreatePersonController()
                .WithPersonRepository(repository)
                .Build();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var results = personController.Delete(person.PersonId) as RedirectToRouteResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(results);
            var routeValue = results.RouteValues["action"];
            Assert.AreEqual("Index", routeValue);
        }

        private static PersonControllerBuilder CreatePersonController()
        {
            return new PersonControllerBuilder();
        }

        private static IPersonRepository CreatePersonRepository()
        {
            return TestUtils.Container.Resolve<IPersonRepository>();
        }

        private static Person CreatePerson()
        {
            return new PersonBuilder().WithRandomId().BuildSaved();
        }

        private static IMappingEngine ResolveMapper()
        {
            return ResolveMappingWith(new PersonMappings());
        }

        private static IMappingEngine ResolveMappingWith(params Profile[] profiles)
        {
            Mapper.Initialize(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });

            return Mapper.Engine;
        }

        private static PersonViewModel CreatePersonViewModel()
        {
            return new PersonViewModelBuilder().WithRandomProps().Build();
        }
    }
}