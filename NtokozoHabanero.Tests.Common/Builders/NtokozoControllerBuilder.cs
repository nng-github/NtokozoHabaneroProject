using AutoMapper;
using NSubstitute;
using NtokozoHabanero.DB.Interfaces;

namespace NtokozoHabanero.Tests.Common.Builders
{
    public class NtokozoControllerBuilder
    {
        private INtokozoRepository _ntokozoRepository;
        private IMappingEngine _mappingEngine;

        public NtokozoControllerBuilder()
        {
            _ntokozoRepository = Substitute.For<INtokozoRepository>();
            _mappingEngine = Substitute.For<IMappingEngine>();
        }

        public NtokozoControllerBuilder WithNtokozoRepository(INtokozoRepository ntokozoRepository)
        {
            _ntokozoRepository = ntokozoRepository;
            return this;
        }

        public NtokozoControllerBuilder WithMappingEngine(IMappingEngine mappingEngine)
        {
            _mappingEngine = mappingEngine;
            return this;
        }

        public NtokozoControllerBuilder Build()
        {
            return new NtokozoControllerBuilder();
        }
    }
}
