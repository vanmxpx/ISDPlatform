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

         [Route("tanks/{query}")]
        public Task Tanks()
        {
            return this.ProxyAsync("http://localhost:60001/");
        }

        [Route("islands/{query}")]
        public Task Islands()
        {
            return this.ProxyAsync("http://localhost:60002/");
        }

        [Route("example/{*query}")]
        public Task Get()
        {
            string a = this.Request.Path.Value;
            string b = this.Request.QueryString.Value;
            return this.ProxyAsync($"https://www.google.com/{b}");
        }

    }
}
