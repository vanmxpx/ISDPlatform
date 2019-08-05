using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cooper.Models;
using Cooper.Repositories;
using Cooper.Configuration;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cooper.Controllers
{
    [Route("api/game/statistics")]
    public class GameStatisticsController : ControllerBase
    {
        StatisticsRepository statisticsRepository;

        public GameStatisticsController(IConfigProvider configProvider)
        {
            statisticsRepository = new StatisticsRepository(configProvider);
        }

        // GET: api/<controller>
        [HttpGet("{id}")]
        public IEnumerable<Statistics> GetAllStatistics()
        {
            return statisticsRepository.GetAll();
        }

        public IEnumerable<Statistics> GetAllStatisticsByUserId(long Id)    // get all game statistics for concrete user
        {
            return statisticsRepository.GetStatisticsByUser(Id);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IEnumerable<Statistics> GetAllStatisticByGameId(long id)                    // get all game statistics for concreate game
        {
            return statisticsRepository.GetStatisticsByGame(id);
        }

        public Statistics GetStatisticsById(long id)
        {
            return statisticsRepository.Get(id);
        }
        
        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        public IActionResult Post([FromBody]Statistics statistics)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (statistics.Id == 0)
            {
                long id = statisticsRepository.Create(statistics);
                statistics.Id = id;

                return Ok(statistics);
            }
            else
            {
                statisticsRepository.Update(statistics);

                return Ok(statistics);
            }
        }
        
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            statisticsRepository.Delete(id);
            return Ok();
        }
    }
}
