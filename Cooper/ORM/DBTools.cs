using System;
using System.Data;
using System.Data.Common;
using Cooper.Configuration;

namespace Cooper
{
    public class DbTools
    {
        private static string[] operators = new string[] { "=", ">=", "<=", ">", "<" };

        public struct WhereRequest {
            public string variable_name;
            public RequestOperator request_operator;

            public string variable_value;
            public WhereRequest(string variable_name, RequestOperator request_operator, object variable_value) {
                this.variable_name = variable_name;
                this.request_operator = request_operator;
                this.variable_value = variable_value.ToString();
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
            Less = 4
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