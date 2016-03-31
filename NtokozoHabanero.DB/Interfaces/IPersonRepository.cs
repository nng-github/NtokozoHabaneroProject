using System;
using System.Collections.Generic;
using NtokozoHabanero.BO;

namespace NtokozoHabanero.DB.Interfaces
{
    public interface IPersonRepository
    {
        void Save(Person person);
        List<Person> GetAllPeople();
        Person GetPersonBy(Guid id);
        void Update(Person person);
        void Delete(Person person);
    }
}