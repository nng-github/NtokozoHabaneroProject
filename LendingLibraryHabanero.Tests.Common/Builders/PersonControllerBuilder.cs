using AutoMapper;
using LendingLibrary.Habanero.DB.Interfaces;
using NSubstitute;
using LendingLibrary.Habanero.Web.Controllers;

namespace LendingLibrary.Habanero.Tests.Common.Builders
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

        public PersonControllerBuilder WithPersonRepository(IPersonRepository personRepository)
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
