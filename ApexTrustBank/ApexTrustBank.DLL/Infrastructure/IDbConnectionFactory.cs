using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexTrustBank.DLL;


namespace ApexTrustBank.DLL.Infrastructure
{

    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
