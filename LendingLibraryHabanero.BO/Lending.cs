using System;
using Habanero.Base;
using Habanero.BO;
using Habanero.Smooth;

namespace LendingLibrary.Habanero.BO
{
    public class Lending : BusinessObject
    {
        [AutoMapPrimaryKey]
        public Guid LendingId   
        {
            get { return ((Guid)GetPropertyValue("LendingId")); }
            set { base.SetPropertyValue("LendingId", value); }
        }

        public DateTime? RequestDate
        {
            get { return ((DateTime)GetPropertyValue("RequestDate")); }
            set { base.SetPropertyValue("RequestDate", value); }
        }
        public DateTime? LoanDate
        {
            get { return ((DateTime)GetPropertyValue("LoanDate")); }
            set { base.SetPropertyValue("LoanDate", value); }
        }
        public DateTime? ReturnDate
        {
            get { return ((DateTime)GetPropertyValue("ReturnDate")); }
            set { base.SetPropertyValue("ReturnDate", value); }
        }

        [AutoMapCompulsory]
        [AutoMapManyToOne]
        public Person Person
        {
            get { return Relationships.GetRelatedObject<Person>("Person"); }
            set { Relationships.SetRelatedObject("Person", value); }
        }

        [AutoMapCompulsory]
        [AutoMapManyToOne]
        public Item Item
        {
            get { return Relationships.GetRelatedObject<Item>("Item"); }
            set { Relationships.SetRelatedObject("Item", value); }
        }
    }
}
