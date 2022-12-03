using System.Threading.Tasks;

namespace Contract
{
    public interface IRegistrationRepository
    {
        Task<string> ReadFile();
        Task WriteFile(string data);
    }
}
