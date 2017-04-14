using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextInpact.Data
{
    public interface IStringConnectionProvider
    {
        String GetConnectionString(string dbName);
    }
}
