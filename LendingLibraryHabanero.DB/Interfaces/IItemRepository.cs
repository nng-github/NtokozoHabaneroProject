using System;
using System.Collections.Generic;
using LendingLibrary.Habanero.BO;

namespace LendingLibrary.Habanero.DB.Interfaces
{
    public interface IItemRepository
    {
        void Save(Item item);
        void Delete(Item item);
        Item GetItemBy(Guid id);
        List<Item> GetAllItems();
    }
}