using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            return string.Format("SELECT {0} FROM {1}{2}",
                (attributes != null) ? string.Join(", ", attributes) : "*",
                table, whereRequest?.ToString());
        }

        public static string CreateUpdateQuery(string table, EntityORM entity, WhereRequest whereRequest = null)
        {
            return string.Format("UPDATE {0} SET {1}{2}", table, string.Join(",", entity.attributeValue.Select(x => x.Key + "=" + x.Value).ToArray()), whereRequest?.ToString());
        }

        public static string CreateDeleteQuery(string table, WhereRequest whereRequest = null)
        {
            return string.Format("DELETE FROM {0} {1}", table, whereRequest?.ToString());
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

    public class WhereRequest
    {
        private StringBuilder whereRequest = new StringBuilder();

        private static readonly string[] operators = new string[] { "=", ">=", "<=", ">", "<", "IS NULL", "IS NOT NULL", "IN" };

        private static string GetOperatorstring(Operators requestOperator)
        {
            return operators[(int)requestOperator];
        }

        public WhereRequest(string attributeName, Operators op, params string[] attributeValues)
        {
            whereRequest.Append(attributeName);
            if (op == Operators.Null || op == Operators.NotNull)
            {
                whereRequest.Append($" {GetOperatorstring(op)}");
            }
            else if (attributeValues.Length > 0)
            {
                if (op == Operators.In)
                {
                    whereRequest.Append(" IN ");
                    whereRequest.Append("(");
                    whereRequest.Append(string.Join(", ", attributeValues));
                    whereRequest.Append(")");
                }
                else if (attributeValues.Length == 1)
                {
                    whereRequest.Append($" {GetOperatorstring(op)} ");
                    whereRequest.Append(attributeValues[0]);
                }
                else
                {
                    throw new ArgumentException("For all operators (except NULL and NOT NULL), the attributeValues array must be initialized with at least one value. " +
                        "For all operators (except NULL, NOT NULL, IN), the attributeValues array must be initialized with one value.");
                }
            }
            else
            {
                throw new ArgumentException("For all operators (except NULL and NOT NULL), the attributeValues array must be initialized with at least one value. " +
                    "For all operators (except NULL, NOT NULL, IN), the attributeValues array must be initialized with one value.");
            }
        }

        public WhereRequest(WhereRequest where)
        {
            whereRequest.Append("(");
            whereRequest.Append(where.ToString().Replace("WHERE ", ""));
            whereRequest.Append(")");
        }

        public WhereRequest And(string attributeName, Operators op, string attributeValue = null)
        {
            whereRequest.Append(" AND ");
            whereRequest.Append(attributeName);
            if (op == Operators.Null || op == Operators.NotNull)
            {
                whereRequest.Append($" {GetOperatorstring(op)}");
            }
            else
            {
                whereRequest.Append($" {GetOperatorstring(op)} ");
                whereRequest.Append(attributeValue);
            }
            return this;
        }

        public WhereRequest And(WhereRequest where)
        {
            whereRequest.Append(" \nAND ");
            whereRequest.Append("(");
            whereRequest.Append(where.ToString().Replace("WHERE ", ""));
            whereRequest.Append(")");
            return this;
        }

        public WhereRequest Or(string attributeName, Operators op, string attributeValue = null)
        {
            whereRequest.Append(" OR ");
            whereRequest.Append(attributeName);
            if (op == Operators.Null || op == Operators.NotNull)
            {
                whereRequest.Append($" {GetOperatorstring(op)}");
            }
            else
            {
                whereRequest.Append($" {GetOperatorstring(op)} ");
                whereRequest.Append(attributeValue);
            }
            return this;
        }

        public WhereRequest Or(WhereRequest where)
        {
            whereRequest.Append(" \nOR ");
            whereRequest.Append("(");
            whereRequest.Append(where.ToString().Replace("WHERE ", ""));
            whereRequest.Append(")");
            return this;
        }

        public WhereRequest AndIn(string attributeName, params string[] attributeValues)
        {
            whereRequest.Append(" AND ");
            whereRequest.Append(attributeName);
            whereRequest.Append(" IN ");
            whereRequest.Append("(");
            whereRequest.Append(string.Join(", ", attributeValues));
            whereRequest.Append(")");
            return this;
        }

        public WhereRequest OrIn(string attributeName, params string[] attributeValues)
        {
            whereRequest.Append(" OR ");
            whereRequest.Append(attributeName);
            whereRequest.Append(" IN ");
            whereRequest.Append("(");
            whereRequest.Append(string.Join(", ", attributeValues));
            whereRequest.Append(")");
            return this;
        }

        public override string ToString()
        {
            return " WHERE " + whereRequest.ToString();
        }
    }

    public enum Operators
    {
        Equal,
        MoreOrEqual,
        LessOrEqual,
        More,
        Less,
        Null,
        NotNull,
        In
    }
}