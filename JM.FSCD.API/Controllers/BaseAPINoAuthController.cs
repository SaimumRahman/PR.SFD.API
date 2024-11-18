using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JM.Middleware.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
namespace JM.FSCD.API.Controllers
{
    
    [EnableCors("CorsPolicy")]
    [ApiController]
    //[Authorization]
    [FromQueryBuildFilter]

    //[Route("api/v{version:apiVersion}/[controller]")]
    public class BaseAPINoAuthController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }

}
