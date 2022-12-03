using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Contract;
using Repository;

namespace Domain
{
    public class RegistrationDomain : IRegistrationService
    {
        private IRegistrationRepository Repository { get; set; }
        public RegistrationDomain(IRegistrationRepository repository) => Repository = repository;
        public async Task SaveUser(UserDetail userDetail)
        {
            userDetail.Password = userDetail.Password.Hash();
            var data = await Repository.ReadFile().ConfigureAwait(false);
            var users = new List<UserDetail>();
            if (!string.IsNullOrWhiteSpace(data))
            {
                users = JsonSerializer.Deserialize<List<UserDetail>>(data);
            }
            users.Add(userDetail);
            var serializedData = JsonSerializer.Serialize(users);
            await Repository.WriteFile(serializedData);
        }
    }
}
