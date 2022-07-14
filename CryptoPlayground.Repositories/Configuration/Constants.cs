using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPlayground.Repositories.Configuration
{
    internal class Constants
    {
        public const string InMemory = nameof(InMemory);
        public const string Sql = nameof(Sql);

        public readonly IEnumerable<string> DataStoreTypes = new HashSet<string>() { InMemory, Sql };
    }
}
