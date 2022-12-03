using Contract;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private Registration Config { get; set; }
        public RegistrationRepository(IOptions<Registration> registration)
        {
            Config = registration != null ? registration.Value : throw new ArgumentNullException();
        }

        public async Task<string> ReadFile()
        {
            using var stream = new FileStream(Config.FileName, FileMode.OpenOrCreate);
            stream.Lock(0, stream.Length);

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

        public async Task WriteFile(string data)
        {
            using var stream = new FileStream(Config.FileName, FileMode.OpenOrCreate);
            stream.Lock(0, stream.Length);
            byte[] encodedText = Encoding.Unicode.GetBytes(data);
            await stream.WriteAsync(encodedText, 0, encodedText.Length);
        }
    }
}
