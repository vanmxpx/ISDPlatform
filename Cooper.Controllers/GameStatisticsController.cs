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
        private readonly ISession session;

        public GameStatisticsController(ISessionFactory sessionFactory)
        {
            session = sessionFactory.FactoryMethod();
            statisticsRepository = new StatisticsRepository(session);
        }

        [HttpGet("{id}")]
        public IEnumerable<Statistics> GetAllStatisticByGameId(long id)
        {
            var statistics = statisticsRepository.GetStatisticsByGame(id);

            session.EndSession();

            return statistics;

        }

        [HttpGet]
        public Statistics GetStatisticsById(long id)
        {
            var statistics = statisticsRepository.Get(id);

            session.EndSession();

            return statistics;
        }

        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        public IActionResult Post([FromBody]Statistics statistics)
        {

            IActionResult result;

            session.StartSession();

            bool isSuccessfull = true;

            if (statistics.Id == 0)
            {
                long id = statisticsRepository.Create(statistics);
                statistics.Id = id;

                isSuccessfull &= (statistics.Id != 0);
            }
            else
            {
                isSuccessfull = statisticsRepository.Update(statistics);
            }

            if (isSuccessfull)
            {
                session.Commit(endSession: true);
                result = Ok(statistics);
            }
            else
            {
                session.Rollback(endSession: true);
                result = StatusCode(500, "Connection to database failed");
            }

            return result;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            IActionResult result;
            bool isDeleted = statisticsRepository.Delete(id);

            if (isDeleted)
            {
                session.Commit(endSession: true);
                result = Ok();
            }
            else
            {
                session.Rollback(endSession: true);
                result = StatusCode(500, "Connection to database failed");
            }

            return result;
        }
    }
}