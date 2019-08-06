using Cooper.Models;
using Cooper.Repositories;
using Cooper.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cooper.Controllers
{
    [Route("api/game/statistics")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class GameStatisticsController : ControllerBase
    {
        private readonly StatisticsRepository statisticsRepository;

        public GameStatisticsController(IConfigProvider configProvider)
        {
            statisticsRepository = new StatisticsRepository(configProvider);
        }

        // GET: api/<controller>
        // [HttpGet("{id}")]
        // public IEnumerable<Statistics> GetAllStatistics()
        // {
        //     return statisticsRepository.GetAll();
        // }

        // public IEnumerable<Statistics> GetAllStatisticsByUserId(long Id)
        // {
        //     return statisticsRepository.GetStatisticsByUser(Id);
        // }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IEnumerable<Statistics> GetAllStatisticByGameId(long id)
        {
            return statisticsRepository.GetStatisticsByGame(id);
        }

        [HttpGet]
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