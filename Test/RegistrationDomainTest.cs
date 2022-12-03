using Contract;
using Domain;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class RegistrationDomainTest
    {
        private RegistrationDomain Service { get; set; } 

        private Mock<IRegistrationRepository> Repo { get; set; }
        public RegistrationDomainTest()
        {
            Repo = new Mock<IRegistrationRepository>();
            Repo.Setup(x => x.ReadFile()).ReturnsAsync(string.Empty);
            Repo.Setup(x => x.WriteFile(It.IsAny<string>()));
            Service = new RegistrationDomain(Repo.Object);
        }

        [Fact]
        public async Task GivenNoFile_WhenFileRead_ThenEmptyFileCreatedWithNoData()
        {            
            await Service.SaveUser(new UserDetail() { Email = "123@gmail.com", Password = "1234"}).ConfigureAwait(false);
            Repo.Verify(x => x.ReadFile(), Times.Once);
            Repo.Verify(x => x.WriteFile(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void GivenPassword_WhenSaveUser_ThenPasswordHashed()
        {
            var password = "123";
            var hash = password.Hash();
            Assert.NotNull(hash);
            Assert.NotEmpty(hash);
        }
    }
}
