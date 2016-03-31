using System;
using Habanero.BO;

namespace NtokozoHabanero.Web.Models
{
    public interface IPersonViewModel
    {
        Guid PersonId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string HomeTown { get; set; }
        DateTime DateOfBirth { get; set; }
        string Education { get; set; }
    }

    public class PersonViewModel : IPersonViewModel
    {
        public Guid PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HomeTown { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Education { get; set; }
    }
}