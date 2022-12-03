using Contract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SampleRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private IRegistrationService Service { get; set; }
        public RegistrationController(IRegistrationService service) => Service = service;

        [HttpPost]
        [Route("save")]
        public async Task SaveUser(UserDetail user) => await Service.SaveUser(user).ConfigureAwait(false);
    }
}
