using CryptoPlayground.Services;
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
            Assert.Equal(nameof(Request<string>.Data), ex.ParamName);
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
            Assert.Equal(nameof(Request<string>.Algorithm),ex.ParamName);
        }

        [Fact]
        public void CreateHash_ThrowsOnNullAlgorithm()
        {
            var request = CreateDefaultStringRequest(testString, null);
            var ex = Assert.Throws<ArgumentNullException>(() => testService.CreateHash(request));
            Assert.IsType<ArgumentNullException>(ex);
            Assert.Equal(nameof(Request<string>.Algorithm), ex.ParamName);
        }

        [Fact]
        public void CreateHash_WhenStringProvided_CreatesSha256Hash()
        {
            using var verifierHash = SHA256.Create();

            var compareHash = Convert.ToBase64String(verifierHash.ComputeHash(Encoding.UTF8.GetBytes(testString)));

            var request = CreateDefaultStringRequest(testString, nameof(SHA256));

            var generatedHash = testService.CreateHash(request);

            Assert.Equal(compareHash, generatedHash);
            
        }

        [Fact]
        public void CreateHash_WhenStringProvided_CreatesSha384Hash()
        {
            using var verifierHash = SHA384.Create();

            var compareHash = Convert.ToBase64String(verifierHash.ComputeHash(Encoding.UTF8.GetBytes(testString)));

            var request = CreateDefaultStringRequest(testString, nameof(SHA384));

            var generatedHash = testService.CreateHash(request);

            Assert.Equal(compareHash, generatedHash);

        }

        [Fact]
        public void CreateHash_WhenStringProvided_CreatesSha512Hash()
        {
            using var verifierHash = SHA512.Create();

            var compareHash = Convert.ToBase64String(verifierHash.ComputeHash(Encoding.UTF8.GetBytes(testString)));

            var request = CreateDefaultStringRequest(testString, nameof(SHA512));

            var generatedHash = testService.CreateHash(request);

            Assert.Equal(compareHash, generatedHash);

        }

        [Fact]
        public void CreateHash_WhenStringListProvided_CreatesSha256Hash()
        {
            using var verifierHash = SHA256.Create();
            var testStringCollection = testString.Replace(" ","");

            var compareHash = Convert.ToBase64String(verifierHash.ComputeHash(Encoding.UTF8.GetBytes(testStringCollection)));

            var request = CreateDefaultCollectionRequest(null, nameof(SHA256));

            var generatedHash = testService.CreateHash(request);

            Assert.Equal(compareHash, generatedHash);

        }

        [Fact]
        public void CreateHash_WhenStringListProvided_CreatesSha384Hash()
        {
            using var verifierHash = SHA384.Create();
            var testStringCollection = testString.Replace(" ", "");

            var compareHash = Convert.ToBase64String(verifierHash.ComputeHash(Encoding.UTF8.GetBytes(testStringCollection)));

            var request = CreateDefaultCollectionRequest(null, nameof(SHA384));

            var generatedHash = testService.CreateHash(request);

            Assert.Equal(compareHash, generatedHash);

        }

        [Fact]
        public void CreateHash_WhenStringListProvided_CreatesSha512Hash()
        {
            using var verifierHash = SHA512.Create();
            var testStringCollection = testString.Replace(" ", "");

            var compareHash = Convert.ToBase64String(verifierHash.ComputeHash(Encoding.UTF8.GetBytes(testStringCollection)));

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




        private Request<T> CreateRequest<T>(T data, string algorithm = nameof(SHA512))
        {
            return new Request<T>() { Data = data, Algorithm = algorithm };
        }

        private Request<string> CreateDefaultStringRequest(string? data = testString, string? algorithm = nameof(SHA512))
        {
            return new Request<string>() { Data = data, Algorithm = algorithm };
        }

        private Request<List<string>> CreateDefaultCollectionRequest(List<string>? data, string? algorithm = nameof(SHA512))
        {
            data ??= testString.Split(" ").ToList();

            return new Request<List<string>>() { Data = data, Algorithm = algorithm };
        }

        private Request<object> CreateDefaultObjectRequest(object? data, string? algorithm = nameof(SHA512))
        {
            return new Request<object>() { Data = data, Algorithm = algorithm };
        }

    }
}