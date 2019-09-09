using System;
using System.Text;

namespace Cooper.ORM
{
    public class WhereRequest
    {
        private StringBuilder whereRequest = new StringBuilder();

        private static readonly string[] operators = new string[] { "=", ">=", "<=", ">", "<", "IS NULL", "IS NOT NULL", "IN" };

        private static string GetOperatorString(Operators requestOperator)
        {
            return operators[(int)requestOperator];
        }

        public WhereRequest(string attributeName, Operators op, params string[] attributeValues)
        {
            whereRequest.Append(GenerateStringForOperator(attributeName, op, attributeValues));
        }

        public WhereRequest(WhereRequest where)
        {
            whereRequest.Append($"({where.ToString()})");
        }

        public WhereRequest And(string attributeName, Operators op, params string[] attributeValues)
        {
            whereRequest.Append($" AND {GenerateStringForOperator(attributeName, op, attributeValues)}");
            return this;
        }

        public WhereRequest And(WhereRequest where)
        {
            whereRequest.Append($" \nAND ({where.ToString()})");
            return this;
        }

        public WhereRequest Or(string attributeName, Operators op, params string[] attributeValues)
        {
            whereRequest.Append($" OR {GenerateStringForOperator(attributeName, op, attributeValues)}");
            return this;
        }

        public WhereRequest Or(WhereRequest where)
        {
            whereRequest.Append($" \nOR ({where.ToString()})");
            return this;
        }

        private string GenerateStringForOperator(string attributeName, Operators op, params string[] attributeValues)
        {
            var stringForOperator = new StringBuilder();
            stringForOperator.Append(attributeName);
            switch (op)
            {
                case Operators.Null:
                case Operators.NotNull:
                    stringForOperator.Append($" {GetOperatorString(op)}");
                    break;
                case Operators.In:
                    if (attributeValues.Length > 0)
                    {
                        stringForOperator.Append($" IN ({string.Join(", ", attributeValues)})");
                    }
                    else
                    {
                        throw new ArgumentException("For the operator IN, the attributeValues array must be initialized with at least one value.");
                    }
                    break;
                case Operators.Equal:
                case Operators.Less:
                case Operators.LessOrEqual:
                case Operators.More:
                case Operators.MoreOrEqual:
                    if (attributeValues.Length == 1)
                    {
                        stringForOperator.Append($" {GetOperatorString(op)} {attributeValues[0]}");
                    }
                    else
                    {
                        throw new ArgumentException("For the operators (Equal, Less, LessOrEqual, More, MoreOrEqual), the attributeValues array must be initialized with one value.");
                    }
                    break;
                default:
                    throw new ArgumentException("Passed operator does not exist.");
            }
            return stringForOperator.ToString();
        }

        public override string ToString()
        {
            return whereRequest.ToString();
        }
    }
}
