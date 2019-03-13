using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cooper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            return new JsonResult("value1");
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }
        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        //public class SampleDataController : Controller
        //{
        //    private static string[] Summaries = new[]
        //    {
        //        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //    };

        //    [HttpGet("[action]")]
        //    public IEnumerable<WeatherForecast> WeatherForecasts()
        //    {
        //        var rng = new Random();
        //        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //        {
        //            DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
        //            TemperatureC = rng.Next(-20, 55),
        //            Summary = Summaries[rng.Next(Summaries.Length)]
        //        });
        //    }

        //    public class WeatherForecast
        //    {
        //        public string DateFormatted { get; set; }
        //        public int TemperatureC { get; set; }
        //        public string Summary { get; set; }

        //        public int TemperatureF
        //        {
        //            get
        //            {
        //                return 32 + (int)(TemperatureC / 0.5556);
        //            }
        //        }
        //    }
        //}
    }
}
