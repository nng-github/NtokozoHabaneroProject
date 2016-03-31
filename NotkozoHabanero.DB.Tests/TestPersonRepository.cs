using System;
using System.Linq;
using Habanero.Base;
using Habanero.BO;
using NtokozoHabanero.BO;
using NtokozoHabanero.DB.Interfaces;
using NtokozoHabanero.DB.Repository;
using NtokozoHabanero.Tests.Common.Builders;
using NUnit.Framework;
using BORegistry = Habanero.BO.BORegistry;

namespace NotkozoHabanero.DB.Tests
{
    [TestFixture]
    public class TestPersonRepository
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
            //---------------Test Result -----------------------
            Assert.DoesNotThrow(() => new PersonRepository());
        }

        [Test]
        public void GetAllPeople_GivenThereIsOnePerson_ShouldReturnOnePerson()
        {
            //---------------Set up test pack-------------------
            var person = CreateSavedPerson();
            var repository = CreatePersonRepository();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var actual = repository.GetAllPeople();
            //---------------Test Result -----------------------
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(person.FirstName, actual.First().FirstName);
            Assert.AreEqual(person.LastName, actual.First().LastName);
            Assert.AreEqual(person.DateOfBirth, actual.First().DateOfBirth);
            Assert.AreEqual(person.Education, actual.First().Education);
            Assert.AreEqual(person.HomeTown, actual.First().HomeTown);
        }

        [Test]
        public void GetAllPeople_GivenThereIsTwoPerson_ShouldReturnOnePerson()
        {
            //---------------Set up test pack-------------------
            CreateSavedPerson();
            CreateSavedPerson();
            var repository = CreatePersonRepository();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var actual = repository.GetAllPeople();
            //---------------Test Result -----------------------
            Assert.IsNotNull(actual);
            Assert.AreEqual(2, actual.Count);
        }

        [Test]
        public void GetAllPeople_GivenThereIsThreePeople_ShouldReturnThreePerson()
        {
            //---------------Set up test pack-------------------
            CreateSavedPerson();
            CreateSavedPerson();
            CreateSavedPerson();
            var repository = CreatePersonRepository();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var actual = repository.GetAllPeople();
            //---------------Test Result -----------------------
            Assert.AreEqual(3, actual.Count);
        }

        [Test]
        public void GetPersonBy_GivenPersonId_ShouldPerson()
        {
            //---------------Set up test pack-------------------
            var person = CreateSavedPerson();
            var personRepository = CreatePersonRepository();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var actual = personRepository.GetPersonBy(person.PersonId);
            //---------------Test Result -----------------------
            Assert.IsNotNull(actual);
            Assert.AreSame(person, actual);
        }

        [Test]
        public void Save_GivenNewPerson_ShouldSavePerson()
        {
            //---------------Set up test pack-------------------
            var person = CreatePersonBuilder()
                .WithRandomId()
                .WithLastName("Gabela")
                .WithFirstName("Ntokozo")
                .WithHomeTown("Durban")
                .WithEducation("BSc Computer Sciences")
                .Build();
            var personRepository = CreatePersonRepository();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            personRepository.Save(person);
            var actual = Broker.GetBusinessObject<Person>(new Criteria("PersonId", Criteria.ComparisonOp.Equals, person.PersonId));
            //---------------Test Result -----------------------
            Assert.AreSame(person, actual);
        }

        [Test]
        public void Update_GivenEditedPerson_ShouldUpdatePersonDetails()
        {
            //---------------Set up test pack-------------------
            var dateTime = new DateTime(2015, 2, 25);
            var person = CreatePersonBuilder()
                .WithRandomId()
                .WithDateOfBirth(dateTime)
                .WithHomeTown("Durban")
                .BuildSaved();
            person.HomeTown = "Port Shepstone";
            var repository = CreatePersonRepository();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            repository.Update(person);
            //---------------Test Result -----------------------
            var personFromRepo = repository.GetPersonBy(person.PersonId);
            Assert.IsNotNull(personFromRepo);
            var homeTown = personFromRepo.HomeTown;
            Assert.AreEqual("Port Shepstone", homeTown);
        }

        [Test]
        public void Delete_GivenValidPerson_ShouldDeleteThePerson()
        {
            //---------------Set up test pack-------------------
            var savedPerson = CreateSavedPerson();
            var repo = CreatePersonRepository();
            //---------------Assert Precondition----------------
            var people = Broker.GetBusinessObjectCollection<Person>("");
            Assert.AreEqual(1, people.Count);
            //---------------Execute Test ----------------------
            repo.Delete(savedPerson);
            //---------------Test Result -----------------------
            var peopleAfterDelete = Broker.GetBusinessObjectCollection<Person>("");
            Assert.AreEqual(0, peopleAfterDelete.Count);
        }

        private static Person CreateSavedPerson()
        {
            return new PersonBuilder().WithRandomId().BuildSaved();
        }

        private static PersonBuilder CreatePersonBuilder()
        {
            return new PersonBuilder();
        }

        private static IPersonRepository CreatePersonRepository()
        {
            return TestUtils.Container.Resolve<IPersonRepository>();
        }

    }
}
