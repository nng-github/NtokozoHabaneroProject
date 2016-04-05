using System;
using System.Collections.Generic;
using Habanero.Base;
using Habanero.BO;
using LendingLibrary.Habanero.BO;
using LendingLibrary.Habanero.DB.Interfaces;

namespace LendingLibrary.Habanero.DB.Repository
{
    public class LendingsRepository : ILendingsRepository
    {
        public void Save(Lending lending)
        {
            lending.Save();
        }

        public void Delete(Lending lending)
        {
            lending.MarkForDelete();
            lending.Save();
        }

        public Lending GetLendingsBy(Guid lendingId)
        {
           return Broker.GetBusinessObject<Lending>(new Criteria("LendingId", Criteria.ComparisonOp.Equals, lendingId));
        }

        public List<Lending> GetAllLendings()
        {
            return new List<Lending>(Broker.GetBusinessObjectCollection<Lending>("", "LendingId"));
        }
    }
}
