using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cooper.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cooper.Controllers
{
    [Route("api/user/reviews")]
    [ApiController]
    public class UserReviewController : ControllerBase
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> GetReviewesForUser()     // all reviews for concrete user from others users
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        public IEnumerable<string> GetUserReviews()     // all user reviews about others users
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
