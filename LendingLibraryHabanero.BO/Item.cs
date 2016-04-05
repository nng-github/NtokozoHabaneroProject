using System;
using Habanero.Base;
using Habanero.BO;
using Habanero.Smooth;

namespace LendingLibrary.Habanero.BO
{
    public class Item : BusinessObject
    {
        [AutoMapPrimaryKey]
        public Guid ItemId
        {
            get { return ((Guid)GetPropertyValue("ItemId")); }
            set { base.SetPropertyValue("ItemId", value); }
        }

        public string ItemName
        {
            get { return ((string)GetPropertyValue("ItemName")); }
            set { base.SetPropertyValue("ItemName", value);}
        }

        public string Description
        {
            get { return ((string) GetPropertyValue("Description")); }
            set { base.SetPropertyValue("Description", value);}
        }


        [AutoMapOneToMany(RelationshipType.Association, ReverseRelationshipName = "Item")]
        public virtual Lending Lending
        {
            get { return Relationships.GetRelatedObject<Lending>("Lending"); }
        }
    }
}
