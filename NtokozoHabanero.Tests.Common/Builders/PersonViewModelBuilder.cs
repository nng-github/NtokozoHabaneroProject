using System;
using Habanero.Base;
using Habanero.BO;
using Habanero.Testability;
using NtokozoHabanero.BO;
using NtokozoHabanero.Web.Models;

namespace NtokozoHabanero.Tests.Common.Builders
{
    public class PersonViewModelBuilder
    {
        private readonly PersonViewModel _person;

        public PersonViewModelBuilder()
        {
            //_person = BuildValid();
            _person.FirstName = RandomValueGen.GetRandomString();
            _person.LastName = RandomValueGen.GetRandomString();
            _person.DateOfBirth = new DateTime(2015, 03, 05);
            _person.Education = RandomValueGen.GetRandomString();
            _person.HomeTown = RandomValueGen.GetRandomString();
        }

        public PersonViewModelBuilder WithFirstName(string name)
        {
            _person.FirstName = name;
            return this;
        }

        public PersonViewModelBuilder WithLastName(string surname)
        {
            _person.LastName = surname;
            return this;
        }

        public PersonViewModelBuilder WithRandomId()
        {
            _person.PersonId = Guid.NewGuid();
            return this;
        }

        public PersonViewModelBuilder WithId(Guid id)
        {
            _person.PersonId = id;
            return this;
        }

        public PersonViewModelBuilder WithDateOfBirth(DateTime dob)
        {
            _person.DateOfBirth = dob;
            return this;
        }

        public PersonViewModelBuilder WithEducation(string education)
        {
            _person.Education = education;
            return this;
        }

        public PersonViewModelBuilder WithHomeTown(string town)
        {
            _person.HomeTown = town;
            return this;
        }

        private BOTestFactory<T> CreateFactory<T>() where T : BusinessObject, IPersonViewModel
        {
            BOBroker.LoadClassDefs();
            return new BOTestFactory<T>();
        }

       /* private PersonViewModel BuildValid()
        {
            var personViewModel = CreateFactory<PersonViewModel>()
                .SetValueFor(v => v.FirstName)
                .SetValueFor(v => v.LastName)
                .SetValueFor(v => v.DateOfBirth)
                .SetValueFor(v => v.Education)
                .SetValueFor(v => v.HomeTown)
                .CreateValidBusinessObject();
            return personViewModel;
        }*/

        /*public PersonViewModel BuildSaved()
        {
            _person.Save();
            return _person;
        }*/

        public PersonViewModel Build()
        {
            return _person;
        }
    }
}
