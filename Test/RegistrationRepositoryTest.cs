using Contract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Repository;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class RegistrationRepositoryTest
    {
        private string file = "data.json";
        private RegistrationRepository Repository { get; set; }

        public RegistrationRepositoryTest()
        {
            file = Path.Combine(Directory.GetCurrentDirectory(), file);
            var mockLogger = new Mock<ILogger>();
            IOptions<Registration> options = Options.Create(
                new Registration()
                {
                    FileName = Path.Combine(Directory.GetCurrentDirectory(), file),
                });
            Repository = new RegistrationRepository(options, mockLogger.Object);
        }

        [Fact]
        public async Task AGivenNoFile_WhenFileRead_ThenEmptyFileCreatedWithNoData()
        {
            CleanUp();
            var data = await Repository.ReadFile().ConfigureAwait(false);
            Assert.Equal(string.Empty, data);
        }

        [Fact]
        public async Task BGivenNoFile_WhenFileWrite_ThenEmptyFileCreatedWithDatatThenRead()
        {
            CleanUp();
            var datatxt = "data";
            await Repository.WriteFile(datatxt).ConfigureAwait(false);
            var data = await Repository.ReadFile().ConfigureAwait(false);
            Assert.Equal(datatxt, data);
            CleanUp();
        }

        private void CleanUp()
        {
            if(File.Exists(file))
               File.Delete(file);
        }

    }
}
