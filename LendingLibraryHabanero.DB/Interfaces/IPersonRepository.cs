using System;
using System.Collections.Generic;
using LendingLibrary.Habanero.BO;

namespace LendingLibrary.Habanero.DB.Interfaces
{
    public interface IPersonRepository
    {
        void Save(Person person);
        List<Person> GetAllPeople();
        Person GetPersonBy(Guid id);
        void Update(Person person, Guid personId);
        void Delete(Person person);
    }
}