using System.Collections.Generic;
using System.Linq;

namespace Cooper.ORM
{
    public class DbTools
    {
        public static string GetVariableAttribute(string attribute)
        {
            int start_pos = attribute.IndexOf("(");
            if (start_pos != -1)
            {
                start_pos++;
                attribute = attribute.Substring(start_pos, attribute.IndexOf(")") - start_pos);
            }
            return attribute;
        }

        public static string CreateSelectQuery(string table, HashSet<string> attributes, WhereRequest whereRequest = null)
        {
            return string.Format("SELECT {0} FROM {1} {2}",
                (attributes != null) ? string.Join(", ", attributes) : "*",
                table, whereRequest != null ? "WHERE " + whereRequest.ToString() : null);
        }

        public static string CreateUpdateQuery(string table, EntityORM entity, WhereRequest whereRequest = null)
        {
            return string.Format("UPDATE {0} SET {1} {2}", table, string.Join(",", entity.attributeValue.Select(x => x.Key + "=" + x.Value).ToArray()), whereRequest != null ? "WHERE " + whereRequest.ToString() : null);
        }

        public static string CreateDeleteQuery(string table, WhereRequest whereRequest = null)
        {
            return string.Format("DELETE FROM {0} {1}", table, whereRequest != null ? "WHERE " + whereRequest.ToString() : null);
        }

        public static string SanitizeString(string value)
        {
            if (value == null) { 
                return null;
            }
            return value.Replace("'", "").Replace("\"", "");
        }

        public static bool ProcessBoolean(object value)
        {
            if (value == null)
            {
                return false;
            }

            return (value.ToString() == "y") ? true : false;
        }

        public static string WrapBoolean(bool value)
        {
            return (value) ? "\'y\'" : "\'n\'";
        }

        public static string Wrapstring(string value)
        {
            return string.Format("\'{0}\'", value);
        }
    }
}