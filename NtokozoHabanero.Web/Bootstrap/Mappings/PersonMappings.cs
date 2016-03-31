using System.Collections.Generic;
using AutoMapper;
using NtokozoHabanero.BO;
using PersonViewModel = NtokozoHabanero.Web.Models.PersonViewModel;

namespace NtokozoHabanero.Web.Bootstrap.Mappings
{
    public class PersonMappings : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<PersonViewModel, Person>();

            Mapper.CreateMap<Person, PersonViewModel>();
        }
    }
}