using System;
using System.Collections.Generic;

namespace Cooper.ORM
{
    public class DbTools
    {
        private static string[] operators = new string[] { "=", ">=", "<=", ">", "<", "IS NULL", "IS NOT NULL" };

        public struct WhereRequest {
            public readonly string variable_name;
            public readonly RequestOperator request_operator;
            public readonly WhereRequest[] and_requests;

            public string variable_value;
            public WhereRequest(string variable_name, RequestOperator request_operator, object variable_value, WhereRequest[] and_requests = null) {
                this.variable_name = variable_name;
                this.request_operator = request_operator;
                if (request_operator != RequestOperator.NULL
                && request_operator != RequestOperator.NOTNULL) {
                    this.variable_value = " " + variable_value.ToString();
                }
                else 
                {
                    this.variable_value = "";
                }
                this.and_requests = and_requests;
            }
        }
        public static string GetOperatorString(RequestOperator request_operator) {
            return operators[(int)request_operator];
        }

        public static string GetVariableAttribute(string attribute) {
            int start_pos = attribute.IndexOf("(");
            if (start_pos != -1) {
                start_pos++;
                attribute = attribute.Substring(start_pos, attribute.IndexOf(")") - start_pos);
            }
            return attribute;
        }

        public enum RequestOperator {
            Equal = 0,
            MoreOrEqual = 1,
            LessOrEqual = 2,
            More = 3,
            Less = 4,
            NULL = 5,
            NOTNULL = 6
        }

        public static string createQuery(string table, HashSet<string> attributes, WhereRequest[] whereRequests = null)
        {
            string where = "";
            if (whereRequests != null)
            {
                where = " WHERE";
                foreach (WhereRequest request in whereRequests)
                {
                    where += String.Format(" {0} {1}{2} ", request.variable_name, GetOperatorString(request.request_operator), request.variable_value);
                    if (request.and_requests != null)
                    {
                        foreach (WhereRequest and_request in request.and_requests)
                        {
                            where += String.Format("AND {0} {1}{2} ", and_request.variable_name, GetOperatorString(and_request.request_operator), and_request.variable_value);
                        }
                    }
                    where += "OR";
                }
                //Remove last OR
                where = where.Remove(where.Length - 2);
            }
            return String.Format("SELECT {0} FROM {1}{2}",
                (attributes != null) ? string.Join(", ", attributes) : "*",
                table, where);
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
    }
}