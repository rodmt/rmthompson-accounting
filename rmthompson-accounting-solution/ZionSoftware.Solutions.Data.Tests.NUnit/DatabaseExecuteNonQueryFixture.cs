/***************************************************************************
**
**      Auth:   Rodrigues M. Thompson, Chief Software Architect
**
**      Name:   Database Execute NonQuery Test Fixture
**
**      Date:   2013 June 05
**
**      Desc:   Test fixture for database execute nonqueries.
**
** Copyright © 2016 Zion Software Solutions, LLC. All Rights Reserved.
**
** Unpublished copyright. This material contains proprietary information
** that shall be used or copied only within Zion Software Solutions, 
** except with written permission of Zion Software Solutions.	
**
***************************************************************************/

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using ZionSoftware.Data.Tests.NUnit.TestSupport;
using ZionSoftware.Solutions.Data;

namespace ZionSoftware.Data.Tests.NUnit
{
    [TestFixture]
    public class DatabaseExecuteNonQueryFixture
    {
        private Database m_database;

        [SetUp]
        public void Setup()
        {
            var connectionStringName = ConfigurationManager.AppSettings.Get("ConnectionStringName");
            var connectionData = ConfigurationManager.ConnectionStrings[connectionStringName];
            m_database = new TestDatabase(connectionData.ConnectionString, SqlClientFactory.Instance);
        }

        [Test]
        public void AnInsertShouldReturnNumberOfRowsAffected()
        {
            // Arrange
            const String sqlText = "insert [Northwind].[dbo].[Region]( [RegionId], [RegionDescription] ) values ( @param1, @param2 );";
            IDbCommand dbCommand = m_database.GetSqlTextCommand(sqlText);
            m_database.AddInParameter(dbCommand, "@param1", DbType.Int32, 7);
            m_database.AddInParameter(dbCommand, "@param2", DbType.String, "TestRegion");
            int rowsAffected;
            IDbConnection dbConnection = null;
            IDbTransaction dbTransaction = null;

            try
            {
                // Act
                dbConnection = m_database.CreateConnection();
                dbConnection.Open();
                dbTransaction = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
                rowsAffected = m_database.ExecuteNonQuery(dbCommand, dbTransaction);
            }
            finally
            {
	            dbTransaction?.Rollback();
	            dbTransaction?.Dispose();

	            dbConnection?.Close();
	            dbConnection?.Dispose();
            }

            // Assert
			Assert.That(rowsAffected,Is.EqualTo(1));
        }
    }
}
