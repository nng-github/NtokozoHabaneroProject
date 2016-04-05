using System;
using System.Runtime.InteropServices;
using Habanero.BO;
using Habanero.Testability;
using LendingLibrary.Habanero.BO;

namespace LendingLibrary.Habanero.Tests.Common.Builders
{
    public class ItemBuilder
    {
        private readonly Item _item;

        public ItemBuilder()
        {
            _item = BuildValid();
            _item.ItemName = RandomValueGen.GetRandomString();
            _item.Description = RandomValueGen.GetRandomString();
        }

        public ItemBuilder WithRandomId()
        {
            _item.ItemId = Guid.NewGuid();
            return this;
        }

        public ItemBuilder WithId(Guid id)
        {
            _item.ItemId = id;
            return this;
        }

        public ItemBuilder WithItemName(string name)
        {
            _item.ItemName = name;
            return this;
        }

        public ItemBuilder WithDescription(string description)
        {
            _item.Description = description;
            return this;
        }

        private Item BuildValid()
        {
            var item = CreateFactory<Item>()
                .SetValueFor(v => v.ItemName)
                .SetValueFor(v => v.Description)
                .CreateValidBusinessObject();
            return item;
        }

        public Item BuildSaved()
        {
            _item.Save();
            return _item;
        }

        public Item Build()
        {
            return _item;
        }

        private BOTestFactory<T> CreateFactory<T>() where T : BusinessObject
        {
            BOBroker.LoadClassDefs();
            return new BOTestFactory<T>();
        }
    }
}
