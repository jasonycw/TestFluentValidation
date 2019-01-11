using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TestFluentValidation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ValuesController(IMediator mediator) => _mediator = mediator;

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Hellow", "World" };
        }

        // GET api/values/test1
        [HttpGet("test1")]
        public async Task<ActionResult<Get.Model>> Test1() 
            => await _mediator.Send(new Get.Query { Email = "asdf" });

        // GET api/values/test2
        [HttpGet("test2")]
        public async Task<ActionResult<Get.Model>> Test2() 
            => await _mediator.Send(new Get.Query { Email = "asdf@asdf.com" });

        // GET api/values/test3
        [HttpGet("test3")]
        public async Task<ActionResult<Get.Model>> Test3() 
            => await _mediator.Send(new Get.Query { Email = "asdf@gmail.com" });

        // GET api/values/test4
        [HttpGet("test4")]
        public async Task<ActionResult<Get.Model>> Test4() 
            => await _mediator.Send(new Get.Query { Email = null });
    }
}
