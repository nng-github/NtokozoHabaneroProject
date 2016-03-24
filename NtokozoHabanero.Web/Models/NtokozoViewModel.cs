using System;
using Habanero.BO;
using NtokozoHabanero.BO;

namespace NtokozoHabanero.Web.Models
{
    public abstract class NtokozoViewModel : BusinessObject
    {
        public Guid NtokozoId { get; set; }
        public string HomeTown { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Education { get; set; }
    }
}