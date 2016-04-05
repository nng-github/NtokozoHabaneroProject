using System;
using Habanero.BO;
using Habanero.Testability;
using LendingLibrary.Habanero.BO;

namespace LendingLibrary.Habanero.Tests.Common.Builders
{
    public class LendingBuilder
    {
        private readonly Lending _lending;

        public LendingBuilder()
        {
            _lending = BuildValid();
            _lending.RequestDate = RandomValueGen.GetRandomDate();
            _lending.ReturnDate = RandomValueGen.GetRandomDate();
            _lending.LoanDate = RandomValueGen.GetRandomDate();
        }

        private BOTestFactory<T> CreateFactory<T>() where T : BusinessObject
        {
            BOBroker.LoadClassDefs();
            return new BOTestFactory<T>();
        }

        private Lending BuildValid()
        {
            var lendings = CreateFactory<Lending>()
                .SetValueFor(v => v.RequestDate)
                .SetValueFor(v => v.LoanDate)
                .SetValueFor(v => v.ReturnDate)
                .CreateValidBusinessObject();
            return lendings;
        }

        public Lending BuildSaved()
        {
            _lending.Save();
            return _lending;
        }

        public Lending Build()
        {
            return _lending;
        }
    }
}
