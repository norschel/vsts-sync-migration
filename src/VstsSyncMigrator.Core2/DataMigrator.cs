using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VstsSyncMigrator;
using VstsSyncMigrator.DataContracts;

namespace VstsSyncMigrator.Core2
{
    public class DataMigrator<TType>
    {
        public void SourcetoStore(IRepository<TType> source, IRepository<TType> store)
        {

        }

        public void StoretoTarget(IRepository<TType> store, IRepository<TType> target)
        {

        }

    }
}
