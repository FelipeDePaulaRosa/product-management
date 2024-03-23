using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        protected readonly ISender Sender;
        
        public ApiController(ISender sender)
        {
            Sender = sender;
        }
    }
}