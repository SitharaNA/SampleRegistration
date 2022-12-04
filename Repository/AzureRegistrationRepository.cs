using Azure.Storage.Blobs;
using Contract;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AzureRegistrationRepository: RegistrationRepository
    {
        private Registration Config { get; set; }
        public AzureRegistrationRepository(IOptions<Registration> registration, ILogger<IRegistrationRepository> logger)
            :base(registration, logger)
        {
            Config = registration != null ? registration.Value : throw new ArgumentNullException();
        }

        public async override Task WriteFile(string data)
        {
            await base.WriteFile(data).ConfigureAwait(false);

            BlobClient blobClient = new BlobClient(
                connectionString: Config.Connection,
                blobContainerName: Config.Container,
                blobName: "data");

            blobClient.Upload(Config.FileName, true);
        }
    }
}
