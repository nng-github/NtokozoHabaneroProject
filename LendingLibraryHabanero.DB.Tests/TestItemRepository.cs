using System.Linq;
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
    public class TestItemRepository
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
            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new ItemRepository());
        }

        [Test]
        public void GetAllItems_GivenThereIsOneItem_ShouldReturnThatOneItem()
        {
            //---------------Set up test pack-------------------
            var item = CreateSavedItem();
            var repository = CreateItemRepository();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var actual = repository.GetAllItems();
            //---------------Test Result -----------------------
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(item.ItemName, actual.First().ItemName);
            Assert.AreEqual(item.Description, actual.First().Description);
        }

        [Test]
        public void GetAllItems_GivenThereIsTwoItems_ShouldReturnThoseTwoItem()
        {
            //---------------Set up test pack-------------------
            CreateSavedItem();
            CreateSavedItem();
            var repository = CreateItemRepository();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var actual = repository.GetAllItems();
            //---------------Test Result -----------------------
            Assert.AreEqual(2, actual.Count);
        }

        [Test]
        public void GetAllItems_GivenThereIsThreeItems_ShouldReturnThoseItems()
        {
            //---------------Set up test pack-------------------
            CreateSavedItem();
            CreateSavedItem();
            CreateSavedItem();
            var repository = CreateItemRepository();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var actual = repository.GetAllItems();
            //---------------Test Result -----------------------
            Assert.AreEqual(3, actual.Count);
        }

        [Test]
        public void GetItemBy_GivenItemId_ShouldReturnItem()
        {
            //---------------Set up test pack-------------------
            var item = CreateSavedItem();
            var repository = CreateItemRepository();        
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var actual = repository.GetItemBy(item.ItemId);
            //---------------Test Result -----------------------
            Assert.IsNotNull(actual);
            Assert.AreSame(item, actual);

        }

        [Test]
        public void Save_GivenNewItem_ShouldSaveThatItem()
        {
            //---------------Set up test pack-------------------
            var item = CreateItemBuilder()
                .WithRandomId()
                .WithItemName("New Item")
                .WithDescription("Description")
                .Build();
            var repositiory = CreateItemRepository();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            repositiory.Save(item);
            //---------------Test Result -----------------------
            var actual =
                Broker.GetBusinessObject<Item>(new Criteria("ItemId", Criteria.ComparisonOp.Equals, item.ItemId));
            Assert.AreEqual(item.ItemId, actual.ItemId);
            Assert.AreEqual(item.ItemName, actual.ItemName);
            Assert.AreEqual(item.Description, actual.Description);
        }

        [Test]
        public void Save_GivenUpdatedExistingItem_ShouldSaveChanges()
        {
            //---------------Set up test pack-------------------
            var item = CreateItemBuilder()
                .WithRandomId()
                .WithItemName("Old Name")
                .BuildSaved();
            var repository = CreateItemRepository();
            item.ItemName = "New Name";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            repository.Save(item);
            //---------------Test Result -----------------------
            var savedItem =
                Broker.GetBusinessObject<Item>(new Criteria("ItemId", Criteria.ComparisonOp.Equals, item.ItemId));
            Assert.IsNotNull(savedItem);
            Assert.AreEqual(savedItem.ItemName, "New Name");
        }

        [Test]
        public void Delete_GivenExistingItem_ShouldDeleteThatItemAndSaveChanges()
        {
            //---------------Set up test pack-------------------
            var item = CreateSavedItem();
            var repository = CreateItemRepository();
            //---------------Assert Precondition----------------
            var people = Broker.GetBusinessObjectCollection<Item>("");
            Assert.AreEqual(1, people.Count);
            //---------------Execute Test ----------------------
            repository.Delete(item);
            //---------------Test Result -----------------------
            var actual =
                Broker.GetBusinessObject<Item>(new Criteria("ItemId", Criteria.ComparisonOp.Equals, item.ItemId));
            Assert.IsNull(actual);
        }


        private ItemBuilder CreateItemBuilder()
        {
            return new ItemBuilder();
        }

        private static IItemRepository CreateItemRepository()
        {
          return new ItemRepository();
        }

        private static Item CreateSavedItem()
        {
            return new ItemBuilder().WithRandomId().BuildSaved();
        }
    }
}
