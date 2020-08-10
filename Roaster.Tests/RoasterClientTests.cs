using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roaster.Enums;
using System.Threading.Tasks;

namespace Roaster.Tests
{
    [TestClass]
    public class RoasterClientTests
    {
        [TestMethod]
        public async Task GetPostResultAsync_NonExistingUri_ReturnsFailureAndException()
        {
            var roaster = new RoasterClient();

            var result = await roaster.PostResultAsync<string>(null);

            Assert.IsNotNull(result.Exception);
            Assert.AreEqual(ResultStatus.Failure, result.Status);
        }
    }
}
