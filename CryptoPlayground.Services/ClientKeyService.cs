using Ardalis.GuardClauses;
using CryptoPlayground.Domain.Models;
using CryptoPlayground.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPlayground.Services
{
    internal class ClientKeyService
    {
        private readonly ClientKeyConfiguration keyConfig;
        private readonly byte[] key;

        public ClientKeyService(ClientKeyConfiguration keyConfiguration)
        {
            Guard.Against.Null(keyConfiguration);
            keyConfig = keyConfiguration;
        }

        public ClientKey GenerateKey(Guid clientId)
        {
            using var rsaCrypto = new RSACryptoServiceProvider();
            //rsaCrypto.

            return null;
        }


        public ValueTask<bool> IsValidKey(string keyToCheck) { return ValueTask.FromResult(true); }

    }
}
