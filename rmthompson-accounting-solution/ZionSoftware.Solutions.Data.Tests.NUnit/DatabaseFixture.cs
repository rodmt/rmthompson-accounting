﻿/***************************************************************************
**
**      Auth:   Rodrigues M. Thompson, Chief Software Architect
**
**      Name:   Database Test Fixture
**
**      Date:   2013 May 30
**
**      Desc:   Handles the tests for the database class.
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
using System.Data.Common;
using NUnit.Framework;
using ZionSoftware.Data.Tests.NUnit.TestSupport;

namespace ZionSoftware.Data.Tests.NUnit
{
    [TestFixture]
    public class DatabaseFixture
    {
        private DbProviderFactory m_dbProviderFactory;
        private String m_connectionString;

        [SetUp]
        public void Init()
        {
            m_dbProviderFactory = DbProviderFactories.GetFactory("System.Data.SqlClient");

            var connectionStringName = ConfigurationManager.AppSettings.Get("ConnectionStringName");
            var connectionData = ConfigurationManager.ConnectionStrings[connectionStringName];
            m_connectionString = connectionData.ConnectionString;
        }

        //[Test]
        //[ExpectedException(typeof(ArgumentException))]
        //public void ADatabaseConstuctWithNullConnectionStringShouldThrow()
        //{
        //    new TestDatabase(null, m_dbProviderFactory);
        //}

        //[Test]
        //[ExpectedException(typeof(ArgumentException))]
        //public void ADatabaseConstuctWithEmptyStringConnectionStringShouldThrow()
        //{
        //    new TestDatabase(String.Empty, m_dbProviderFactory);
        //}

        [Test]
        //[ExpectedException(typeof(ArgumentNullException))]
        public void ADatabaseConstuctWithNullDbProviderFactoryShouldThrow()
        {
            Assert.That(() => new TestDatabase(m_connectionString, null), Throws.ArgumentNullException);
        }

        [Test]
        //[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be less than zero.\r\nParameter name: commandTimeout")]
        public void ADatabaseConstuctWithLessThanZeroCommandTimoutShouldThrowWithSpecificMessage()
        {
            //var ex = Assert.Throws<ArgumentException>(
            //                () => new TestDatabase( "connectinString", m_dbProviderFactory, -1 ) );
            //Assert.AreEqual( "The value must not be less than zero.\r\nParameter name: commandTimeout", ex.Message );
            Assert.That(() => new TestDatabase(m_connectionString, m_dbProviderFactory, -1), Throws.ArgumentException);
        }
    }
}
