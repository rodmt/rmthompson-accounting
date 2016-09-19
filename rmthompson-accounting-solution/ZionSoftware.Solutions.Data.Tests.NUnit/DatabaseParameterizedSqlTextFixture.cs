/***************************************************************************
**
**      Auth:   Rodrigues M. Thompson, Chief Software Architect
**
**      Name:   Database Parameterized Sql Text Test Fixture
**
**      Date:   2013 June 05
**
**      Desc:   Test fixture for Database Parameterized Sql Text.
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
	public class DatabaseParameterizedSqlTextFixture
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
		public void ASqlTextCommandWithParametersShouldReturnDataSet()
		{
			// Arrange
			const String sqlText = "select * from [Northwind].[dbo].[Products] where [SupplierId] = @param1 and [ProductName] = @param2;";
			IDbCommand dbCommand = m_database.GetSqlTextCommand(sqlText);
			m_database.AddInParameter(dbCommand, "@param1", DbType.Int32, 1);
			m_database.AddInParameter(dbCommand, "@param2", DbType.String, "Chang");

			// Act
			var dataSet = m_database.ExecuteDataSet(dbCommand);

			// Assert
			Assert.That(1, Is.EqualTo(dataSet.Tables[0].Rows.Count));
		}

		[Test]
		//[ExpectedException(typeof(SqlException))]
		public void ASqlTextCommandWithNotEnoughParameterValuesShouldThrow()
		{
			// Arrange
			const String sqlText = "select * from [Northwind].[dbo].[Products] where [SupplierId] = @param1 and [ProductName] = @param2;";
			IDbCommand dbCommand = m_database.GetSqlTextCommand(sqlText);
			m_database.AddInParameter(dbCommand, "@param1", DbType.Int32, 1);

            // Act
            // Assert
            Assert.That(() => m_database.ExecuteDataSet(dbCommand), Throws.TypeOf<SqlException>());
		}

		[Test]
		//[ExpectedException(typeof(SqlException))]
		public void ASqlTextCommandWithTooManyParameterValuesShouldThrow()
		{
			// Arrange
			const String sqlText = "select * from [Northwind].[dbo].[Products] where [SupplierId] = @param1 and [ProductName] = @param2;";
			IDbCommand dbCommand = m_database.GetSqlTextCommand(sqlText);
			m_database.AddInParameter(dbCommand, "@param1", DbType.Int32, 1);
			m_database.AddInParameter(dbCommand, "@param2", DbType.String, "Chang");
			m_database.AddInParameter(dbCommand, "@param2", DbType.String, "Chang2");

            // Act
            // Assert
            Assert.That(() => m_database.ExecuteDataSet(dbCommand), Throws.TypeOf<SqlException>());
        }
	}
}
