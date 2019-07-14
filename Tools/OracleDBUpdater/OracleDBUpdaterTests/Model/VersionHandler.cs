using NUnit.Framework;
using OracleDBUpdater;
using System.Collections.Generic;

namespace Tests
{
    public class VersionHandlerTest
    {
        public struct TestVersion
        {
            public string version;
            public bool can_parse;
            public VersionHandler.Version version_out;
            public TestVersion(string version, bool can_parse, VersionHandler.Version version_out)
            {
                this.version = version;
                this.can_parse = can_parse;
                this.version_out = version_out;
            }
        }

        static IEnumerable<TestVersion> TestVersionsRequests
        {
            get
            {
                yield return new TestVersion("0.1.1", true, new VersionHandler.Version(0, 1, 1));
                yield return new TestVersion("0.1.t", false, new VersionHandler.Version(0, 0, 0));
                yield return new TestVersion("0.t.1", false, new VersionHandler.Version(0, 0, 0));
                yield return new TestVersion("t.1.1", false, new VersionHandler.Version(0, 0, 0));
                yield return new TestVersion("0.1", true, new VersionHandler.Version(0, 1, 0));
                yield return new TestVersion("0.0.1", true, new VersionHandler.Version(0, 0, 1));
                yield return new TestVersion("0.0.0", true, new VersionHandler.Version(0, 0, 0));
                yield return new TestVersion("1.0.0", true, new VersionHandler.Version(1, 0, 0));
                yield return new TestVersion("1.1.1", true, new VersionHandler.Version(1, 1, 1));
                yield return new TestVersion("2.1.1", true, new VersionHandler.Version(2, 1, 1));
                yield return new TestVersion("2.-1.1", false, new VersionHandler.Version(0, 0, 0));
                yield return new TestVersion("0.0.-1", false, new VersionHandler.Version(0, 0, 0));
                yield return new TestVersion("-1.2.0", false, new VersionHandler.Version(0, 0, 0));
                yield return new TestVersion("0.1.12312123", true, new VersionHandler.Version(0, 1, 12312123));
                yield return new TestVersion("11232132.11232132.12312123", true, new VersionHandler.Version(11232132, 11232132, 12312123));
                yield return new TestVersion("", false, new VersionHandler.Version(0, 0, 0));
            }
        }

        [Test]
        [TestCaseSource("TestVersionsRequests")]
        public void TryParseVersionTest(TestVersion testVersion)
        {
            if (VersionHandler.TryParseVersion(testVersion.version, out VersionHandler.Version test_version))
            {
                Assert.IsTrue(testVersion.version_out.Equals(test_version) && testVersion.can_parse);
            }
            else
            {
                Assert.IsTrue(!testVersion.can_parse);
            }
        }
    }
}