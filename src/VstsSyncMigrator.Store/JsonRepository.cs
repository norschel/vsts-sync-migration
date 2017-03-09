using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace VstsSyncMigrator.Store
{
    public class JsonRepository : IRepository<Object>
    {
        public object Add(object entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object entity)
        {
            throw new NotImplementedException();
        }

        public object Get(Expression<Func<object, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public ICollection<object> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(object entity)
        {
            throw new NotImplementedException();
        }
    }
}
