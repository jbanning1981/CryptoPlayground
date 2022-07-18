using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPlayground.Services.Configuration
{
    internal class ClientKeyConfiguration
    {
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
        public string Password { get; set; }

    }
}
