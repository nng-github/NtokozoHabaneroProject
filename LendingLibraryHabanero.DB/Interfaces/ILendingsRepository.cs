using System;
using System.Collections.Generic;
using LendingLibrary.Habanero.BO;

namespace LendingLibrary.Habanero.DB.Interfaces
{
    public interface ILendingsRepository
    {
        void Save(Lending lending);
        void Delete(Lending lending);
        Lending GetLendingsBy(Guid lendingId);
        List<Lending> GetAllLendings();
    }
}
