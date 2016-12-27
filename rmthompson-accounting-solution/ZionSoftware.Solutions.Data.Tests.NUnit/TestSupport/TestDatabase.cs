using System;
using System.Data.Common;
using ZionSoftware.Solutions.Data.Tests.NUnit.Properties;

namespace ZionSoftware.Solutions.Data.Tests.NUnit.TestSupport
{
    public class TestDatabase : Database
    {
        public TestDatabase(String connectionString,
                            DbProviderFactory dbProviderFactory,
                            Int32 commandTimeout = 60)
                            : base(dbProviderFactory, GetDbConnectionStringBuilder(connectionString), commandTimeout)
        {
        }

        private static DbConnectionStringBuilder GetDbConnectionStringBuilder(String connectionString)
        {
            if (String.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString);
            return new DbConnectionStringBuilder() { ConnectionString = connectionString };
        }

        protected override void DeriveParameters(DbCommand discoveryCommand)
        {
            throw new NotImplementedException();
        }
    }
}
