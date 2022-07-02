using HashGenerator.Services;
using System.Security.Cryptography;

namespace HashGenerator.Tests
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
            var request = CreateDefaultRequest(null);
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
            var request = CreateDefaultRequest(testString, invalidAlgo);
            var ex = Assert.Throws<ArgumentException>(() => testService.CreateHash(request));
            Assert.IsType<ArgumentException>(ex);
            Assert.Equal(nameof(Request<string>.Algorithm),ex.ParamName);
        }

        [Fact]
        public void CreateHash_ThrowsOnNullAlgorithm()
        {
            var request = CreateDefaultRequest(testString, null);
            var ex = Assert.Throws<ArgumentNullException>(() => testService.CreateHash(request));
            Assert.IsType<ArgumentNullException>(ex);
            Assert.Equal(nameof(Request<string>.Algorithm), ex.ParamName);
        }

        private Request<T> CreateRequest<T>(T data, string algorithm = nameof(SHA512))
        {
            return new Request<T>() { Data = data, Algorithm = algorithm };
        }

        private Request<string> CreateDefaultRequest(string? data = testString, string? algorithm = nameof(SHA512))
        {
            return new Request<string>() { Data = data, Algorithm = algorithm };
        }
    }
}