using System.Security.Cryptography;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;
using Ardalis.GuardClauses;
using System.Diagnostics.CodeAnalysis;
using CryptoPlayground.Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace CryptoPlayground.Services
{
    public class HashService : IHashService
    {
        //SHA-1 not provided as it is no longer cryptographically secure.
        private readonly HashSet<string> _availableHashes = new HashSet<string>() { nameof(SHA256), nameof(SHA384), nameof(SHA512) };


        public string CreateHash<T>(HashRequest<T> hashRequest)
        {
            ValidateHashRequest(hashRequest);

            using var hashAlg = GetHashAlgorithm(hashRequest?.Algorithm);

            byte[] hashContents = BuildHashContents(hashRequest.Data);

            return GenerateBaseEncodedHash(hashAlg, hashContents);
        }

        public string CreateHash(string algorithm, string data)
        {
            Guard.Against.NullOrWhiteSpace(algorithm, nameof(algorithm));
            Guard.Against.NullOrWhiteSpace(data, nameof(data));

            using var hashAlg = GetHashAlgorithm(algorithm);

            byte[] hashContents = BuildHashContents(data);

            return GenerateBaseEncodedHash(hashAlg, hashContents);
        }

        public string GenerateBaseEncodedHash(HashAlgorithm hashAlg, byte[] dataToHash)
        {
            var hash = hashAlg.ComputeHash(dataToHash);
            return Base64UrlEncoder.Encode(hash);
        }


        private void ValidateHashRequest<T>(HashRequest<T> hashRequest)
        {
            Guard.Against.Null(hashRequest);
            Guard.Against.Null(hashRequest.Data, nameof(hashRequest.Data));
            Guard.Against.NullOrWhiteSpace(hashRequest.Algorithm, nameof(hashRequest.Algorithm));
            Guard.Against.InvalidInput(hashRequest.Algorithm, nameof(hashRequest.Algorithm), (alg) => _availableHashes.Contains(alg), "No hash provider available for the provided value");
        }

        private HashAlgorithm GetHashAlgorithm(string hashName)
        {
            switch (hashName)
            {
                case nameof(SHA256): return SHA256.Create();
                case nameof(SHA384): return SHA384.Create();
                case nameof(SHA512): return SHA512.Create();
                default: throw new InvalidOperationException($"No hash matched the provided value {hashName}");
            }
        }


        private byte[] BuildHashContents<T>([NotNull] T hashData)
        {

            var dataType = hashData!.GetType();

            if (dataType == typeof(string))
            {
                return Encoding.UTF8.GetBytes(hashData as string ?? string.Empty);
            }

            if (dataType?.GetInterface(nameof(IEnumerable<string>)) is not null)
            {
                var concatString = string.Join(string.Empty, hashData as IEnumerable<string>);
                return Encoding.UTF8.GetBytes(concatString);
            }

            return ObjectToByteArray(hashData);
        }

        // BinaryFormatter is deprecated
        // Give credit where credit is due: https://stackoverflow.com/a/68050536
        private byte[] ObjectToByteArray([NotNull] object objData)
        {
            return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(objData, GetJsonSerializerOptions()));
        }

        private static JsonSerializerOptions GetJsonSerializerOptions()
        {
            return new JsonSerializerOptions()
            {
                PropertyNamingPolicy = null,
                WriteIndented = true,
                AllowTrailingCommas = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };
        }


    }
}