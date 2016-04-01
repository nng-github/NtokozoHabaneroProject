using System;
using System.ComponentModel.DataAnnotations;

namespace LendingLibrary.Habanero.Web.Models
{
    public interface IPersonViewModel
    {
        Guid PersonId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string HomeTown { get; set; }
        DateTime? DateOfBirth { get; set; }
        string Education { get; set; }
    }

    public class PersonViewModel : IPersonViewModel
    {
        public Guid PersonId { get; set; }
        [Required]
        [Display(Name="First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Hometown")]
        public string HomeTown { get; set; }
        [Required]
        [Display(Name = "DOB")]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        [Display(Name = "Education")]
        public string Education { get; set; }
    }
}