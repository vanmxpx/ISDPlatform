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
        [Route("tanks/{*query}")]
        public Task Tanks()
        {
            string path = this.Request.Path.Value.Replace("/api/proxy/tanks/", "");
            string query = this.Request.QueryString.Value;
            return this.ProxyAsync($"http://localhost:60001/{path}{query}");
        }

        [Route("islands/{*query}")]
        public Task Islands()
        {
            string path = this.Request.Path.Value.Replace("/api/proxy/islands/", "");
            string query = this.Request.QueryString.Value;
            return this.ProxyAsync($"http://localhost:60002/{path}{query}");
        }

        [Route("example/{*query}")]
        public Task Get(string name)
        {
            string path = this.Request.Path.Value.Replace("/api/proxy/example/", "");
            string query = this.Request.QueryString.Value;
            return this.ProxyAsync($"http://www.google.com/{path}{query}");
        }

    }
}
