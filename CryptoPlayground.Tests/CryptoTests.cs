using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPlayground.Tests
{
    [Trait("Category", "Test")]
    public class CryptoTests
    {


        [Fact]
        public void Temp()
        {
            using var rsa = new RSACryptoServiceProvider();

            var blob = rsa.ExportCspBlob(true);

            var cryptoParam = rsa.ExportParameters(true);

            var privateKey = rsa.ExportRSAPrivateKey();
            var publicKey = rsa.ExportRSAPublicKey();

            var webPrivateKey = Base64UrlEncoder.Encode(privateKey);
            var webPublicKey = Base64UrlEncoder.Encode(publicKey);










            Assert.True(true);
        }
    }
}
