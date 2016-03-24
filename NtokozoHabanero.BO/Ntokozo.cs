using System;
using Habanero.BO;

namespace NtokozoHabanero.BO
{
    public enum Gender
    {
        Male,
        Female,
        Both,
        Other
    }

    public abstract class Ntokozo : BusinessObject
    {
        public Guid NtokozoId
        {
            get { return ((Guid) GetPropertyValue("NtokozoId")); }
            set { base.SetPropertyValue("NtokozoId", value);}
        }

        public Gender Gender
        {
            get { return ((Gender) GetPropertyValue("Color")); }
            set { base.SetPropertyValue("Gender", value);}
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
