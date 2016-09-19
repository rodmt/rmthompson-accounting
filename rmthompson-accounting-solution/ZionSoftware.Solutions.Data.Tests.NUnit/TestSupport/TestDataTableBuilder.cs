/***************************************************************************
**
**      Auth:   Rodrigues M. Thompson, Chief Software Architect
**
**      Name:   Test Data Table Builder Class
**
**      Date:   2013 May 30
**
**      Desc:   Inherits the data table builder class for test support.
**
** Copyright © 2016 Zion Software Solutions, LLC. All Rights Reserved.
**
** Unpublished copyright. This material contains proprietary information
** that shall be used or copied only within Zion Software Solutions, 
** except with written permission of Zion Software Solutions.		
**
***************************************************************************/

using System;
using ZionSoftware.Solutions.Data;

namespace ZionSoftware.Data.Tests.NUnit.TestSupport
{
    internal class TestDataTableBuilder : DataTableBuilder
	{
		public TestDataTableBuilder()
		{
		}

		public TestDataTableBuilder(String tableName)
			: base(tableName)
		{
		}

		#region Overrides of DataTableBuilder

		/// <summary>
		/// Adds columns to the <see cref="DataTable"/> instance.
		/// </summary>
		protected override void BuildColumns()
		{
		}

		#endregion

		public void CreateBooleanNullColumn(String columnName)
		{
			AddBooleanNullColumn(columnName);
		}

		public void CreateBooleanNotNullColumn(String columnName)
		{
			AddBooleanNotNullColumn(columnName);
		}

		public void CreateByteNullColumn(String columnName)
		{
			AddByteNullColumn(columnName);
		}

		public void CreateByteNotNullColumn(String columnName)
		{
			AddByteNotNullColumn(columnName);
		}

		public void CreateDateTimeNullColumn(String columnName)
		{
			AddDateTimeNullColumn(columnName);
		}

		public void CreateDateTimeNotNullColumn(String columnName)
		{
			AddDateTimeNotNullColumn(columnName);
		}

		public void CreateDoubleNullColumn(String columnName)
		{
			AddDoubleNullColumn(columnName);
		}

		public void CreateDoubleNotNullColumn(String columnName)
		{
			AddDoubleNotNullColumn(columnName);
		}

		public void CreateGuidNullColumn(String columnName)
		{
			AddGuidNullColumn(columnName);
		}

		public void CreateGuidNotNullColumn(String columnName)
		{
			AddGuidNotNullColumn(columnName);
		}

		public void CreateInt16NullColumn(String columnName)
		{
			AddInt16NullColumn(columnName);
		}

		public void CreateInt16NotNullColumn(String columnName)
		{
			AddInt16NotNullColumn(columnName);
		}

		public void CreateInt32NullColumn(String columnName)
		{
			AddInt32NullColumn(columnName);
		}

		public void CreateInt32NotNullColumn(String columnName)
		{
			AddInt32NotNullColumn(columnName);
		}

		public void CreateInt64NullColumn(String columnName)
		{
			AddInt64NullColumn(columnName);
		}

		public void CreateInt64NotNullColumn(String columnName)
		{
			AddInt64NotNullColumn(columnName);
		}

		public void CreateSingleNullColumn(String columnName)
		{
			AddSingleNullColumn(columnName);
		}

		public void CreateSingleNullColumn(String columnName, Single defaultValue)
		{
			AddSingleNullColumn(columnName, defaultValue);
		}

		public void CreateSingleNotNullColumn(String columnName)
		{
			AddSingleNotNullColumn(columnName);
		}

		public void CreateSingleNotNullColumn(String columnName, Single defaultValue)
		{
			AddSingleNotNullColumn(columnName, defaultValue);
		}

		public void CreateStringNullColumn(String columnName)
		{
			AddStringNullColumn(columnName);
		}

		public void CreateStringNotNullColumn(String columnName)
		{
			AddStringNotNullColumn(columnName);
		}

		public void CreateStringNotNullColumn(String columnName, String defaultValue)
		{
			AddStringNotNullColumn(columnName, defaultValue);
		}

		public void CreateUInt16NullColumn(String columnName)
		{
			AddUInt16NullColumn(columnName);
		}

		public void CreateUInt16NotNullColumn(String columnName)
		{
			AddUInt16NotNullColumn(columnName);
		}

		public void CreateUInt32NullColumn(String columnName)
		{
			AddUInt32NullColumn(columnName);
		}

		public void CreateUInt32NotNullColumn(String columnName)
		{
			AddUInt32NotNullColumn(columnName);
		}

		public void CreateUInt64NullColumn(String columnName)
		{
			AddUInt64NullColumn(columnName);
		}

		public void CreateUInt64NotNullColumn(String columnName)
		{
			AddUInt64NotNullColumn(columnName);
		}
	}
}