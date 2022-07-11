using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPlayground.Domain.Models
{
    public class ApiClient
    {
        public Guid ClientId { get; set; }
        public string ClientName { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; } = true;

        public List<ApiKey> ApiKeys { get; set; }
    }
}
