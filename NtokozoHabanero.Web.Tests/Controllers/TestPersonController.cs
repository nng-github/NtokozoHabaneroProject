using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Habanero.BO;
using Habanero.Util;
using NotkozoHabanero.DB.Tests;
using NSubstitute;
using NtokozoHabanero.BO;
using NtokozoHabanero.DB.Interfaces;
using NtokozoHabanero.Tests.Common.Builders;
using NtokozoHabanero.Web.Bootstrap.Mappings;
using NtokozoHabanero.Web.Controllers;
using NtokozoHabanero.Web.Models;
using NUnit.Framework;
using BORegistry = NtokozoHabanero.BO.BORegistry;

namespace NtokozoHabanero.Web.Tests.Controllers
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
            var ex = Assert.Throws<ArgumentNullException>(() => new PersonController(null, Substitute.For<IMappingEngine>()));
            //---------------Test Result -----------------------
            Assert.AreEqual("personRepository", ex.ParamName);
        }

        [Test]
        public void Constructor_GivenIMappingEngineIsNull_ShouldThrow()
        {
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
                .WithRepository(repository)
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
                .WithRepository(repository)
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
                .WithRepository(repository)
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
            return new PersonBuilder().WithRandomId().Build();
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
    }
}