using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooper.ORM;
using Cooper.DAO.Models;

namespace Cooper.DAO.Mapping
{
    public class EntityMapping
    {

        #region Game/entity mapping

        ///<summary>
        ///Maps properties from EntityORM object to GameDb object
        ///</summary>
        public static void Map(EntityORM entity, out GameDb game)
        {
            game = new GameDb();

            foreach (KeyValuePair<string, object> aV in entity.attributeValue)
            {
                switch (aV.Key)  // entity attribute
                {
                    case "ID":
                        game.Id = Convert.ToInt64(aV.Value);
                        break;
                    case "NAME":
                        game.Name = aV.Value.ToString();
                        break;
                    case "DESCRIPTION":
                        game.Description = aV.Value.ToString();
                        break;
                    case "GENRE":
                        game.Genre = aV.Value.ToString();
                        break;
                    case "LINK":
                        game.Link = aV.Value.ToString();
                        break;
                    case "LOGOURL":
                        game.LogoURL = aV.Value.ToString();
                        break;
                    case "COVERURL":
                        game.CoverURL = aV.Value.ToString();
                        break;
                    case "ISVERIFIED":
                        game.IsVerified = ((string)aV.Value == "y") ? true : false;
                        break;
                    default:
                        break;
                }
            }
        }

        ///<summary>
        ///Maps properties from GameDb object to EntityORM object
        ///</summary>
        public static EntityORM Map(GameDb game, HashSet<string> attributes)
        {
            EntityORM entity = new EntityORM();

            foreach (string attribute in attributes)
            {
                object value = null;        // attribute value

                switch (attribute)
                {
                    case "NAME":
                        value = game.Name;
                        break;
                    case "DESCRIPTION":
                        value = game.Description;
                        break;
                    case "GENRE":
                        value = game.Genre;
                        break;
                    case "LINK":
                        value = game.Link;
                        break;
                    case "LOGOURL":
                        value = game.LogoURL;
                        break;
                    case "COVERURL":
                        value = game.CoverURL;
                        break;
                    case "ISVERIFIED":
                        value = game.IsVerified;
                        break;
                    default:
                        break;
                }

                entity.attributeValue.Add(attribute, value);
            }

            return entity;
        }
        #endregion

        #region User/entity mapping
        
        ///<summary>
        ///Maps properties from EntityORM object to UserDb object
        ///</summary>
        public static void Map(EntityORM entity, out UserDb user)
        {
            user = new UserDb();

            foreach (KeyValuePair<string, object> aV in entity.attributeValue)
            {
                switch (aV.Key)        // attribute
                {
                    case "ID":
                        user.Id = Convert.ToInt64(aV.Value);
                        break;
                    case "NAME":
                        user.Name = aV.Value.ToString();
                        break;
                    case "NICKNAME":
                        user.Nickname = aV.Value.ToString();
                        break;
                    case "EMAIL":
                        user.Email = aV.Value.ToString();
                        break;
                    case "PASSWORD":
                        user.Password = aV.Value.ToString();
                        break;
                    case "PHOTOURL":
                        user.PhotoURL = aV.Value.ToString();
                        break;
                    case "ISVERIFIED":
                        user.IsVerified = ((string)aV.Value == "y") ? true : false;
                        break;
                    case "ISCREATOR":
                        user.IsCreator = ((string)aV.Value == "y") ? true : false;
                        break;
                    case "ISBANNED":
                        user.IsBanned = ((string)aV.Value == "y") ? true : false;
                        break;
                    case "ENDBANDATE":
                        user.EndBanDate = (DateTime)aV.Value;
                        break;
                    case "PLATFORMLANGUAGE":
                        user.PlatformLanguage = aV.Value.ToString();
                        break;
                    case "PLATFORMTHEME":
                        user.PlatformTheme = aV.Value.ToString();
                        break;
                    default:
                        break;
                }
            }
        }


        ///<summary>
        ///Maps properties from UserDb object to EntityORM object
        ///</summary>
        public static EntityORM Map(UserDb user, HashSet<string> attributes)
        {
            EntityORM entity = new EntityORM();

            foreach (string attribute in attributes)
            {
                object value = null;    // attribute value

                switch (attribute)
                {
                    case "NAME":
                        value = $"\'{user.Name}\'";
                        break;
                    case "NICKNAME":
                        value = $"\'{user.Nickname}\'";
                        break;
                    case "EMAIL":
                        value = $"\'{user.Email}\'";
                        break;
                    case "PASSWORD":
                        value = $"\'{ user.Password}\'";
                        break;
                    case "PHOTOURL":
                        value = $"\'{user.PhotoURL}\'";
                        break;
                    case "ISVERIFIED":
                        value = (user.IsVerified) ? "\'y\'" : "\'n\'";
                        break;
                    case "ISCREATOR":
                        value = (user.IsCreator) ? "\'y\'" : "\'n\'";
                        break;
                    case "ISBANNED":
                        value = (user.IsBanned) ? "\'y\'" : "\'n\'";
                        break;
                    case "ENDBANDATE":
                        value = $"\'{ToOracleDateFormat(user.EndBanDate)}\'";
                        break;
                    case "PLATFORMLANGUAGE":
                        value = $"\'{user.PlatformLanguage}\'";
                        break;
                    case "PLATFORMTHEME":
                        value = $"\'{user.PlatformTheme}\'";
                        break;
                    default:
                        break;
                }

                entity.attributeValue.Add(attribute, value);
            }

            return entity;
        }

        #endregion

        #region Helping methods

        private static string ToOracleDateFormat(DateTime dateTime)
        {
            string day = dateTime.Day.ToString();
            string month = dateTime.Month.ToString();
            string year = dateTime.Year.ToString();

            switch (month)
            {
                case "1":
                    month = "JAN";
                    break;
                case "2":
                    month = "FEB";
                    break;
                case "3":
                    month = "MAR";
                    break;
                case "4":
                    month = "APR";
                    break;
                case "5":
                    month = "MAN";
                    break;
                case "6":
                    month = "JUN";
                    break;
                case "7":
                    month = "JUL";
                    break;
                case "8":
                    month = "AUG";
                    break;
                case "9":
                    month = "SEP";
                    break;
                case "10":
                    month = "OCT";
                    break;
                case "11":
                    month = "NOV";
                    break;
                case "12":
                    month = "DEC";
                    break;
            }

            string oracleDateFormat = $"{day}-{month}-{year}";

            return oracleDateFormat;
        }

        #endregion
    }

}

