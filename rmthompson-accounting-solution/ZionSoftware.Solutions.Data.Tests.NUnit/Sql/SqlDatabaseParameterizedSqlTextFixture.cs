/***************************************************************************
**
**      Auth:   Rodrigues M. Thompson, Chief Software Architect
**
**      Name:   Sql Database Parameterized Sql Text Test Fixture
**
**      Date:   2013 June 05
**
**      Desc:   Test fixture for Sql Database Parameterized Sql Text.
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
using ZionSoftware.Solutions.Data.AdoNet.SqlServer;

namespace ZionSoftware.Solutions.Data.Tests.NUnit.Sql
{
    [TestFixture]
	public class SqlDatabaseParameterizedSqlTextFixture
	{
		private SqlDatabase m_sqlDatabase;

		[SetUp]
		public void Setup()
		{
			var connectionStringName = ConfigurationManager.AppSettings.Get("ConnectionStringName");
			var connectionData = ConfigurationManager.ConnectionStrings[connectionStringName];
			m_sqlDatabase = new SqlDatabase(connectionData.ConnectionString);
		}

        [Test]
	    public void CanDiscoverParameters()
	    {
            using (var connection = m_sqlDatabase.CreateConnection())
            {
                connection.Open();
                using (var command = m_sqlDatabase.GetStoredProcCommand("dbo.Employee Sales by Country"))
                {
                    m_sqlDatabase.DiscoverParameters(command);

                    Assert.That(command.Parameters.Count, Is.EqualTo(3));
                }
            }
	    }

		[Test]
		public void ASqlTextCommandWithParametersShouldReturnDataSet()
		{
			// Arrange
			const string sqlText = "select * from [Northwind].[dbo].[Products] where [SupplierId] = @param1 and [ProductName] = @param2;";
			IDbCommand dbCommand = m_sqlDatabase.GetSqlTextCommand(sqlText);
			m_sqlDatabase.AddInParameter(dbCommand, "@param1", SqlDbType.Int, 1);
			m_sqlDatabase.AddInParameter(dbCommand, "@param2", SqlDbType.NVarChar, "Chang");

			// Act
			var dataSet = m_sqlDatabase.ExecuteDataSet(dbCommand);

			// Assert
			Assert.That(dataSet.Tables[0].Rows.Count, Is.EqualTo(1));
		}

		[Test]
		//[ExpectedException(typeof(SqlException))]
		public void ASqlTextCommandWithNotEnoughParameterValuesShouldThrow()
		{
			// Arrange
			const string sqlText = "select * from [Northwind].[dbo].[Products] where [SupplierId] = @param1 and [ProductName] = @param2;";
			IDbCommand dbCommand = m_sqlDatabase.GetSqlTextCommand(sqlText);
			m_sqlDatabase.AddInParameter(dbCommand, "@param1", SqlDbType.Int, 1);

            // Act
            // Assert
            Assert.That(() => m_sqlDatabase.ExecuteDataSet(dbCommand), Throws.TypeOf<SqlException>());
        }

		[Test]
		//[ExpectedException(typeof(SqlException))]
		public void ASqlTextCommandWithTooManyParameterValuesShouldThrow()
		{
			// Arrange
			const string sqlText = "select * from [Northwind].[dbo].[Products] where [SupplierId] = @param1 and [ProductName] = @param2;";
			IDbCommand dbCommand = m_sqlDatabase.GetSqlTextCommand(sqlText);
			m_sqlDatabase.AddInParameter(dbCommand, "@param1", SqlDbType.Int, 1);
			m_sqlDatabase.AddInParameter(dbCommand, "@param2", SqlDbType.NVarChar, "Chang");
			m_sqlDatabase.AddInParameter(dbCommand, "@param2", SqlDbType.NVarChar, "Chang2");

            // Act
            // Assert
            Assert.That(() => m_sqlDatabase.ExecuteDataSet(dbCommand), Throws.TypeOf<SqlException>());
        }
	}
}
