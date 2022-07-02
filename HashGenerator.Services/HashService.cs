using System.Security.Cryptography;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;
using Ardalis.GuardClauses;
using System.Diagnostics.CodeAnalysis;

namespace HashGenerator.Services
{
    public class HashService
    {
        private readonly HashSet<string> _availableHashes = new HashSet<string>() { nameof(SHA256), nameof(SHA384), nameof(SHA512) };


        public string CreateHash<T>([MaybeNull] Request<T> hashRequest)
        {
            ValidateRequest(hashRequest);

            using var hash = GetHashAlgorithm(hashRequest.Algorithm);

            byte[] hashContents = BuildHashContents(hashRequest);

            return Convert.ToBase64String(hash.ComputeHash(hashContents));
        }
        private void ValidateRequest<T>(Request<T> hashRequest)
        {
            Guard.Against.Null(hashRequest);
            Guard.Against.Null(hashRequest.Data, nameof(hashRequest.Data));
            Guard.Against.Null(hashRequest.Algorithm, nameof(hashRequest.Algorithm));
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

            if(dataType == typeof(string))
            {
                return Encoding.UTF8.GetBytes(hashData as string ?? string.Empty);
            }

            if(dataType?.GetInterface(nameof(IEnumerable<string>)) is not null)
            {
                var concatString = string.Join(string.Empty, hashData);
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