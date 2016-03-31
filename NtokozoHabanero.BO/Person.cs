using System;
using Habanero.BO;

namespace NtokozoHabanero.BO
{
    public class Person : BusinessObject
    {
        public Guid PersonId
        {
            get { return ((Guid) GetPropertyValue("PersonId")); }
            set { base.SetPropertyValue("PersonId", value);}
        }

        public string FirstName
        {
            get { return ((string) GetPropertyValue("FirstName")); }
            set { base.SetPropertyValue("FirstName", value);}
        }
        public string LastName
        {
            get { return ((string) GetPropertyValue("FirstName")); }
            set { base.SetPropertyValue("FirstName", value);}
        }


        public DateTime DateOfBirth
        {
            get { return ((DateTime) GetPropertyValue("DateOfBirth")); }
            set { base.SetPropertyValue("DateOfBirth", value);}
        }

        public string HomeTown
        {
            get { return ((string) GetPropertyValue("HomeTown")); }
            set { base.SetPropertyValue("HomeTown", value);}
        }

        public string Education
        {
            get { return ((string) GetPropertyValue("Education")); }
            set { base.SetPropertyValue("Education", value);}
        }
    }
}
