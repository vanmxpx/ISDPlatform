using Cooper.Configuration;
using Cooper.DAO.Mapping;
using Cooper.DAO.Models;
using Cooper.ORM;
using Cooper.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace Cooper.Services
{
    class TokenCleaner : ITokenCleaner
    {
        private readonly CRUD crud;
        private Timer timer;

        private readonly HashSet<string> attributes = new HashSet<string>()
        {
            "TOKEN",
            "ENDVERIFYDATE"
        };

        private readonly HashSet<string> token_attribute = new HashSet<string>() { "TOKEN" };
        private readonly HashSet<string> min_date = new HashSet<string>() { "MIN(ENDVERIFYDATE)" };

        const string tokens_table = "TOKENS";
        const string users_table = "USERS";
        private bool timerStart = false;

        public TokenCleaner(IConfigProvider configProvider)
        {
            crud = new CRUD(configProvider);
            RemoveOutdated();
        }

        private void RemoveOutdated()
        {
            VerificationDb unverify;
            string now = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            //Get all users that don't verify email
            var whereRequest = new WhereRequest("ENDVERIFYDATE", Operators.Less, $"TO_TIMESTAMP(\'{now}\', 'DD.MM.YYYY HH24:MI:SS')");
            var unverified = crud.Read(tokens_table, attributes, whereRequest);
            var allTokens = crud.Read($"{users_table} u INNER JOIN {tokens_table} t ON u.EMAIL = t.TOKEN", token_attribute).Select(item => item.attributeValue["TOKEN"]).ToList();
            foreach (var entity in unverified) {
                EntityMapping.Map(entity, out unverify);
                
                if (allTokens.Contains(unverify.Token)) {
                    crud.Delete($"'{unverify.Token}'", users_table, "EMAIL");
                }
                crud.Delete($"'{unverify.Token}'", tokens_table, "TOKEN");
            }
            if (timer != null) {
                timer.Stop();
                timer.Dispose();
                timerStart = false;
            }
            TryToStart();
        }

        private string GetMinDate()
        {
            string result = "";
            List<EntityORM> data = (List<EntityORM>)crud.Read(tokens_table, min_date);

            if (data.Any())
            { 
                result = data[0].attributeValue["ENDVERIFYDATE"].ToString();
            }

            return result;
        }

        public void TryToStart()
        {
            if (!timerStart) {
                string date = GetMinDate();
                if (date != "")
                { 
                    timer = new Timer((int)((DateTime.Parse(date) - DateTime.Now).TotalMilliseconds));
                    timer.Elapsed += ( sender, e ) => RemoveOutdated();
                    timer.Start();

                    timerStart = true;
                }
            }
        }
    }
}