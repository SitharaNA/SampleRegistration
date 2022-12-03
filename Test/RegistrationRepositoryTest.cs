using Contract;
using Microsoft.Extensions.Options;
using Repository;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class RegistrationRepositoryTest
    {
        private const string file = @"C:\data.json";
        private RegistrationRepository Repository { get; set; }

        public RegistrationRepositoryTest()
        {
            IOptions<Registration> options = Options.Create(new Registration() { FileName = file });
            Repository = new RegistrationRepository(options);
        }

        [Fact]
        public async Task AGivenNoFile_WhenFileRead_ThenEmptyFileCreatedWithNoData()
        {
            var data = await Repository.ReadFile().ConfigureAwait(false);
            Assert.Equal(string.Empty, data);
        }

    }
}
