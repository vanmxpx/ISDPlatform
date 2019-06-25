using Cooper.Configuration;
using Cooper.DAO.Models;
using Cooper.DAO.Mapping;
using Cooper.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Cooper.Services
{
    class TokenCleaner : ITokenCleaner
    {
        readonly HashSet<string> attributes = new HashSet<string>()
        {
            "TOKEN",
            "ENDVERIFYDATE"
        };

        const string table = "TOKENS";
        const string users_table = "USERS";
        bool timerStart = false;
        private CRUD crud;
        public TokenCleaner(IConfigProvider configProvider) {
            crud = new CRUD(configProvider);
            TryToStart(GetMinDateEntities().ToList());
        }

        IEnumerable<VerificationDb> GetMinDateEntities() {
            VerificationDb verify;
            var entities = crud.ReadAll("(SELECT MIN(ENDVERIFYDATE) FROM TOKENS)", "ENDVERIFYDATE", attributes, table);
            foreach (var entity in entities) {
                EntityMapping.Map(entity, out verify);
                yield return verify;
            }
        }

        void RemoveByTokens(IEnumerable<string> tokens) {
            foreach (string token in tokens) {
                crud.Delete($"'{token}'", table, "TOKEN");
                crud.Delete("(SELECT u.ID FROM USERS u INNER JOIN TOKENS t ON u.EMAIL = t.TOKEN)", users_table, "ID");
                timerStart = false;
            }
            TryToStart(GetMinDateEntities().ToList());
        }

        List<VerificationDb> CheckOutdated(List<VerificationDb> entities) {
            if (entities.Count > 0 && (entities[0].EndVerifyDate - DateTime.Now).TotalMilliseconds < 0) { //Outdated
                RemoveByTokens(entities.Select(item => item.Token));
                entities = CheckOutdated(GetMinDateEntities().ToList());
            }
            
            return entities;
        }

        void TryToStart(List<VerificationDb> entities) {
            if (!timerStart && entities.Count > 0) {
                entities = CheckOutdated(entities);
                if (entities.Count > 0) {
                    Timer timer = new Timer((int)((entities[0].EndVerifyDate - DateTime.Now).TotalMilliseconds));
                    timer.Elapsed += async ( sender, e ) => await Task.Run(()=>RemoveByTokens(entities.Select(item => item.Token)));
                    timer.Start();
                    
                    Console.WriteLine((int)((entities[0].EndVerifyDate - DateTime.Now).TotalMilliseconds));
                    timerStart = true;
                }
                else
                {
                    timerStart = false;
                }
            }
        }

        public void TryToStart() {
            TryToStart(GetMinDateEntities().ToList());
        }
    }
}