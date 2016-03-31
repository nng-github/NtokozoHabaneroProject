using AutoMapper;
using NSubstitute;
using NtokozoHabanero.DB.Interfaces;
using NtokozoHabanero.Web.Controllers;

namespace NtokozoHabanero.Tests.Common.Builders
{
    public class PersonControllerBuilder
    {
        private IPersonRepository _personRepository;
        private IMappingEngine _mappingEngine;

        public PersonControllerBuilder()
        {
            _personRepository = Substitute.For<IPersonRepository>();
            _mappingEngine = Substitute.For<IMappingEngine>();
        }

        public PersonControllerBuilder WithRepository(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
            return this;
        }

        public PersonControllerBuilder WithMappingEngine(IMappingEngine mappingEngine)
        {
            _mappingEngine = mappingEngine;
            return this;
        }

        public PersonController Build()
        {
            return new PersonController(_personRepository, _mappingEngine);
        }
    }
}
