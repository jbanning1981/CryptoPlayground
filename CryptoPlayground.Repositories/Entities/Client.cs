using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPlayground.Repositories.Entities
{
    internal class Client
    {
        public long Id { get; set; }
        public Guid PublicClientId { get; set; }
        public string? ClientName { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; } = true;

        public virtual IEnumerable<ClientKey> ClientKey { get; set; }
    }
}
