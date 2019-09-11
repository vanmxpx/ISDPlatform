using Cooper.ORM;
using NUnit.Framework;
using System.Collections.Generic;
using static Cooper.ORM.DbTools;

namespace Cooper.Tests.ORM
{
    [TestFixture]
    public class DbToolsTest
    {
        public struct SelectQuery
        {
            public string table;
            public HashSet<string> attributes;
            public WhereRequest where_request;
            public string expected_result;
            public SelectQuery(string table, HashSet<string> attributes, WhereRequest where_request, string expected_result)
            {
                this.table = table;
                this.attributes = attributes;
                this.where_request = where_request;
                this.expected_result = expected_result;
            }

            public override string ToString()
            {
                return this.expected_result;
            }
        }

        public struct UpdateQuery
        {
            public string table;
            public WhereRequest where_request;
            public string expected_result;

            public UpdateQuery(string table, WhereRequest where_request, string expected_result)
            {
                this.table = table;
                this.where_request = where_request;
                this.expected_result = expected_result;
            }

            public override string ToString()
            {
                return this.expected_result;
            }
        }

        static IEnumerable<SelectQuery> TestSelectRequests
        {
            get
            {
                yield return new SelectQuery("Test'''Table", null, null, "SELECT * FROM Test'''Table");
                WhereRequest whereReq = new WhereRequest("var_name", Operators.Equal, "value");
                yield return new SelectQuery("table", new HashSet<string> { "Name", "ID" }, whereReq, "SELECT Name, ID FROM table WHERE var_name = value");
                yield return new SelectQuery("SomeName", new HashSet<string>() { "MIN(name)" }, null, "SELECT MIN(name) FROM SomeName");
                whereReq = new WhereRequest("name", Operators.NotNull)
                    .And("test_name", Operators.More, "value")
                    .Or("name1", Operators.LessOrEqual, "3")
                    .And("test_name1", Operators.Null);
                yield return new SelectQuery("SomeName", new HashSet<string>() { "MIN(name)" }, whereReq, "SELECT MIN(name) FROM SomeName WHERE name IS NOT NULL AND test_name > value OR name1 <= 3 AND test_name1 IS NULL");
                whereReq = new WhereRequest("name", Operators.Less, "test_value");
                yield return new SelectQuery("Table@###", null, whereReq, "SELECT * FROM Table@### WHERE name < test_value");
            }
        }

        static IEnumerable<UpdateQuery> TestUpdateRequests
        {
            //"UPDATE {0} SET {1} WHERE {2}"

            get
            {
                yield return new UpdateQuery("Test'''Table", null, "UPDATE Test'''Table SET test=test");
                WhereRequest whereReq = new WhereRequest("var_name", Operators.Equal, "value");
                yield return new UpdateQuery("table", whereReq, "UPDATE table SET test=test WHERE var_name = value");
                yield return new UpdateQuery("SomeName", null, "UPDATE SomeName SET test=test");
                whereReq = new WhereRequest("name", Operators.NotNull)
                    .And("test_name", Operators.More, "value")
                    .Or("name1", Operators.LessOrEqual, "3")
                    .And("test_name1", Operators.Null);
                yield return new UpdateQuery("SomeName", whereReq, "UPDATE SomeName SET test=test WHERE name IS NOT NULL AND test_name > value OR name1 <= 3 AND test_name1 IS NULL");
                whereReq = new WhereRequest("name", Operators.Less, "test_value");
                yield return new UpdateQuery("Table@###", whereReq, "UPDATE Table@### SET test=test WHERE name < test_value");
            }
        }

        [Test]
        [TestCaseSource("TestSelectRequests")]
        public void CreateSelectQuery(SelectQuery request)
        {
            string sqlExpression = Cooper.ORM.DbTools.CreateSelectQuery(request.table, request.attributes, request.where_request);
            string str1 = request.expected_result.Trim().ToUpper();
            string str2 = sqlExpression.Trim().ToUpper();
            Assert.IsTrue(str1.Equals(str2));
        }

        [Test]
        [TestCaseSource("TestUpdateRequests")]
        public void CreateUpdateQuery(UpdateQuery request)
        {
            EntityORM entity = new EntityORM();
            entity.attributeValue.Add(DbTools.GetVariableAttribute("test"), "test");

            string sqlExpression = Cooper.ORM.DbTools.CreateUpdateQuery(request.table, entity, request.where_request);
            string str1 = request.expected_result.Trim().ToUpper();
            string str2 = sqlExpression.Trim().ToUpper();
            Assert.IsTrue(str1.Equals(str2));
        }
    }
}