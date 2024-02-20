using NUnit.Framework;
using System.Threading.Tasks;

namespace CHANGE_TO_APP_NAME.Services.Application.IntegrationTests
{
    using static Testing;

    public class TestBase
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();
        }
    }
}
