using AutoMapper;
using LendingLibrary.Habanero.BO;
using PersonViewModel = LendingLibrary.Habanero.Web.Models.PersonViewModel;

namespace LendingLibrary.Habanero.Web.Bootstrap.Mappings
{
    public class PersonMappings : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<PersonViewModel, Person>()
                .ForMember(b => b.PersonId, x => x.Ignore());
            Mapper.CreateMap<Person, PersonViewModel>();

        }
    }
}