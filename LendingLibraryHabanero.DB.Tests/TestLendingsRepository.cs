using Habanero.Base;
using Habanero.BO;
using LendingLibrary.Habanero.BO;
using LendingLibrary.Habanero.DB.Interfaces;
using LendingLibrary.Habanero.DB.Repository;
using LendingLibrary.Habanero.Tests.Common.Builders;
using NUnit.Framework;
using BORegistry = Habanero.BO.BORegistry;

namespace LendingLibrary.Habanero.DB.Tests
{
    [TestFixture]
    public class TestLendingsRepository
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
            //---------------Set up test pack-------------------

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new LendingsRepository());
            //---------------Test Result -----------------------
        }

        [Test]
        public void GetAllLendings_GivenOneLending_ShouldReturnThatLending()
        {
            //---------------Set up test pack-------------------
            CreateSavedLending();
            var repository = CreateLendingsRepository();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var results = repository.GetAllLendings();
            //---------------Test Result -----------------------
            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count);
        }

        [Test]
        public void GetAllLendings_GivenTwoLendings_ShouldReturnTwoLendings()
        {
            //---------------Set up test pack-------------------
            CreateSavedLending();
            CreateSavedLending();
            var repository = CreateLendingsRepository();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var results = repository.GetAllLendings();
            //---------------Test Result -----------------------
            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count);
        }

        [Test]
        public void GetAllLendings_GivenManyLendings_ShouldAllLendings()
        {
            //---------------Set up test pack-------------------
            CreateSavedLending();
            CreateSavedLending();
            CreateSavedLending();
            var repository = CreateLendingsRepository();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var results = repository.GetAllLendings();
            //---------------Test Result -----------------------
            Assert.IsNotNull(results);
            Assert.AreEqual(3, results.Count);
        }



        [Test]
        public void GetLendingBy_GivenLendingId_ShouldReturnLending()
        {
            //---------------Set up test pack-------------------
            var lendings = CreateSavedLending();
            var repository = CreateLendingsRepository();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var results = repository.GetLendingsBy(lendings.LendingId);
            //---------------Test Result -----------------------
            Assert.IsNotNull(results);
            Assert.AreSame(lendings, results);
        }

        [Test]
        public void Save_GivenValidLending_ShouldSaveToDb()
        {
            //---------------Set up test pack-------------------
            var lending = CreateSavedLending();
            var repository = CreateLendingsRepository();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            repository.Save(lending);
            //---------------Test Result -----------------------
            var actual = Broker.GetBusinessObject<Lending>(new Criteria("LendingId", Criteria.ComparisonOp.Equals, lending.LendingId));
            Assert.AreSame(lending, actual);
        }

        [Test]
        public void Delete_GivenValidLendings_ShouldDeleteAndSave()
        {
            //---------------Set up test pack-------------------
            var lending = CreateSavedLending();
            var lendings = Broker.GetBusinessObjectCollection<Lending>("");
            var repository = CreateLendingsRepository();
            //---------------Assert Precondition----------------
            Assert.AreEqual(1, lendings.Count);
            //---------------Execute Test ----------------------
            repository.Delete(lending);
            //---------------Test Result -----------------------
            Assert.AreEqual(0, lendings.Count);
        }

        private static ILendingsRepository CreateLendingsRepository()
        {
            return new LendingsRepository();
        }

        private Lending CreateSavedLending()
        {
            return new LendingBuilder().BuildSaved();
        }

    }
}
