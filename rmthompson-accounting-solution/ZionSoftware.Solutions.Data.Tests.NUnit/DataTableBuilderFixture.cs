/***************************************************************************
**
**      Auth:   Rodrigues M. Thompson, Chief Software Architect
**
**      Name:   Data Table Builder Test Fixture
**
**      Date:   2013 May 30
**
**      Desc:   Handles the tests for the data table builder class.
**
** Copyright © 2016 Zion Software Solutions, LLC. All Rights Reserved.
**
** Unpublished copyright. This material contains proprietary information
** that shall be used or copied only within Zion Software Solutions, 
** except with written permission of Zion Software Solutions.		
**
***************************************************************************/

using System;
using NUnit.Framework;
using ZionSoftware.Solutions.Data.Tests.NUnit.TestSupport;

namespace ZionSoftware.Solutions.Data.Tests.NUnit
{
	[TestFixture]
	public class DataTableBuilderFixture
	{
		// Internal const(s)
	    private const String _DataTableName = "dataTableName";

		[Test]
		public void ADataTableBuilderConstructShouldCreateDataTable()
		{
			// Arrange - Create a new test data table builder instance.
			TestDataTableBuilder testDataTableBuilder = null;

			try
			{
				// Act - Creates the data table internally.
				testDataTableBuilder = new TestDataTableBuilder();

				// Assert
				Assert.That(new TestDataTableBuilder().InternalDataTable, Is.Not.Null);
			}
			finally
			{
				testDataTableBuilder?.InternalDataTable?.Dispose();
			}
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: tableName")]
		public void ADataTableBuilderConstuctWithNullTableNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder(null), Throws.ArgumentException);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: tableName")]
		public void ADataTableBuilderConstuctWithEmptyStringTableNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder(String.Empty), Throws.ArgumentException);
		}

		[Test]
		public void ADataTableBuilderConstructShouldNotBeNullAndCreateDataTableWithName()
		{
			// Arrange - Create a new test data table builder instance.
			TestDataTableBuilder testDataTableBuilder = null;

			try
			{
				// Act - Creates the data table internally.
				testDataTableBuilder = new TestDataTableBuilder(_DataTableName);

				// Assert
				Assert.That(testDataTableBuilder, Is.Not.Null);
				Assert.That(_DataTableName, Is.EqualTo(testDataTableBuilder.InternalDataTable.TableName));
			}
			finally
			{
				if (testDataTableBuilder != null && testDataTableBuilder.InternalDataTable != null)
					testDataTableBuilder.InternalDataTable.Dispose();
			}
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void AByteNullDataColumnWithNullNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateByteNullColumn(null), Throws.ArgumentException);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void AByteNullDataColumnWithEmptyStringNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateByteNullColumn(String.Empty), Throws.ArgumentException);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void AByteNotNullDataColumnWithNullNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateByteNullColumn(null), Throws.ArgumentException);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void AByteNotNullDataColumnWithEmptyStringNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateByteNullColumn(String.Empty), Throws.ArgumentException);
		}

		[Test]
		public void ADataTableBuilderConstructShouldCreatesDataTableWithNameAndAddByteNullColumn()
		{
			// Arrange - Create a new test data table builder instance.
			const String columnName = "byte_null_column";
			TestDataTableBuilder testDataTableBuilder = null;

			try
			{
				// Act - Creates the data table internally.
				testDataTableBuilder = new TestDataTableBuilder(_DataTableName);
				// Act - Add byte null column.
				testDataTableBuilder.CreateByteNullColumn(columnName);

				// Assert
				Assert.That(_DataTableName, Is.EqualTo(testDataTableBuilder.InternalDataTable.TableName));
				Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName], Is.Not.Null);
				Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].DataType, Is.EqualTo(typeof(Byte)));
				Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].AllowDBNull, Is.True);
			}
			finally
			{
				testDataTableBuilder?.InternalDataTable?.Dispose();
			}
		}

		[Test]
		public void ADataTableBuilderConstructShouldCreatesDataTableWithNameAndAddByteNotNullColumn()
		{
			// Arrange - Set the internal consts.
			const String columnName = "byte_not_null_column";
			// Arrange - Create a new test data table builder instance.
			var testDataTableBuilder = new TestDataTableBuilder(_DataTableName);

			// Act - Add byte not null column.
			testDataTableBuilder.CreateByteNotNullColumn(columnName);

			// Assert
			Assert.That(_DataTableName, Is.EqualTo(testDataTableBuilder.InternalDataTable.TableName));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName], Is.Not.Null);
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].DataType, Is.EqualTo(typeof(Byte)));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].AllowDBNull, Is.False);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void ABooleanNullDataColumnWithNullNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateBooleanNullColumn(null), Throws.ArgumentException);
        }

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void ABooleanNullDataColumnWithEmptyStringNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateBooleanNullColumn(String.Empty), Throws.ArgumentException);
        }

		[Test]
		public void ADataTableBuilderConstructShouldCreatesDataTableWithNameAndAddBooleanNullColumn()
		{
			// Arrange - Set the internal consts.
			const String columnName = "boolean_null_column";
			// Arrange - Create a new test data table builder instance.
			var testDataTableBuilder = new TestDataTableBuilder(_DataTableName);

			// Act - Add byte not null column.
			testDataTableBuilder.CreateBooleanNullColumn(columnName);

			// Assert
			Assert.That(_DataTableName, Is.EqualTo(testDataTableBuilder.InternalDataTable.TableName));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName], Is.Not.Null);
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].DataType, Is.EqualTo(typeof(Boolean)));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].AllowDBNull, Is.True);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void ABooleanNotNullDataColumnWithNullNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateBooleanNotNullColumn(null), Throws.ArgumentException);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void ABooleanNotNullDataColumnWithEmptyStringNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateBooleanNotNullColumn(String.Empty), Throws.ArgumentException);
		}

		[Test]
		public void ADataTableBuilderConstructShouldCreatesDataTableWithNameAndAddBooleanNotNullColumn()
		{
			// Arrange - Set the internal consts.
			const String columnName = "boolean_not_null_column";
			// Arrange - Create a new test data table builder instance.
			var testDataTableBuilder = new TestDataTableBuilder(_DataTableName);

			// Act - Add byte not null column.
			testDataTableBuilder.CreateBooleanNotNullColumn(columnName);

			// Assert
			Assert.That(_DataTableName, Is.EqualTo(testDataTableBuilder.InternalDataTable.TableName));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName], Is.Not.Null);
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].DataType, Is.EqualTo(typeof(Boolean)));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].AllowDBNull, Is.False);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void ADateTimeNotNullDataColumnWithNullNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateDateTimeNotNullColumn(null), Throws.ArgumentException);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void ADateTimeNotNullDataColumnWithEmptyStringNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateDateTimeNotNullColumn(String.Empty), Throws.ArgumentException);
		}

		[Test]
		public void ADataTableBuilderConstructShouldCreatesDataTableWithNameAndAddDateTimeNotNullColumn()
		{
			// Arrange - Set the internal consts.
			const String columnName = "date_time_not_null_column";
			// Arrange - Create a new test data table builder instance.
			var testDataTableBuilder = new TestDataTableBuilder(_DataTableName);

			// Act - Add byte not null column.
			testDataTableBuilder.CreateDateTimeNotNullColumn(columnName);

			// Assert
			Assert.That(_DataTableName, Is.EqualTo(testDataTableBuilder.InternalDataTable.TableName));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName], Is.Not.Null);
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].DataType, Is.EqualTo(typeof(DateTime)));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].AllowDBNull, Is.False);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void ADateTimeNullDataColumnWithNullNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateDateTimeNullColumn(null), Throws.ArgumentException);
        }

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void ADateTimeNullDataColumnWithEmptyStringNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateDateTimeNullColumn(String.Empty), Throws.ArgumentException);
        }

		[Test]
		public void ADataTableBuilderConstructShouldCreatesDataTableWithNameAndAddDateTimeNullColumn()
		{
			// Arrange - Set the internal consts.
			const String columnName = "date_time_null_column";
			// Arrange - Create a new test data table builder instance.
			var testDataTableBuilder = new TestDataTableBuilder(_DataTableName);

			// Act - Add byte not null column.
			testDataTableBuilder.CreateDateTimeNullColumn(columnName);

			// Assert
			Assert.That(_DataTableName, Is.EqualTo(testDataTableBuilder.InternalDataTable.TableName));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName], Is.Not.Null);
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].DataType, Is.EqualTo(typeof(DateTime)));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].AllowDBNull, Is.True);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void ADoubleNotNullDataColumnWithNullNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateDoubleNotNullColumn(null), Throws.ArgumentException);
        }

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void ADoubleNotNullDataColumnWithEmptyStringNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateDoubleNotNullColumn(String.Empty), Throws.ArgumentException);
        }

		[Test]
		public void ADataTableBuilderConstructShouldCreatesDataTableWithNameAndAddDoubleNotNullColumn()
		{
			// Arrange - Set the internal consts.
			const String columnName = "double_not_null_column";
			// Arrange - Create a new test data table builder instance.
			var testDataTableBuilder = new TestDataTableBuilder(_DataTableName);

			// Act - Add byte not null column.
			testDataTableBuilder.CreateDoubleNotNullColumn(columnName);

			// Assert
			Assert.That(_DataTableName, Is.EqualTo(testDataTableBuilder.InternalDataTable.TableName));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName], Is.Not.Null);
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].DataType, Is.EqualTo(typeof(Double)));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].AllowDBNull, Is.False);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void ADoubleNullDataColumnWithNullNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateDoubleNullColumn(null), Throws.ArgumentException);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void ADoubleNullDataColumnWithEmptyStringNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateDoubleNullColumn(String.Empty), Throws.ArgumentException);
		}

		[Test]
		public void ADataTableBuilderConstructShouldCreatesDataTableWithNameAndAddDoubleNullColumn()
		{
			// Arrange - Set the internal consts.
			const String columnName = "double_null_column";
			// Arrange - Create a new test data table builder instance.
			var testDataTableBuilder = new TestDataTableBuilder(_DataTableName);

			// Act - Add byte not null column.
			testDataTableBuilder.CreateDoubleNullColumn(columnName);

			// Assert
			Assert.That(_DataTableName, Is.EqualTo(testDataTableBuilder.InternalDataTable.TableName));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName], Is.Not.Null);
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].DataType, Is.EqualTo(typeof(Double)));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].AllowDBNull, Is.True);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void AGuidNotNullDataColumnWithNullNameShouldThrowWithSpecificMessage()
		{
			Assert.That(() => new TestDataTableBuilder().CreateGuidNotNullColumn(null), Throws.ArgumentException);
        }

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void AGuidNotNullDataColumnWithEmptyStringNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateGuidNotNullColumn(String.Empty), Throws.ArgumentException);
		}

		[Test]
		public void ADataTableBuilderConstructShouldCreatesDataTableWithNameAndAddGuidNotNullColumn()
		{
			// Arrange - Set the internal consts.
			const String columnName = "guid_not_null_column";
			// Arrange - Create a new test data table builder instance.
			var testDataTableBuilder = new TestDataTableBuilder(_DataTableName);

			// Act - Add byte not null column.
			testDataTableBuilder.CreateGuidNotNullColumn(columnName);

			// Assert
			Assert.That(_DataTableName, Is.EqualTo(testDataTableBuilder.InternalDataTable.TableName));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName], Is.Not.Null);
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].DataType, Is.EqualTo(typeof(Guid)));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].AllowDBNull, Is.False);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void AGuidNullDataColumnWithNullNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateGuidNullColumn(null), Throws.ArgumentException);
        }

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void AGuidNullDataColumnWithEmptyStringNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateGuidNullColumn(String.Empty), Throws.ArgumentException);
        }

		[Test]
		public void ADataTableBuilderConstructShouldCreatesDataTableWithNameAndAddGuidNullColumn()
		{
			// Arrange - Set the internal consts.
			const String columnName = "guid_null_column";
			// Arrange - Create a new test data table builder instance.
			var testDataTableBuilder = new TestDataTableBuilder(_DataTableName);

			// Act - Add byte not null column.
			testDataTableBuilder.CreateGuidNullColumn(columnName);

			// Assert
			Assert.That(_DataTableName, Is.EqualTo(testDataTableBuilder.InternalDataTable.TableName));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName], Is.Not.Null);
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].DataType, Is.EqualTo(typeof(Guid)));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].AllowDBNull, Is.True);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void AInt16NullDataColumnWithNullNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateInt16NullColumn(null), Throws.ArgumentException);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void AInt16NullDataColumnWithEmptyStringNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateInt16NullColumn(String.Empty), Throws.ArgumentException);
		}

		[Test]
		public void ADataTableBuilderConstructShouldCreatesDataTableWithNameAndAddInt16NullColumn()
		{
			// Arrange - Set the internal consts.
			const String columnName = "int16_null_column";
			// Arrange - Create a new test data table builder instance.
			var testDataTableBuilder = new TestDataTableBuilder(_DataTableName);

			// Act - Add byte not null column.
			testDataTableBuilder.CreateInt16NullColumn(columnName);

			// Assert
			Assert.That(_DataTableName, Is.EqualTo(testDataTableBuilder.InternalDataTable.TableName));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName], Is.Not.Null);
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].DataType, Is.EqualTo(typeof(Int16)));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].AllowDBNull, Is.True);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void AInt16NotNullDataColumnWithNullNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateInt16NotNullColumn(null), Throws.ArgumentException);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void AInt16NotNullDataColumnWithEmptyStringNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateInt16NotNullColumn(String.Empty), Throws.ArgumentException);
		}

		[Test]
		public void ADataTableBuilderConstructShouldCreatesDataTableWithNameAndAddInt16NotNullColumn()
		{
			// Arrange - Set the internal consts.
			const String columnName = "int16_not_null_column";
			// Arrange - Create a new test data table builder instance.
			var testDataTableBuilder = new TestDataTableBuilder(_DataTableName);

			// Act - Add byte not null column.
			testDataTableBuilder.CreateInt16NotNullColumn(columnName);

			// Assert
			Assert.That(_DataTableName, Is.EqualTo(testDataTableBuilder.InternalDataTable.TableName));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName], Is.Not.Null);
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].DataType, Is.EqualTo(typeof(Int16)));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].AllowDBNull, Is.False);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void AInt32NullDataColumnWithNullNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateInt32NullColumn(null), Throws.ArgumentException);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void AInt32NullDataColumnWithEmptyStringNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateInt32NullColumn(String.Empty), Throws.ArgumentException);
		}

		[Test]
		public void ADataTableBuilderConstructShouldCreatesDataTableWithNameAndAddInt32NullColumn()
		{
			// Arrange - Set the internal consts.
			const String columnName = "int32_null_column";
			// Arrange - Create a new test data table builder instance.
			var testDataTableBuilder = new TestDataTableBuilder(_DataTableName);

			// Act - Add byte not null column.
			testDataTableBuilder.CreateInt32NullColumn(columnName);

			// Assert
			Assert.That(_DataTableName, Is.EqualTo(testDataTableBuilder.InternalDataTable.TableName));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName], Is.Not.Null);
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].DataType, Is.EqualTo(typeof(Int32)));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].AllowDBNull, Is.True);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void AInt32NotNullDataColumnWithNullNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateInt32NotNullColumn(null), Throws.ArgumentException);
        }

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void AInt32NotNullDataColumnWithEmptyStringNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateInt32NotNullColumn(String.Empty), Throws.ArgumentException);
        }

		[Test]
		public void ADataTableBuilderConstructShouldCreatesDataTableWithNameAndAddInt32NotNullColumn()
		{
			// Arrange - Set the internal consts.
			const String columnName = "int32_not_null_column";
			// Arrange - Create a new test data table builder instance.
			var testDataTableBuilder = new TestDataTableBuilder(_DataTableName);

			// Act - Add byte not null column.
			testDataTableBuilder.CreateInt32NotNullColumn(columnName);

			// Assert
			Assert.That(_DataTableName, Is.EqualTo(testDataTableBuilder.InternalDataTable.TableName));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName], Is.Not.Null);
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].DataType, Is.EqualTo(typeof(Int32)));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].AllowDBNull, Is.False);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void AInt64NullDataColumnWithNullNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateInt64NullColumn(null), Throws.ArgumentException);
        }

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void AInt64NullDataColumnWithEmptyStringNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateInt64NullColumn(String.Empty), Throws.ArgumentException);
        }

		[Test]
		public void ADataTableBuilderConstructShouldCreatesDataTableWithNameAndAddInt64NullColumn()
		{
			// Arrange - Set the internal consts.
			const String columnName = "int64_null_column";
			// Arrange - Create a new test data table builder instance.
			var testDataTableBuilder = new TestDataTableBuilder(_DataTableName);

			// Act - Add byte not null column.
			testDataTableBuilder.CreateInt64NullColumn(columnName);

			// Assert
			Assert.That(_DataTableName, Is.EqualTo(testDataTableBuilder.InternalDataTable.TableName));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName], Is.Not.Null);
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].DataType, Is.EqualTo(typeof(Int64)));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].AllowDBNull, Is.True);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void AInt64NotNullDataColumnWithNullNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateInt64NotNullColumn(null), Throws.ArgumentException);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void AInt64NotNullDataColumnWithEmptyStringNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateInt64NotNullColumn(String.Empty), Throws.ArgumentException);
		}

		[Test]
		public void ADataTableBuilderConstructShouldCreatesDataTableWithNameAndAddInt64NotNullColumn()
		{
			// Arrange - Set the internal consts.
			const String columnName = "int64_not_null_column";
			// Arrange - Create a new test data table builder instance.
			var testDataTableBuilder = new TestDataTableBuilder(_DataTableName);

			// Act - Add byte not null column.
			testDataTableBuilder.CreateInt64NotNullColumn(columnName);

			// Assert
			Assert.That(_DataTableName, Is.EqualTo(testDataTableBuilder.InternalDataTable.TableName));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName], Is.Not.Null);
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].DataType, Is.EqualTo(typeof(Int64)));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].AllowDBNull, Is.False);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void ASingleNotNullDataColumnWithNullNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateSingleNotNullColumn(null), Throws.ArgumentException);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void ASingleNotNullDataColumnWithEmptyStringNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateSingleNotNullColumn(String.Empty), Throws.ArgumentException);
		}

		[Test]
		public void ADataTableBuilderConstructShouldCreatesDataTableWithNameAndAddSingleNotNullColumn()
		{
			// Arrange - Set the internal consts.
			const String columnName = "single_not_null_column";
			// Arrange - Create a new test data table builder instance.
			var testDataTableBuilder = new TestDataTableBuilder(_DataTableName);

			// Act - Add byte not null column.
			testDataTableBuilder.CreateSingleNotNullColumn(columnName);

			// Assert
			Assert.That(_DataTableName, Is.EqualTo(testDataTableBuilder.InternalDataTable.TableName));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName], Is.Not.Null);
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].DataType, Is.EqualTo(typeof(Single)));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].AllowDBNull, Is.False);
		}

		[Test]
		public void ADataTableBuilderConstructShouldCreateDataTableWithNameAndAddSingleNotNullColumnAndDefaultValue()
		{
			// Arrange - Set the internal consts.
			const String columnName = "single_not_null_column";
			// Arrange - Create a new test data table builder instance.
			var testDataTableBuilder = new TestDataTableBuilder(_DataTableName);

			// Act - Add byte not null column.
			testDataTableBuilder.CreateSingleNotNullColumn(columnName, Single.Epsilon);

			// Assert
			Assert.That(_DataTableName, Is.EqualTo(testDataTableBuilder.InternalDataTable.TableName));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName], Is.Not.Null);
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].DataType, Is.EqualTo(typeof(Single)));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].AllowDBNull, Is.False);
			Assert.That(Single.Epsilon, Is.EqualTo(Convert.ToSingle(testDataTableBuilder.InternalDataTable.Columns[columnName].DefaultValue)));
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void ASingleNullDataColumnWithNullNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateSingleNullColumn(null), Throws.ArgumentException);
        }

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void ASingleNullDataColumnWithEmptyStringNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateSingleNullColumn(String.Empty), Throws.ArgumentException);
        }

		[Test]
		//[ExpectedException(typeof(ArgumentNullException))]
		public void AStringNullDataColumnWithNullDefaultValueShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateStringNotNullColumn("string_null", null), Throws.ArgumentNullException);
        }

		[Test]
		public void ADataTableBuilderConstructShouldCreateDataTableWithNameAndSingleNullColumn()
		{
			// Arrange - Set the internal consts.
			const String columnName = "single_null_column";
			// Arrange - Create a new test data table builder instance.
			var testDataTableBuilder = new TestDataTableBuilder(_DataTableName);

			// Act - Add byte not null column.
			testDataTableBuilder.CreateSingleNullColumn(columnName);

			// Assert
			Assert.That(_DataTableName, Is.EqualTo(testDataTableBuilder.InternalDataTable.TableName));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName], Is.Not.Null);
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].DataType, Is.EqualTo(typeof(Single)));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].AllowDBNull, Is.True);
		}

		[Test]
		public void ADataTableBuilderConstructShouldCreateDataTableWithNameAndSingleNullColumnAndDefaultValue()
		{
			// Arrange - Set the internal consts.
			const String columnName = "single_null_column";
			// Arrange - Create a new test data table builder instance.
			var testDataTableBuilder = new TestDataTableBuilder(_DataTableName);

			// Act - Add byte not null column.
			testDataTableBuilder.CreateSingleNullColumn(columnName, Single.Epsilon);

			// Assert
			Assert.That(_DataTableName, Is.EqualTo(testDataTableBuilder.InternalDataTable.TableName));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName], Is.Not.Null);
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].DataType, Is.EqualTo(typeof(Single)));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].AllowDBNull, Is.True);
			Assert.That(Single.Epsilon, Is.EqualTo(Convert.ToSingle(testDataTableBuilder.InternalDataTable.Columns[columnName].DefaultValue)));
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void AStringNotNullDataColumnWithNullNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateStringNotNullColumn(null), Throws.ArgumentException);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void AStringNotNullDataColumnWithEmptyStringNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateStringNotNullColumn(String.Empty), Throws.ArgumentException);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentNullException))]
		public void AStringNotNullDataColumnWithNullDefaultValueShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateStringNotNullColumn("string_not_null", null), Throws.ArgumentNullException);
		}

		[Test]
		public void ADataTableBuilderConstructShouldCreatesDataTableWithNameAndAddStringNotNullColumn()
		{
			// Arrange - Set the internal consts.
			const String columnName = "string_not_null_column";
			// Arrange - Create a new test data table builder instance.
			var testDataTableBuilder = new TestDataTableBuilder(_DataTableName);

			// Act - Add byte not null column.
			testDataTableBuilder.CreateStringNotNullColumn(columnName);

			// Assert
			Assert.That(_DataTableName, Is.EqualTo(testDataTableBuilder.InternalDataTable.TableName));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName], Is.Not.Null);
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].DataType, Is.EqualTo(typeof(String)));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].AllowDBNull, Is.False);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void AStringNullDataColumnWithNullNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateStringNullColumn(null), Throws.ArgumentException);
        }

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		public void AStringNullDataColumnWithEmptyStringNameShouldThrowWithSpecificMessage()
		{
            Assert.That(() => new TestDataTableBuilder().CreateStringNullColumn(String.Empty), Throws.ArgumentException);
        }

		[Test]
		public void ADataTableBuilderConstructShouldCreatesDataTableWithNameAndAddStringNullColumn()
		{
			// Arrange - Set the internal consts.
			const String columnName = "string_null_column";
			// Arrange - Create a new test data table builder instance.
			var testDataTableBuilder = new TestDataTableBuilder(_DataTableName);

			// Act - Add byte not null column.
			testDataTableBuilder.CreateStringNullColumn(columnName);

			// Assert
			Assert.That(_DataTableName, Is.EqualTo(testDataTableBuilder.InternalDataTable.TableName));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName], Is.Not.Null);
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].DataType, Is.EqualTo(typeof(String)));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].AllowDBNull, Is.True);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		// ReSharper disable InconsistentNaming
		public void AUInt16NullDataColumnWithNullNameShouldThrowWithSpecificMessage()
		// ReSharper restore InconsistentNaming
		{
            Assert.That(() => new TestDataTableBuilder().CreateUInt16NullColumn(null), Throws.ArgumentException);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		// ReSharper disable InconsistentNaming
		public void AUInt16NullDataColumnWithEmptyStringNameShouldThrowWithSpecificMessage()
		// ReSharper restore InconsistentNaming
		{
            Assert.That(() => new TestDataTableBuilder().CreateUInt16NullColumn(String.Empty), Throws.ArgumentException);
		}

		[Test]
		public void ADataTableBuilderConstructShouldCreatesDataTableWithNameAndAddUInt16NullColumn()
		{
			// Arrange - Set the UInternal consts.
			const String columnName = "uint16_null_column";
			// Arrange - Create a new test data table builder instance.
			var testDataTableBuilder = new TestDataTableBuilder(_DataTableName);

			// Act - Add byte not null column.
			testDataTableBuilder.CreateUInt16NullColumn(columnName);

			// Assert
			Assert.That(_DataTableName, Is.EqualTo(testDataTableBuilder.InternalDataTable.TableName));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName], Is.Not.Null);
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].DataType, Is.EqualTo(typeof(UInt16)));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].AllowDBNull, Is.True);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		// ReSharper disable InconsistentNaming
		public void AUInt16NotNullDataColumnWithNullNameShouldThrowWithSpecificMessage()
		// ReSharper restore InconsistentNaming
		{
            Assert.That(() => new TestDataTableBuilder().CreateUInt16NotNullColumn(null), Throws.ArgumentException);
        }

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		// ReSharper disable InconsistentNaming
		public void AUInt16NotNullDataColumnWithEmptyStringNameShouldThrowWithSpecificMessage()
		// ReSharper restore InconsistentNaming
		{
            Assert.That(() => new TestDataTableBuilder().CreateUInt16NotNullColumn(String.Empty), Throws.ArgumentException);
        }

		[Test]
		public void ADataTableBuilderConstructShouldCreatesDataTableWithNameAndAddUInt16NotNullColumn()
		{
			// Arrange - Set the UInternal consts.
			const String columnName = "uint16_not_null_column";
			// Arrange - Create a new test data table builder instance.
			var testDataTableBuilder = new TestDataTableBuilder(_DataTableName);

			// Act - Add byte not null column.
			testDataTableBuilder.CreateUInt16NotNullColumn(columnName);

			// Assert
			Assert.That(_DataTableName, Is.EqualTo(testDataTableBuilder.InternalDataTable.TableName));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName], Is.Not.Null);
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].DataType, Is.EqualTo(typeof(UInt16)));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].AllowDBNull, Is.False);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		// ReSharper disable InconsistentNaming
		public void AUInt32NullDataColumnWithNullNameShouldThrowWithSpecificMessage()
		// ReSharper restore InconsistentNaming
		{
            Assert.That(() => new TestDataTableBuilder().CreateUInt32NullColumn(null), Throws.ArgumentException);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		// ReSharper disable InconsistentNaming
		public void AUInt32NullDataColumnWithEmptyStringNameShouldThrowWithSpecificMessage()
		// ReSharper restore InconsistentNaming
		{
            Assert.That(() => new TestDataTableBuilder().CreateUInt32NullColumn(String.Empty), Throws.ArgumentException);
		}

		[Test]
		public void ADataTableBuilderConstructShouldCreatesDataTableWithNameAndAddUInt32NullColumn()
		{
			// Arrange - Set the UInternal consts.
			const String columnName = "uint32_null_column";
			// Arrange - Create a new test data table builder instance.
			var testDataTableBuilder = new TestDataTableBuilder(_DataTableName);

			// Act - Add byte not null column.
			testDataTableBuilder.CreateUInt32NullColumn(columnName);

			// Assert
			Assert.That(_DataTableName, Is.EqualTo(testDataTableBuilder.InternalDataTable.TableName));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName], Is.Not.Null);
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].DataType, Is.EqualTo(typeof(UInt32)));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].AllowDBNull, Is.True);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		// ReSharper disable InconsistentNaming
		public void AUInt32NotNullDataColumnWithNullNameShouldThrowWithSpecificMessage()
		// ReSharper restore InconsistentNaming
		{
            Assert.That(() => new TestDataTableBuilder().CreateUInt32NotNullColumn(null), Throws.ArgumentException);
        }

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		// ReSharper disable InconsistentNaming
		public void AUInt32NotNullDataColumnWithEmptyStringNameShouldThrowWithSpecificMessage()
		// ReSharper restore InconsistentNaming
		{
            Assert.That(() => new TestDataTableBuilder().CreateUInt32NotNullColumn(String.Empty), Throws.ArgumentException);
        }

		[Test]
		public void ADataTableBuilderConstructShouldCreatesDataTableWithNameAndAddUInt32NotNullColumn()
		{
			// Arrange - Set the UInternal consts.
			const String columnName = "uint32_not_null_column";
			// Arrange - Create a new test data table builder instance.
			var testDataTableBuilder = new TestDataTableBuilder(_DataTableName);

			// Act - Add byte not null column.
			testDataTableBuilder.CreateUInt32NotNullColumn(columnName);

			// Assert
			Assert.That(_DataTableName, Is.EqualTo(testDataTableBuilder.InternalDataTable.TableName));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName], Is.Not.Null);
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].DataType, Is.EqualTo(typeof(UInt32)));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].AllowDBNull, Is.False);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		// ReSharper disable InconsistentNaming
		public void AUInt64NullDataColumnWithNullNameShouldThrowWithSpecificMessage()
		// ReSharper restore InconsistentNaming
		{
            Assert.That(() => new TestDataTableBuilder().CreateUInt64NullColumn(null), Throws.ArgumentException);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		// ReSharper disable InconsistentNaming
		public void AUInt64NullDataColumnWithEmptyStringNameShouldThrowWithSpecificMessage()
		// ReSharper restore InconsistentNaming
		{
            Assert.That(() => new TestDataTableBuilder().CreateUInt64NullColumn(String.Empty), Throws.ArgumentException);
		}

		[Test]
		public void ADataTableBuilderConstructShouldCreatesDataTableWithNameAndAddUInt64NullColumn()
		{
			// Arrange - Set the UInternal consts.
			const String columnName = "uint64_null_column";
			// Arrange - Create a new test data table builder instance.
			var testDataTableBuilder = new TestDataTableBuilder(_DataTableName);

			// Act - Add byte not null column.
			testDataTableBuilder.CreateUInt64NullColumn(columnName);

			// Assert
			Assert.That(_DataTableName, Is.EqualTo(testDataTableBuilder.InternalDataTable.TableName));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName], Is.Not.Null);
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].DataType, Is.EqualTo(typeof(UInt64)));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].AllowDBNull, Is.True);
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		// ReSharper disable InconsistentNaming
		public void AUInt64NotNullDataColumnWithNullNameShouldThrowWithSpecificMessage()
		// ReSharper restore InconsistentNaming
		{
            Assert.That(() => new TestDataTableBuilder().CreateUInt64NotNullColumn(null), Throws.ArgumentException);
        }

		[Test]
		//[ExpectedException(typeof(ArgumentException), ExpectedMessage = "The value must not be null or an empty string.\r\nParameter name: columnName")]
		// ReSharper disable InconsistentNaming
		public void AUInt64NotNullDataColumnWithEmptyStringNameShouldThrowWithSpecificMessage()
		// ReSharper restore InconsistentNaming
		{
            Assert.That(() => new TestDataTableBuilder().CreateUInt64NotNullColumn(String.Empty), Throws.ArgumentException);
        }

		[Test]
		public void ADataTableBuilderConstructShouldCreatesDataTableWithNameAndAddUInt64NotNullColumn()
		{
			// Arrange - Set the UInternal consts.
			const String columnName = "uint64_not_null_column";
			// Arrange - Create a new test data table builder instance.
			var testDataTableBuilder = new TestDataTableBuilder(_DataTableName);

			// Act - Add byte not null column.
			testDataTableBuilder.CreateUInt64NotNullColumn(columnName);

			// Assert
			Assert.That(_DataTableName, Is.EqualTo(testDataTableBuilder.InternalDataTable.TableName));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName], Is.Not.Null);
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].DataType, Is.EqualTo(typeof(UInt64)));
			Assert.That(testDataTableBuilder.InternalDataTable.Columns[columnName].AllowDBNull, Is.False);
		}
	}
}
