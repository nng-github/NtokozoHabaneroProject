using System;
using System.Collections.Generic;
using Habanero.Base;
using Habanero.BO;
using LendingLibrary.Habanero.BO;
using LendingLibrary.Habanero.DB.Interfaces;

namespace LendingLibrary.Habanero.DB.Repository
{
    public class ItemRepository : IItemRepository
    {
        public void Save(Item item)
        {
            item.Save();
        }

        public void Delete(Item item)
        {
            item.MarkForDelete();
            item.Save();
        }

        public Item GetItemBy(Guid id)
        {
            var item = Broker.GetBusinessObject<Item>(new Criteria("ItemId", Criteria.ComparisonOp.Equals, id));
            return item;
        }

        public List<Item> GetAllItems()
        {
            return new List<Item>(Broker.GetBusinessObjectCollection<Item>("", "ItemId"));
        }
    }
}
