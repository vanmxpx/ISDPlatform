using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCore.Proxy;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cooper.Controllers
{
    [Route("api/proxy")]
    public class ProxyController : Controller
    {
        // TODO: Make it scoped

        [Route("tanks")]
        public Task Tanks()
        {
            return this.ProxyAsync("https://localhost:60001/");
        }

        [Route("islands")]
        public Task Islands()
        {
            return this.ProxyAsync("https://localhost:60002/");
        }

    }
}
