using System;
using System.Collections.Generic;
using Habanero.Base;
using Habanero.BO;
using LendingLibrary.Habanero.BO;
using LendingLibrary.Habanero.DB.Interfaces;

namespace LendingLibrary.Habanero.DB.Repository
{
    public class PersonRepository : IPersonRepository
    {
        public void Save(Person person)
        {
            person.Save();
        }

        public List<Person> GetAllPeople()
        {
            return new List<Person>(Broker.GetBusinessObjectCollection<Person>("", "PersonId"));
        }

        public Person GetPersonBy(Guid id)
        {
            var person = Broker.GetBusinessObject<Person>(new Criteria("PersonId", Criteria.ComparisonOp.Equals, id));
            return person;
        }

        public void Update(Person person, Guid personId)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }
            var existingPerson = GetPersonBy(personId);
            SetPersonNewDetails(person, existingPerson);
            existingPerson.Save();
        }

        private static void SetPersonNewDetails(Person person, Person existingPerson)
        {
            if (existingPerson != null)
            {
                existingPerson.FirstName = person.FirstName;
                existingPerson.LastName = person.LastName;
                existingPerson.HomeTown = person.HomeTown;
                existingPerson.DateOfBirth = person.DateOfBirth;
                existingPerson.Education = person.Education;
            }
        }

        public void Delete(Person person)
        {
            person.MarkForDelete();
            person.Save();
        }
    }
}
