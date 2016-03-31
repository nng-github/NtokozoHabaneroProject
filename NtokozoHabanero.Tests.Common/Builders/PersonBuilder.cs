using System;
using Habanero.BO;
using Habanero.Testability;
using NtokozoHabanero.BO;

namespace NtokozoHabanero.Tests.Common.Builders
{
    public class PersonBuilder
    {
        private readonly Person _person;

        public PersonBuilder()
        {
            _person = BuildValid();
            _person.FirstName = RandomValueGen.GetRandomString();
            _person.LastName = RandomValueGen.GetRandomString();
            _person.DateOfBirth = RandomValueGen.GetRandomDate();
            _person.HomeTown = RandomValueGen.GetRandomString();
            _person.Education = RandomValueGen.GetRandomString();
        }

        public PersonBuilder WithRandomId()
        {
            _person.PersonId = Guid.NewGuid();
            return this;
        }

        public PersonBuilder WithId(Guid id)
        {
            _person.PersonId = id;
            return this;
        }

        public PersonBuilder WithFirstName(string name)
        {
            _person.FirstName = name;
            return this;
        }

        public PersonBuilder WithLastName(string surname)
        {
            _person.LastName = surname;
            return this;
        }

        public PersonBuilder WithEducation(string education)
        {
            _person.Education = education;
            return this;
        }

        public PersonBuilder WithDateOfBirth(DateTime dob)
        {
            _person.DateOfBirth = dob;
            return this;
        }

        public PersonBuilder WithHomeTown(string town)
        {
            _person.HomeTown = town;
            return this;
        }

        private BOTestFactory<T> CreateFactory<T>() where T : BusinessObject
        {
            BOBroker.LoadClassDefs();
            return new BOTestFactory<T>();
        }

        private Person BuildValid()
        {
            var person = CreateFactory<Person>()
                .SetValueFor(v => v.FirstName)
                .SetValueFor(v => v.LastName)
                .SetValueFor(v => v.DateOfBirth)
                .SetValueFor(v => v.HomeTown)
                .SetValueFor(v => v.Education)
                .CreateValidBusinessObject();
            return person;
        }

        public Person BuildSaved()
        {
            _person.Save();
            return _person;
        }

        public Person Build()
        {
            return _person;
        }
    }
}
