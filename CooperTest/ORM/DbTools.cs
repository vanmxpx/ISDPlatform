using NUnit.Framework;
using System.Collections.Generic;
using static Cooper.DbTools;

namespace DbToolsTest
{
    [TestFixture]
    public class Tests
    {
        public struct Query
        {
            public string table;
            public HashSet<string> attributes;
            public WhereRequest[] where_request;
            public string expected_result;
            public Query(string table, HashSet<string> attributes, WhereRequest[] where_request, string expected_result)
            {
                this.table = table;
                this.attributes = attributes;
                this.where_request = where_request;
                this.expected_result = expected_result;
            }
        }
        static IEnumerable<Query> TestRequests
        {
            get
            {
                yield return new Query("Test'''Table", null, null, "SELECT * FROM Test'''Table");
                WhereRequest[] whereReq = new WhereRequest[] { new WhereRequest("var_name", RequestOperator.Equal, "value") };
                yield return new Query("table", new HashSet<string> { "Name", "ID" }, whereReq, "SELECT Name, ID FROM table WHERE var_name = value");
                yield return new Query("SomeName", new HashSet<string>() { "MIN(name)" }, null, "SELECT MIN(name) FROM SomeName");
                whereReq = new WhereRequest[] {
                    new WhereRequest("name", RequestOperator.NOTNULL, null, new WhereRequest[] { new WhereRequest("test_name", RequestOperator.More, "value") }),
                    new WhereRequest("name1", RequestOperator.LessOrEqual, "3", new WhereRequest[] { new WhereRequest("test_name1", RequestOperator.NULL, null) })
                };
                yield return new Query("SomeName", new HashSet<string>() { "MIN(name)" }, whereReq, "SELECT MIN(name) FROM SomeName WHERE name IS NOT NULL AND test_name > value OR name1 <= 3 AND test_name1 IS NULL");
                whereReq = new WhereRequest[] { new WhereRequest("name", RequestOperator.Less, "test_value") };
                yield return new Query("Table@###", null, whereReq, "SELECT * FROM Table@### WHERE name < test_value");
            }
        }

        [Test]
        [TestCaseSource("TestRequests")]
        public void CreateQuery(Query request)
        {
            string sqlExpression = createQuery(request.table, request.attributes, request.where_request);
            Assert.IsTrue(request.expected_result.Trim().ToUpper().Equals(sqlExpression.Trim().ToUpper()));
        }
    }
}