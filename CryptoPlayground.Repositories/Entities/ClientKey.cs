using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPlayground.Repositories.Entities
{
    internal class ClientKey
    {
        public long ClientKeyId { get; set; }
        public long ClientId { get; set; }
        public Guid PublicKeyId { get; set; }
        public string Description { get; set; }
        public DateTimeOffset IssueDate { get; set; }
        public DateTimeOffset? ExpirationDate { get; set; }
        public string KeyHash { get; set; }

        public virtual Client Client { get; set; }
    }
}
