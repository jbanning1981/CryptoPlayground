using CryptoPlayground.Domain.Models;
using CryptoPlayground.Services;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;


namespace CryptoPlayground.Tests
{
    public class HashServiceTests
    {

        private HashService testService = new HashService();
        private const string testString = "Nothing is guaranteed until it has been tested";


        [Fact]
        public void CreateHash_ThrowsOnNullInput()
        {
            _ = Assert.Throws<ArgumentNullException>(() => testService.CreateHash<object>(null));
        }

        [Fact]
        public void CreateHash_ThrowsOnNullData()
        {
            var request = CreateDefaultStringRequest(null);
            var ex = Assert.Throws<ArgumentNullException>(() => testService.CreateHash(request));
            Assert.IsType<ArgumentNullException>(ex);
            Assert.Equal(nameof(HashRequest<string>.Data), ex.ParamName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("NotAValidHashAlgorithm")]
        public void CreateHash_ThrowsOnInvalidAlgorithm(string invalidAlgo)
        {
            var request = CreateDefaultStringRequest(testString, invalidAlgo);
            var ex = Assert.Throws<ArgumentException>(() => testService.CreateHash(request));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal(nameof(HashRequest<string>.Algorithm),ex.ParamName);
        }

        [Fact]
        public void CreateHash_ThrowsOnNullAlgorithm()
        {
            var request = CreateDefaultStringRequest(testString, null);
            var ex = Assert.Throws<ArgumentNullException>(() => testService.CreateHash(request));
            Assert.IsType<ArgumentNullException>(ex);
            Assert.Equal(nameof(HashRequest<string>.Algorithm), ex.ParamName);
        }

        [Fact]
        public void CreateHash_WhenStringProvided_CreatesSha256Hash()
        {
            using var verifierHash = SHA256.Create();

            var compareHash = Base64UrlEncoder.Encode(verifierHash.ComputeHash(Encoding.UTF8.GetBytes(testString)));

            var request = CreateDefaultStringRequest(testString, nameof(SHA256));

            var generatedHash = testService.CreateHash(request);

            Assert.Equal(compareHash, generatedHash);
            
        }

        [Fact]
        public void CreateHash_WhenStringProvided_CreatesSha384Hash()
        {
            using var verifierHash = SHA384.Create();

            var compareHash = Base64UrlEncoder.Encode(verifierHash.ComputeHash(Encoding.UTF8.GetBytes(testString)));

            var request = CreateDefaultStringRequest(testString, nameof(SHA384));

            var generatedHash = testService.CreateHash(request);

            Assert.Equal(compareHash, generatedHash);

        }

        [Fact]
        public void CreateHash_WhenStringProvided_CreatesSha512Hash()
        {
            using var verifierHash = SHA512.Create();

            var compareHash = Base64UrlEncoder.Encode(verifierHash.ComputeHash(Encoding.UTF8.GetBytes(testString)));

            var request = CreateDefaultStringRequest(testString, nameof(SHA512));

            var generatedHash = testService.CreateHash(request);

            Assert.Equal(compareHash, generatedHash);

        }

        [Fact]
        public void CreateHash_WhenStringListProvided_CreatesSha256Hash()
        {
            using var verifierHash = SHA256.Create();
            var testStringCollection = testString.Replace(" ","");

            var compareHash = Base64UrlEncoder.Encode(verifierHash.ComputeHash(Encoding.UTF8.GetBytes(testStringCollection)));

            var request = CreateDefaultCollectionRequest(null, nameof(SHA256));

            var generatedHash = testService.CreateHash(request);

            Assert.Equal(compareHash, generatedHash);

        }

        [Fact]
        public void CreateHash_WhenStringListProvided_CreatesSha384Hash()
        {
            using var verifierHash = SHA384.Create();
            var testStringCollection = testString.Replace(" ", "");

            var compareHash = Base64UrlEncoder.Encode(verifierHash.ComputeHash(Encoding.UTF8.GetBytes(testStringCollection)));

            var request = CreateDefaultCollectionRequest(null, nameof(SHA384));

            var generatedHash = testService.CreateHash(request);

            Assert.Equal(compareHash, generatedHash);

        }

        [Fact]
        public void CreateHash_WhenStringListProvided_CreatesSha512Hash()
        {
            using var verifierHash = SHA512.Create();
            var testStringCollection = testString.Replace(" ", "");

            var compareHash = Base64UrlEncoder.Encode(verifierHash.ComputeHash(Encoding.UTF8.GetBytes(testStringCollection)));

            var request = CreateDefaultCollectionRequest(null, nameof(SHA512));

            var generatedHash = testService.CreateHash(request);

            Assert.Equal(compareHash, generatedHash);

        }

        [Fact]
        public void CreateHash_WhenObjectProvided_CreatesSha256Hash()
        {
            using var verifierHash = SHA256.Create();
            var testObj = new { FirstName = "Luke", LastName = "Skywalker" };

            var request = CreateDefaultObjectRequest(testObj, nameof(SHA256));

            var generatedHash = testService.CreateHash(request);

            Assert.False(string.IsNullOrWhiteSpace(generatedHash));

        }

        [Fact]
        public void CreateHash_WhenObjectProvided_CreatesSha384Hash()
        {
            using var verifierHash = SHA384.Create();
            var testObj = new { FirstName = "Luke", LastName = "Skywalker" };

            var request = CreateDefaultObjectRequest(testObj, nameof(SHA384));

            var generatedHash = testService.CreateHash(request);

            Assert.False(string.IsNullOrWhiteSpace(generatedHash));

        }

        [Fact]
        public void CreateHash_WhenObjectProvided_CreatesSha512Hash()
        {
            using var verifierHash = SHA512.Create();
            var testObj = new { FirstName = "Luke", LastName = "Skywalker" };

            var request = CreateDefaultObjectRequest(testObj, nameof(SHA512));

            var generatedHash = testService.CreateHash(request);

            Assert.False(string.IsNullOrWhiteSpace(generatedHash));

        }




        private HashRequest<T> CreateHashHashRequest<T>(T data, string algorithm = nameof(SHA512))
        {
            return new HashRequest<T>() { Data = data, Algorithm = algorithm };
        }

        private HashRequest<string> CreateDefaultStringRequest(string? data = testString, string? algorithm = nameof(SHA512))
        {
            return new HashRequest<string>() { Data = data, Algorithm = algorithm };
        }

        private HashRequest<List<string>> CreateDefaultCollectionRequest(List<string>? data, string? algorithm = nameof(SHA512))
        {
            data ??= testString.Split(" ").ToList();

            return new HashRequest<List<string>>() { Data = data, Algorithm = algorithm };
        }

        private HashRequest<object> CreateDefaultObjectRequest(object? data, string? algorithm = nameof(SHA512))
        {
            return new HashRequest<object>() { Data = data, Algorithm = algorithm };
        }

    }
}