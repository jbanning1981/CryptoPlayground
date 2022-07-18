using CryptoPlayground.Domain.Models;
using System.Security.Cryptography;

namespace CryptoPlayground.Services
{
    public interface IHashService
    {
        string CreateHash(string algorithm, string data);
        string CreateHash<T>(HashRequest<T> hashRequest);
        string GenerateBaseEncodedHash(HashAlgorithm hashAlg, byte[] dataToHash);
    }
}