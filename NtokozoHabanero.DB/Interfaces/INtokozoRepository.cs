using System;
using System.Collections.Generic;
using NtokozoHabanero.BO;

namespace NtokozoHabanero.DB.Interfaces
{
    public interface INtokozoRepository
    {
        void Save(Ntokozo ntokozo);
        List<Ntokozo> GetNtokozos();
        Ntokozo GetNtokozoBy(Guid id);
        void Update(Ntokozo ntokozo, Ntokozo newNtokozo);
        void Delete(Ntokozo ntokozo);
    }
}