using Contract;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repository
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private static SemaphoreSlim semaphore = new SemaphoreSlim(1);
        private Registration Config { get; set; }
        private ILogger Logger { get; set; }
        public RegistrationRepository(IOptions<Registration> registration, ILogger<IRegistrationRepository> logger)
        {
            Logger = logger;
            Config = registration != null ? registration.Value : throw new ArgumentNullException();
        }

        public async virtual Task<string> ReadFile()
        {
            semaphore.Wait();
            try
            {
                using var stream = new FileStream(Config.FileName, FileMode.OpenOrCreate);
                var sb = new StringBuilder();

                byte[] buffer = new byte[1024];
                int numRead;
                while ((numRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    string text = Encoding.Unicode.GetString(buffer, 0, numRead);
                    sb.Append(text);
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                Logger.LogError("Error reading from file");
                throw;
            }
            finally
            {
                semaphore.Release();
            }
        }

        public async virtual Task WriteFile(string data)
        {
            semaphore.Wait();
            try
            {
                using var stream = new FileStream(Config.FileName, FileMode.OpenOrCreate);
                byte[] encodedText = Encoding.Unicode.GetBytes(data);
                await stream.WriteAsync(encodedText, 0, encodedText.Length);
            }
            catch(Exception ex)
            {
                Logger.LogError("Error writing to file");
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
