using System.Threading.Tasks;

namespace Contract
{
    public interface IRegistrationService
    {
        Task SaveUser(UserDetail userDetail);
    }
}
