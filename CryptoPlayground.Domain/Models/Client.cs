using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPlayground.Domain.Models
{
    public class Client
    {
        public Guid ClientId { get; set; }
        public string? ClientName { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; } = true;

        public IEnumerable<ClientKey> ClientKeys { get; set; } = new List<ClientKey>();
    }
}
