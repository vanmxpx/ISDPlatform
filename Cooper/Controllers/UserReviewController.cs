using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cooper.Models;
using Cooper.Repository;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cooper.Controllers
{
    [Route("api/user/reviews")]
    public class UserReviewController : ControllerBase
    {
        // GET: api/<controller>
        UserReviewRepository userReviewRepository;

        public UserReviewController()
        {
            userReviewRepository = new UserReviewRepository();
        }

        // TODO: get all reviews for reviewed user method
        // TODO: get all reviews for user reviewer method

        [HttpGet]
        public IEnumerable<UserReview> GetAll()
        {
            return userReviewRepository.GetAll();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(UserReview))]
        [ProducesResponseType(404)]
        public IActionResult GetUserReviewById(long id)
        {
            UserReview userReview = userReviewRepository.Get(id);
            if (userReview == null)
            {
                return NotFound();
            }

            return Ok(userReview);
        }

        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        public IActionResult Post([FromBody]UserReview userReview)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (userReview.Id == 0)
            {
                long id = userReviewRepository.Create(userReview);
                userReview.Id = id;

                return Ok(userReview);
            }
            else
            {
                userReviewRepository.Update(userReview);

                return Ok(userReview);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            userReviewRepository.Delete(id);
            return Ok();
        }
    }
}
