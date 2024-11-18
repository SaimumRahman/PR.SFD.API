
using JM.Middleware.Attributes;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
namespace JM.MFSAPI.Controllers
{
    
    [EnableCors("CorsPolicy")]
    [ApiController]
    [Authorization]
    [FromQueryBuildFilter]

    //[Route("api/v{version:apiVersion}/[controller]")]
    public class BaseAPIController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }

}
