using System;
using System.Runtime.InteropServices;
using Habanero.BO;
using Habanero.Testability;
using NtokozoHabanero.BO;

namespace NtokozoHabanero.Tests.Common.Builders
{
    public class NtokozoBuilder
    {
        private readonly Ntokozo _ntokozo;

        public NtokozoBuilder()
        {
            _ntokozo = BuildValid();
            _ntokozo.DateOfBirth = RandomValueGen.GetRandomDate();
            _ntokozo.HomeTown = RandomValueGen.GetRandomString();
            _ntokozo.Gender = RandomValueGen.GetRandomEnum<Gender>();
            _ntokozo.Education = RandomValueGen.GetRandomString();
        }

        public NtokozoBuilder WithNewId()
        {
            _ntokozo.NtokozoId = Guid.NewGuid();
            return this;
        }

        public NtokozoBuilder WithEducation(string education)
        {
            _ntokozo.Education = education;
            return this;
        }

        public NtokozoBuilder WithDateOfBirth(DateTime dob)
        {
            _ntokozo.DateOfBirth = dob;
            return this;
        }

        public NtokozoBuilder WithHomeTown(string town)
        {
            _ntokozo.HomeTown = town;
            return this;
        }

        public NtokozoBuilder WithGender(Gender gender)
        {
            _ntokozo.Gender = gender;
            return this;
        }

        private BOTestFactory<T> CreateFactory<T>() where T : BusinessObject
        {
            return new BOTestFactory<T>();
        }

        private Ntokozo BuildValid()
        {
            var ntokozo = CreateFactory<Ntokozo>()
                .SetValueFor(v => v.DateOfBirth)
                .SetValueFor(v => v.HomeTown)
                .SetValueFor(v => v.Gender)
                .SetValueFor(v => v.Education)
                .CreateValidBusinessObject();
            return ntokozo;
        }
    }
}
