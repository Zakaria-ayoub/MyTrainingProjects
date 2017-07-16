using CustomerAuthorizations.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAuthorizations.DataLayer
{
    public static class Helpers
    {
        public static EntityState ConvertState(ObjectState objectState)
        {
            switch (objectState)
            {
                case ObjectState.Added:
                    return EntityState.Added;
                case ObjectState.Modified:
                    return EntityState.Modified;
                case ObjectState.Deleted:
                    return EntityState.Deleted;
                default:
                    return EntityState.Unchanged;
            }
        }
        public static void ApplyStateChanges (this DbContext context)
        {
            foreach (var item in context.ChangeTracker.Entries<IObjectWithState>())
            {
                IObjectWithState stateInfo = item.Entity;
                item.State = ConvertState(stateInfo.ObjectState);
            }
        }
    }
}
