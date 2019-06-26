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

        const string tokens_table = "TOKENS";
        const string users_table = "USERS";
        private bool timerStart = false;
        private CRUD crud;
        private Timer timer;
        public TokenCleaner(IConfigProvider configProvider) {
            crud = new CRUD(configProvider);
            RemoveOutdated();
        }

        void RemoveOutdated() {
            VerificationDb verify;
            string now = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            //Get all users that don't verify email
            var unverified = crud.ReadBellow($"TO_TIMESTAMP(\'{now}\', 'DD.MM.YYYY HH24:MI:SS')", "ENDVERIFYDATE", attributes, tokens_table);
            var allTokens = crud.ReadFieldValues("t.TOKEN", $"{users_table} u INNER JOIN {tokens_table} t ON u.EMAIL = t.TOKEN");
            foreach (var entity in unverified) {
                EntityMapping.Map(entity, out verify);
                
                if (allTokens.Contains(verify.Token)) {
                    crud.Delete(verify.Token, users_table, "EMAIL");
                }
                crud.Delete($"'{verify.Token}'", tokens_table, "TOKEN");
            }
            timer.Stop();
            timer.Dispose();
            TryToStart();
        }

        string GetMinDate() {
            return crud.ReadFieldValues("MIN(ENDVERIFYDATE)", tokens_table)[0];
        }

        public void TryToStart() {
            if (!timerStart) {
                string date = GetMinDate();
                if (date != "") { 
                    timer = new Timer((int)((DateTime.Parse(date) - DateTime.Now).TotalMilliseconds));
                    timer.Elapsed += ( sender, e ) => RemoveOutdated();
                    timer.Start();
                        
                    Console.WriteLine((int)((DateTime.Parse(date) - DateTime.Now).TotalMilliseconds));
                    timerStart = true;
                }
            }
        }
    }
}