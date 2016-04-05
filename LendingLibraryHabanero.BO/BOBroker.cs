using System.Collections.Generic;
using Habanero.Base;
using Habanero.BO.ClassDefinition;
using Habanero.Smooth;

namespace LendingLibrary.Habanero.BO
{
    public class BOBroker
    {
        public static IEnumerable<IClassDef> GetClassDefs()
        {
            var loadClassDefs = new ClassDefCol();
            AllClassesAutoMapper.ClassDefCol = loadClassDefs;
            var personClassAutoMapper = new AllClassesAutoMapper(new AssemblyTypeSource(typeof(Person).Assembly));
            var lendingClassAutoMapper = new AllClassesAutoMapper(new AssemblyTypeSource(typeof(Lending).Assembly));
            var itemClassAutoMapper = new AllClassesAutoMapper(new AssemblyTypeSource(typeof(Item).Assembly));
            personClassAutoMapper.Map();
            lendingClassAutoMapper.Map();
            itemClassAutoMapper.Map();
            return loadClassDefs;
        }
        public static void LoadClassDefs()
        {
            ClassDef.ClassDefs.Clear();
            var loadedClassDefs = GetClassDefs();
            ClassDef.ClassDefs.Add(loadedClassDefs);
        }
    }
}