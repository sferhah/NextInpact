using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextInpact.Core.Data
{
    public static class SQLiteExtensions
    {
        public static int InsertOrReplaceAll(this SQLiteConnection connection, IEnumerable objects, Type objType)
        {
            var c = 0;
            connection.RunInTransaction(() =>
            {
                foreach (var r in objects)
                {
                    c += connection.InsertOrReplace(r, objType);
                }
            });
            return c;
        }
    }
}
