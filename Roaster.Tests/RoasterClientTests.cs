using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roaster.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roaster.Tests
{
    [TestClass]
    public class RoasterClientTests
    {
        [TestMethod]
        public async Task GetPostResultAsync_NonExistingUri_ReturnsFailureAndException()
        {
            var roaster = RoasterClient.Create();

            var result = await roaster.GetPostResultAsync<string>(null);

            Assert.IsNotNull(result.Exception);
            Assert.AreEqual(ResultStatus.Failure, result.Status);
        }
    }
}
