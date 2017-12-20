using System;
using System.Data.Common;
using ZionSoftware.Solutions.Data.Tests.NUnit.Properties;

namespace ZionSoftware.Solutions.Data.Tests.NUnit.TestSupport
{
    public class TestDatabase : Database
    {
        public TestDatabase(string connectionString,
                            DbProviderFactory dbProviderFactory,
                            int commandTimeout = 60)
                            : base(dbProviderFactory, GetDbConnectionStringBuilder(connectionString), commandTimeout)
        {
        }

        private static DbConnectionStringBuilder GetDbConnectionStringBuilder(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString);
            return new DbConnectionStringBuilder() { ConnectionString = connectionString };
        }

        protected override void DeriveParameters(DbCommand discoveryCommand)
        {
            throw new NotImplementedException();
        }
    }
}
