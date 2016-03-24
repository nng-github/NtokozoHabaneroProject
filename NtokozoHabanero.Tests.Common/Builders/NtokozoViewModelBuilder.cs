using System;
using Habanero.BO;
using Habanero.Testability;
using NtokozoHabanero.BO;
using NtokozoHabanero.Web.Models;

namespace NtokozoHabanero.Tests.Common.Builders
{
    public class NtokozoViewModelBuilder
    {
        private readonly NtokozoViewModel _ntokozo;

        public NtokozoViewModelBuilder()
        {
            _ntokozo = BuildValid();
            _ntokozo.DateOfBirth = RandomValueGen.GetRandomDate();
            _ntokozo.Education = RandomValueGen.GetRandomString();
            _ntokozo.Gender = RandomValueGen.GetRandomEnum<Gender>();
            _ntokozo.HomeTown = RandomValueGen.GetRandomString();
        }

        public NtokozoViewModelBuilder WithDateOfBirth(DateTime dob)
        {
            _ntokozo.DateOfBirth = dob;
            return this;
        }

        public NtokozoViewModelBuilder WithEducation(string education)
        {
            _ntokozo.Education = education;
            return this;
        }

        public NtokozoViewModelBuilder WithGender(Gender gender)
        {
            _ntokozo.Gender = gender;
            return this;
        }

        public NtokozoViewModelBuilder WithHomeTown(string town)
        {
            _ntokozo.HomeTown = town;
            return this;
        }

        private BOTestFactory<T> CreateFactory<T>() where T : BusinessObject
        {
            return new BOTestFactory<T>();
        }

        private NtokozoViewModel BuildValid()
        {
            var ntokozo = CreateFactory<NtokozoViewModel>()
                .SetValueFor(v => v.DateOfBirth)
                .SetValueFor(v => v.Education)
                .SetValueFor(v => v.Gender)
                .SetValueFor(v => v.HomeTown)
                .CreateValidBusinessObject();
            return ntokozo;
        }
    }
}
