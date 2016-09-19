/******************************************************************************
**
**		Auth:	Rodrigues M. Thompson, Chief Software Architect
**		
**		Name:	Data Table Builder Class
**
**		Date:	2013 May 15
**
**		Desc:	Builds data table instances.
**
** Copyright © 2016 Zion Software Solutions, LLC. All Rights Reserved.
**
** Unpublished copyright. This material contains proprietary information
** that shall be used or copied only within Zion Software Solutions, 
** except with written permission of Zion Software Solutions.
**              
*******************************************************************************/

using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using ZionSoftware.Solutions.Data.Properties;

namespace ZionSoftware.Solutions.Data
{
    /// <summary>
    /// Builds data table instances.
    /// </summary>
    public abstract class DataTableBuilder : IDisposable
    {
        /// <summary>
        /// Gets a <see cref="DataTable"/>instance.
        /// </summary>
        public DataTable InternalDataTable { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataTableBuilder"/> class.
        /// </summary>
        protected DataTableBuilder()
        {
            // Create the data table.
            InternalDataTable = new DataTable();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataTableBuilder"/> class with the specified table name.
        /// </summary>
        /// <param name="tableName">The name to give the table.</param>
        protected DataTableBuilder(String tableName)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(tableName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(tableName));

            // Create the data table.
            // Note: If tableName is null or an empty string, a default name 
            // Note: is given when added to the DataTableCollection.
            InternalDataTable = new DataTable(tableName);
        }

        /// <summary>
        /// Adds columns to the <see cref="DataTable"/> instance.
        /// </summary>
        protected abstract void BuildColumns();

        #region DataTable Helpers

        /// <summary>
        /// Sets an array of columns that function as primary keys for the data table
        /// </summary>
        /// <param name="dataColumn">Data column to add as a primary key.</param>
        protected void AddPrimaryKeys(DataColumn dataColumn)
        {
            // Validate the argument(s)
            if (dataColumn == null) throw new ArgumentNullException(nameof(dataColumn));

            // Confirm the columns exist in the table.
            if (!CheckIfColumnExist(dataColumn.ColumnName))
                throw new InvalidOperationException(Resources.ExceptionDataColumnsDoNotExist);

            // Add the primary key.
            AddPrimaryKeys(new[] { dataColumn });
        }

        /// <summary>
        /// Sets an array of columns that function as primary keys for the data table
        /// </summary>
        /// <param name="dataColumns">An array of primary key data columns.</param>
        protected void AddPrimaryKeys(DataColumn[] dataColumns)
        {
            // Validate the argument(s)
            if (dataColumns == null) throw new ArgumentNullException(nameof(dataColumns));

            // Confirm the columns exist in the table.
            if (dataColumns.Any(dataColumn => !CheckIfColumnExist(dataColumn.ColumnName)))
                throw new InvalidOperationException(Resources.ExceptionDataColumnsDoNotExist);

            // Add the primary keys.
            InternalDataTable.PrimaryKey = dataColumns;
        }

        /// <summary>
        /// Adds a unique constraint to the data table instance.
        /// </summary>
        /// <param name="dataColumn">The DataColumn to constrain.</param>
        protected void AddUniqueConstraint(DataColumn dataColumn)
        {
            // Validate the argument(s)
            if (dataColumn == null) throw new ArgumentNullException(nameof(dataColumn));

            // Confirm the columns exist in the table.
            if (!CheckIfColumnExist(dataColumn.ColumnName))
                throw new InvalidOperationException(Resources.ExceptionDataColumnsDoNotExist);

            // Add the constraint
            AddUniqueConstraint(new[] { dataColumn });
        }

        /// <summary>
        /// Adds a unique constraint to the data table instance.
        /// </summary>
        /// <param name="dataColumns">A collection of <see cref="DataColumn"/>s.</param>
        protected void AddUniqueConstraint(DataColumn[] dataColumns)
        {
            // Validate the argument(s)
            if (dataColumns == null) throw new ArgumentNullException(nameof(dataColumns));

            // Confirm the columns exist in the table.
            if (dataColumns.Any(dataColumn => !CheckIfColumnExist(dataColumn.ColumnName)))
                throw new InvalidOperationException(Resources.ExceptionDataColumnsDoNotExist);

            // New unique constraint.
            var uniqueConstraint = new UniqueConstraint(dataColumns);

            // Assert
            Debug.Assert(uniqueConstraint != null, "uniqueConstraint != null");

            // Add the contraint.
            AddConstraint(uniqueConstraint);
        }

        /// <summary>
        /// Adds a unique constraint to the data table instance.
        /// </summary>
        /// <param name="constraintName">Name of the constraint.</param>
        /// <param name="dataColumn">The DataColumn to constrain.</param>
        protected void AddUniqueConstraint(String constraintName,
                                           DataColumn dataColumn)
        {
            // Add the constraint.
            AddUniqueConstraint(constraintName, new[] { dataColumn });
        }

        /// <summary>
        /// Adds a unique constraint to the data table instance.
        /// </summary>
        /// <param name="constraintName">Name of the constraint.</param>
        /// <param name="dataColumns">The array of DataColumn objects to constrain.</param>
        protected void AddUniqueConstraint(String constraintName,
                                           DataColumn[] dataColumns)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(constraintName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(constraintName));
            if (dataColumns == null)
                throw new ArgumentNullException(nameof(dataColumns));

            //Confirm the columns exist in the table.
            if (dataColumns.Any(dataColumn => !CheckIfColumnExist(dataColumn.ColumnName)))
                throw new InvalidOperationException(Resources.ExceptionDataColumnsDoNotExist);

            // Create the unique constraint.
            var uniqueConstraint = new UniqueConstraint(constraintName, dataColumns);

            // Assert
            Debug.Assert(uniqueConstraint != null, "uniqueConstraint != null");

            // Add the contraint.
            AddConstraint(uniqueConstraint);
        }

        /// <summary>
        /// Adds a foreign key constraint.
        /// </summary>
        /// <param name="constraintName">Name of the constraint.</param>
        /// <param name="parentColumn">The parent DataColumn in the constraint.</param>
        /// <param name="childColumn">The child DataColumn in the constraint.</param>
        protected void AddForeignKeyContraint(String constraintName,
                                              DataColumn parentColumn,
                                              DataColumn childColumn)
        {
            // Create the foreign key contraint with casade delete rule.
            var foreignKeyConstraint = new ForeignKeyConstraint(constraintName, parentColumn, childColumn) { DeleteRule = Rule.Cascade };

            // Assert
            Debug.Assert(foreignKeyConstraint != null, "foreignKeyConstraint != null");

            // Add the contraint.
            AddConstraint(foreignKeyConstraint);
        }

        /// <summary>
        /// Adds a <see cref="Constraint"/> to a <see cref="DataTable"/> instance.
        /// </summary>
        /// <param name="constraint">A <see cref="Constraint"/>.</param>
        private void AddConstraint(Constraint constraint)
        {
            if (constraint == null)
                throw new ArgumentNullException(nameof(constraint));

            GuardDataTableExist();
            InternalDataTable.Constraints.Add(constraint);
        }

        /// <summary>
        /// Adds a boolean column that allows null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        protected void AddBooleanNullColumn(String columnName)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Add column.
            AddBooleanColumn(columnName);
        }

        /// <summary>
        /// Adds a boolean column that allows null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        protected void AddBooleanNullColumn(String columnName,
                                            Boolean defaultValue)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Add the column.
            AddBooleanColumn(columnName, defaultValue);
        }

        /// <summary>
        /// Adds a boolean column that does not allow null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        protected void AddBooleanNotNullColumn(String columnName)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Add the column.
            AddBooleanColumn(columnName, null, false);
        }

        /// <summary>
        /// Adds a boolean column that does not allow null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        protected void AddBooleanNotNullColumn(String columnName,
                                               Boolean defaultValue)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Add the column.
            AddBooleanColumn(columnName, defaultValue, false);
        }

        /// <summary>
        /// Adds a boolean column to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="allowDbNull">Sets a value that indicates whether null values are allowed in this column for rows that belong to the table.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        private void AddBooleanColumn(String columnName,
                                      Boolean? defaultValue = null,
                                      Boolean allowDbNull = true)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Add the column.
            AddColumn(columnName, Type.GetType("System.Boolean", false, true), defaultValue, allowDbNull);
        }

        /// <summary>
        /// Adds a <see cref="Guid"/> column to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        protected void AddByteNullColumn(String columnName)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Add the column.
            AddByteColumn(columnName);
        }

        /// <summary>
        /// Adds a <see cref="Guid"/> column to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        protected void AddByteNullColumn(String columnName,
                                         Byte defaultValue)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Add the column.
            AddByteColumn(columnName, defaultValue);
        }

        /// <summary>
        /// Adds a <see cref="Guid"/> column to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        protected void AddByteNotNullColumn(String columnName)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Add the column.
            AddByteColumn(columnName, null, false);
        }

        /// <summary>
        /// Adds a <see cref="Guid"/> column to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        protected void AddByteNotNullColumn(String columnName,
                                            Byte defaultValue)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Add the column.
            AddByteColumn(columnName, defaultValue, false);
        }

        /// <summary>
        /// Adds a <see cref="Byte"/> column to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="allowDbNull">Sets a value that indicates whether null values are allowed in this column for rows that belong to the table.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        private void AddByteColumn(String columnName,
                                   Byte? defaultValue = null,
                                   Boolean allowDbNull = true)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Add the column.
            AddColumn(columnName, Type.GetType("System.Byte", false, true), defaultValue, allowDbNull);
        }

        /// <summary>
        /// Adds a <see cref="Guid"/> column to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        protected void AddGuidNullColumn(String columnName)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Add the column.
            AddGuidColumn(columnName);
        }

        /// <summary>
        /// Adds a <see cref="Guid"/> column to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        protected void AddGuidNullColumn(String columnName,
                                         Guid defaultValue)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Add the column.
            AddGuidColumn(columnName, defaultValue);
        }

        /// <summary>
        /// Adds a <see cref="Guid"/> column to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        protected void AddGuidNotNullColumn(String columnName)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Add the column.
            AddGuidColumn(columnName, null, false);
        }

        /// <summary>
        /// Adds a <see cref="Guid"/> column to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        protected void AddGuidNotNullColumn(String columnName,
                                            Guid defaultValue)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Add the column.
            AddGuidColumn(columnName, defaultValue, false);
        }

        /// <summary>
        /// Adds a <see cref="Guid"/> column to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="allowDbNull">Sets a value that indicates whether null values are allowed in this column for rows that belong to the table.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        private void AddGuidColumn(String columnName,
                                   Guid? defaultValue = null,
                                   Boolean allowDbNull = true)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Add the column.
            AddColumn(columnName, Type.GetType("System.Guid", false, true), defaultValue, allowDbNull);
        }

        /// <summary>
        /// Adds a string column that allows null values.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        protected void AddStringNullColumn(String columnName)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            AddStringColumn(columnName);
        }

        /// <summary>
        /// Adds a string column that allows null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        protected void AddStringNullColumn(String columnName,
                                           String defaultValue)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            AddStringColumn(columnName, defaultValue);
        }

        /// <summary>
        /// Adds a string column that does not allow null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        protected void AddStringNotNullColumn(String columnName)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            AddStringColumn(columnName, null, false);
        }

        /// <summary>
        /// Adds a string column that does not allow null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="maxLength">The maximum length of the column in characters.</param>
        protected void AddStringNotNullColumn(String columnName,
                                              Int32 maxLength)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));
            if (maxLength < 0)
                throw new ArgumentException(Resources.ExceptionValueLessThanZero, nameof(maxLength));

            AddStringColumn(columnName, null, false, maxLength);
        }

        /// <summary>
        /// Adds a string column that does not allow null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        protected void AddStringNotNullColumn(String columnName,
                                              String defaultValue)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));
            if (defaultValue == null)
                throw new ArgumentNullException(nameof(defaultValue));

            AddStringColumn(columnName, defaultValue, false);
        }

        /// <summary>
        /// Adds a string column that does not allow null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        /// <param name="maxLength">The maximum length of the column in characters.</param>
        protected void AddStringNotNullColumn(String columnName,
                                              String defaultValue,
                                              Int32 maxLength)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));
            if (maxLength < 0)
                throw new ArgumentException(Resources.ExceptionValueLessThanZero, nameof(maxLength));

            AddStringColumn(columnName, defaultValue, false, maxLength);
        }

        /// <summary>
        /// Adds a string column.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="allowDbNull">Sets a value that indicates whether null values are allowed in this column for rows that belong to the table.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        /// <param name="maxLength">The maximum length of the column in characters.</param>
        private void AddStringColumn(String columnName,
                                     String defaultValue = null,
                                     Boolean allowDbNull = true,
                                     Int32? maxLength = null)
        {
            AddColumn(columnName, Type.GetType("System.String", false, true), defaultValue, allowDbNull, false, 0, 1,
                       null, false, false, maxLength);
        }

        /// <summary>
        /// Adds a <see cref="Single"/> column that allows null values.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        protected void AddDoubleNullColumn(String columnName)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddDoubleColumn(columnName);
        }

        /// <summary>
        /// Adds a <see cref="Single"/> column that allows null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        protected void AddDoubleNullColumn(String columnName,
                                           Double defaultValue)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddDoubleColumn(columnName, defaultValue);
        }

        /// <summary>
        /// Adds a <see cref="Single"/> column that does not allow null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        protected void AddDoubleNotNullColumn(String columnName)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddDoubleColumn(columnName, null, false);
        }

        /// <summary>
        /// Adds a <see cref="Single"/> column that does not allow null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        /// ///
        protected void AddDoubleNotNullColumn(String columnName,
                                              Double defaultValue)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddDoubleColumn(columnName, defaultValue, false);
        }

        /// <summary>
        /// Adds a <see cref="Single"/> column.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="allowDbNull">Sets a value that indicates whether null values are allowed in this column for rows that belong to the table.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        private void AddDoubleColumn(String columnName,
                                     Double? defaultValue = null,
                                     Boolean allowDbNull = true)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddColumn(columnName, Type.GetType("System.Double", false, true), defaultValue, allowDbNull);
        }


        /// <summary>
        /// Adds a <see cref="Single"/> column that allows null values.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        protected void AddSingleNullColumn(String columnName)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddSingleColumn(columnName);
        }

        /// <summary>
        /// Adds a <see cref="Single"/> column that allows null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        protected void AddSingleNullColumn(String columnName,
                                           Single defaultValue)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddSingleColumn(columnName, defaultValue);
        }

        /// <summary>
        /// Adds a <see cref="Single"/> column that does not allow null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        protected void AddSingleNotNullColumn(String columnName)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddSingleColumn(columnName, null, false);
        }

        /// <summary>
        /// Adds a <see cref="Single"/> column that does not allow null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        /// ///
        protected void AddSingleNotNullColumn(String columnName,
                                              Single defaultValue)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddSingleColumn(columnName, defaultValue, false);
        }

        /// <summary>
        /// Adds a <see cref="Single"/> column.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="allowDbNull">Sets a value that indicates whether null values are allowed in this column for rows that belong to the table.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        private void AddSingleColumn(String columnName,
                                     Single? defaultValue = null,
                                     Boolean allowDbNull = true)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddColumn(columnName, Type.GetType("System.Single", false, true), defaultValue, allowDbNull);
        }

        /// <summary>
        /// Adds a date and time column that allows null values.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        protected void AddDateTimeNullColumn(String columnName)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddDateTimeColumn(columnName);
        }

        /// <summary>
        /// Adds a <see cref="DateTime"/> column that allows null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        protected void AddDateTimeNullColumn(String columnName,
                                             DateTime defaultValue)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddDateTimeColumn(columnName, defaultValue);
        }

        /// <summary>
        /// Adds a date and time column that does not allow null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        protected void AddDateTimeNotNullColumn(String columnName)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddDateTimeColumn(columnName, null, false);
        }

        /// <summary>
        /// Adds a date and time column that does not allow null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        protected void AddDateTimeNotNullColumn(String columnName,
                                                DateTime defaultValue)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddDateTimeColumn(columnName, defaultValue, false);
        }

        /// <summary>
        /// Adds a date and time column.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="allowDbNull">Sets a value that indicates whether null values are allowed in this column for rows that belong to the table.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        private void AddDateTimeColumn(String columnName,
                                       DateTime? defaultValue = null,
                                       Boolean allowDbNull = true)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddColumn(columnName, Type.GetType("System.DateTime", false, true), defaultValue, allowDbNull);
        }

        /// <summary>
        /// Adds a 16-bit signed integer column that allows null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        protected void AddInt16NullColumn(String columnName)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddInt16Column(columnName);
        }

        /// <summary>
        /// Adds a 16-bit signed integer column that allows null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        protected void AddInt16NullColumn(String columnName,
                                          Int16 defaultValue)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddInt16Column(columnName, defaultValue);
        }

        /// <summary>
        /// Adds a 16-bit signed integer column that does not allow null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        protected void AddInt16NotNullColumn(String columnName)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddInt16Column(columnName, null, false);
        }

        /// <summary>
        /// Adds a 16-bit signed integer column that does not allow null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        protected void AddInt16NotNullColumn(String columnName, Int16 defaultValue)
        {
            AddInt16Column(columnName, defaultValue, false);
        }

        /// <summary>
        /// Adds a 16-bit signed integer column to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="allowDbNull">Sets a value that indicates whether null values are allowed in this column for rows that belong to the table.</param>
        /// <param name="autoIncrement">Sets the starting value for a column that has its AutoIncrement property set to true.</param>
        /// <param name="autoIncrementSeed">Sets the starting value for a column that has its AutoIncrement property set to true.</param>
        /// <param name="autoIncrementStep">Sets the increment used by a column with its AutoIncrement property set to true.</param>
        /// <param name="readOnly">Sets a value that indicates whether the column allows for changes as soon as a row has been added to the table.</param>
        /// <param name="unique">Set a value that indicates whether the values in each row of the column must be unique.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        protected void AddInt16Column(String columnName,
                                      Int16? defaultValue = null,
                                      Boolean allowDbNull = true,
                                      Boolean autoIncrement = false,
                                      Int64 autoIncrementSeed = 0,
                                      Int64 autoIncrementStep = 1,
                                      Boolean readOnly = false,
                                      Boolean unique = false)
        {
            AddColumn(columnName, Type.GetType("System.Int16", false, true), defaultValue, allowDbNull, autoIncrement,
                       autoIncrementSeed, autoIncrementStep, null, readOnly, unique);
        }

        /// <summary>
        /// Adds a 32-bit signed integer column that allows null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        protected void AddInt32NullColumn(String columnName)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddInt32Column(columnName);
        }

        /// <summary>
        /// Adds a 32-bit signed integer column that allows null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        protected void AddInt32NullColumn(String columnName,
                                          Int32 defaultValue)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddInt32Column(columnName, defaultValue);
        }

        /// <summary>
        /// Adds a 32-bit signed integer column that does not allow null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        protected void AddInt32NotNullColumn(String columnName)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddInt32Column(columnName, null, false);
        }

        /// <summary>
        /// Adds a 32-bit signed integer column that does not allow null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        protected void AddInt32NotNullColumn(String columnName,
                                             Int32 defaultValue)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddInt32Column(columnName, defaultValue, false);
        }

        /// <summary>
        /// Adds an autoincremented 32-bit signed integer columnto a data table.  The increment seed and step values 
        /// will be set to 1 and 1, respectively, and the column will be set to read only and unique.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        protected void AddInt32AutoIncrementColumn(String columnName)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddInt32Column(columnName, null, false, true, 1, 1, true, true);
        }

        /// <summary>
        /// Adds an autoincremented 32-bit signed integer columnto a data table.  The column will be set to
        /// read only and unique.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="autoIncrementSeed">Sets the starting value for a column that has its AutoIncrement property set to true.</param>
        /// <param name="autoIncrementStep">Sets the increment used by a column with its AutoIncrement property set to true.</param>
        protected void AddInt32AutoIncrementColumn(String columnName,
                                                   Int64 autoIncrementSeed,
                                                   Int64 autoIncrementStep)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddInt32Column(columnName, null, false, true, autoIncrementSeed, autoIncrementSeed, true, true);
        }

        /// <summary>
        /// Adds a 32-bit signed integer column to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="allowDbNull">Sets a value that indicates whether null values are allowed in this column for rows that belong to the table.</param>
        /// <param name="autoIncrement">Sets the starting value for a column that has its AutoIncrement property set to true.</param>
        /// <param name="autoIncrementSeed">Sets the starting value for a column that has its AutoIncrement property set to true.</param>
        /// <param name="autoIncrementStep">Sets the increment used by a column with its AutoIncrement property set to true.</param>
        /// <param name="readOnly">Sets a value that indicates whether the column allows for changes as soon as a row has been added to the table.</param>
        /// <param name="unique">Set a value that indicates whether the values in each row of the column must be unique.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        protected void AddInt32Column(String columnName,
                                      Int32? defaultValue = null,
                                      Boolean allowDbNull = true,
                                      Boolean autoIncrement = false,
                                      Int64 autoIncrementSeed = 0,
                                      Int64 autoIncrementStep = 1,
                                      Boolean readOnly = false,
                                      Boolean unique = false)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddColumn(columnName, Type.GetType("System.Int32", false, true), defaultValue, allowDbNull, autoIncrement,
                       autoIncrementSeed, autoIncrementStep, null, readOnly, unique);
        }

        /// <summary>
        /// Adds a 64-bit signed integer column that allows null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        protected void AddInt64NullColumn(String columnName)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddInt64Column(columnName);
        }

        /// <summary>
        /// Adds a 64-bit signed integer column that allows null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        protected void AddInt64NullColumn(String columnName,
                                          Int64 defaultValue)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddInt64Column(columnName, defaultValue);
        }

        /// <summary>
        /// Adds a 64-bit signed integer column that does not allow null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        protected void AddInt64NotNullColumn(String columnName)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddInt64Column(columnName, null, false);
        }

        /// <summary>
        /// Adds a 64-bit signed integer column that does not allow null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        public void AddInt64NotNullColumn(String columnName,
                                          Int64 defaultValue)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddInt64Column(columnName, defaultValue, false);
        }

        /// <summary>
        /// Adds a 64-bit signed integer column to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="allowDbNull">Sets a value that indicates whether null values are allowed in this column for rows that belong to the table.</param>
        /// <param name="autoIncrement">Sets the starting value for a column that has its AutoIncrement property set to true.</param>
        /// <param name="autoIncrementSeed">Sets the starting value for a column that has its AutoIncrement property set to true.</param>
        /// <param name="autoIncrementStep">Sets the increment used by a column with its AutoIncrement property set to true.</param>
        /// <param name="readOnly">Sets a value that indicates whether the column allows for changes as soon as a row has been added to the table.</param>
        /// <param name="unique">Set a value that indicates whether the values in each row of the column must be unique.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        public void AddInt64Column(String columnName,
                                   Int64? defaultValue = null,
                                   Boolean allowDbNull = true,
                                   Boolean autoIncrement = false,
                                   Int64 autoIncrementSeed = 0,
                                   Int64 autoIncrementStep = 1,
                                   Boolean readOnly = false,
                                   Boolean unique = false)
        {
            AddColumn(columnName, Type.GetType("System.Int64", false, true), defaultValue, allowDbNull,
                       autoIncrement, autoIncrementSeed, autoIncrementStep, null, readOnly, unique);
        }

        /// <summary>
        /// Adds a 16-bit unsigned integer column that allows null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        protected void AddUInt16NullColumn(String columnName)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddUInt16Column(columnName);
        }

        /// <summary>
        /// Adds a 16-bit unsigned integer column that allows null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        protected void AddUInt16NullColumn(String columnName,
                                           UInt16 defaultValue)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddUInt16Column(columnName, defaultValue);
        }

        /// <summary>
        /// Adds a 16-bit unsigned integer column that does not allow null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        protected void AddUInt16NotNullColumn(String columnName)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddUInt16Column(columnName, null, false);
        }

        /// <summary>
        /// Adds a 16-bit unsigned integer column that does not allow null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        protected void AddUInt16NotNullColumn(String columnName,
                                              UInt16 defaultValue)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddUInt16Column(columnName, defaultValue, false);
        }

        /// <summary>
        /// Adds a 16-bit unsigned integer column to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="allowDbNull">Sets a value that indicates whether null values are allowed in this column for rows that belong to the table.</param>
        /// <param name="autoIncrement">Sets the starting value for a column that has its AutoIncrement property set to true.</param>
        /// <param name="autoIncrementSeed">Sets the starting value for a column that has its AutoIncrement property set to true.</param>
        /// <param name="autoIncrementStep">Sets the increment used by a column with its AutoIncrement property set to true.</param>
        /// <param name="readOnly">Sets a value that indicates whether the column allows for changes as soon as a row has been added to the table.</param>
        /// <param name="unique">Set a value that indicates whether the values in each row of the column must be unique.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        protected void AddUInt16Column(String columnName,
                                       UInt16? defaultValue = null,
                                       Boolean allowDbNull = true,
                                       Boolean autoIncrement = false,
                                       Int64 autoIncrementSeed = 0,
                                       Int64 autoIncrementStep = 1,
                                       Boolean readOnly = false,
                                       Boolean unique = false)
        {
            AddColumn(columnName, Type.GetType("System.UInt16", false, true), defaultValue, allowDbNull,
                       autoIncrement, autoIncrementSeed, autoIncrementStep, null, readOnly, unique);
        }

        /// <summary>
        /// Adds a 32-bit unsigned integer column that allows null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        protected void AddUInt32NullColumn(String columnName)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddUInt32Column(columnName);
        }

        /// <summary>
        /// Adds a 32-bit unsigned integer column that allows null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        protected void AddUInt32NullColumn(String columnName,
                                           UInt32 defaultValue)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddUInt32Column(columnName, defaultValue);
        }

        /// <summary>
        /// Adds a 32-bit unsigned integer column that does not allow null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        protected void AddUInt32NotNullColumn(String columnName)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddUInt32Column(columnName, null, false);
        }

        /// <summary>
        /// Adds a 32-bit unsigned integer column that does not allow null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        protected void AddUInt32NotNullColumn(String columnName,
                                              UInt32 defaultValue)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddUInt32Column(columnName, defaultValue, false);
        }

        /// <summary>
        /// Adds an autoincremented 32-bit unsigned integer columnto a data table.  The increment seed and step values 
        /// will be set to 1 and 1, respectively, and the column will be set to read only and unique.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        protected void AddUInt32AutoIncrementColumn(String columnName)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddUInt32Column(columnName, null, false, true, 1, 1, true, true);
        }

        /// <summary>
        /// Adds an autoincremented 32-bit unsigned integer columnto a data table.  The column will be set to
        /// read only and unique.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="autoIncrementSeed">Sets the starting value for a column that has its AutoIncrement property set to true.</param>
        /// <param name="autoIncrementStep">Sets the increment used by a column with its AutoIncrement property set to true.</param>
        protected void AddUInt32AutoIncrementColumn(String columnName,
                                                    Int64 autoIncrementSeed,
                                                    Int64 autoIncrementStep)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddUInt32Column(columnName, null, false, true, autoIncrementSeed, autoIncrementSeed, true, true);
        }

        /// <summary>
        /// Adds a 32-bit unsigned integer column to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="allowDbNull">Sets a value that indicates whether null values are allowed in this column for rows that belong to the table.</param>
        /// <param name="autoIncrement">Sets the starting value for a column that has its AutoIncrement property set to true.</param>
        /// <param name="autoIncrementSeed">Sets the starting value for a column that has its AutoIncrement property set to true.</param>
        /// <param name="autoIncrementStep">Sets the increment used by a column with its AutoIncrement property set to true.</param>
        /// <param name="readOnly">Sets a value that indicates whether the column allows for changes as soon as a row has been added to the table.</param>
        /// <param name="unique">Set a value that indicates whether the values in each row of the column must be unique.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        protected void AddUInt32Column(String columnName,
                                       UInt32? defaultValue = null,
                                       Boolean allowDbNull = true,
                                       Boolean autoIncrement = false,
                                       Int64 autoIncrementSeed = 0,
                                       Int64 autoIncrementStep = 1,
                                       Boolean readOnly = false,
                                       Boolean unique = false)
        {
            AddColumn(columnName, Type.GetType("System.UInt32", false, true), defaultValue, allowDbNull,
                       autoIncrement, autoIncrementSeed, autoIncrementStep, null, readOnly, unique);
        }

        /// <summary>
        /// Adds a 64-bit unsigned integer column that allows null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        protected void AddUInt64NullColumn(String columnName)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddUInt64Column(columnName);
        }

        /// <summary>
        /// Adds a 64-bit unsigned integer column that allows null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        protected void AddUInt64NullColumn(String columnName,
                                           UInt64 defaultValue)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddUInt64Column(columnName, defaultValue);
        }

        /// <summary>
        /// Adds a 64-bit unsigned integer column that does not allow null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        protected void AddUInt64NotNullColumn(String columnName)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddUInt64Column(columnName, null, false);
        }

        /// <summary>
        /// Adds a 64-bit unsigned integer column that does not allow null values to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        protected void AddUInt64NotNullColumn(String columnName,
                                              UInt64 defaultValue)
        {
            // Validate the argument(s)
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(columnName));

            // Create the column.
            AddUInt64Column(columnName, defaultValue, false);
        }

        /// <summary>
        /// Adds a 64-bit unsigned integer column to a data table.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="allowDbNull">Sets a value that indicates whether null values are allowed in this column for rows that belong to the table.</param>
        /// <param name="autoIncrement">Sets the starting value for a column that has its AutoIncrement property set to true.</param>
        /// <param name="autoIncrementSeed">Sets the starting value for a column that has its AutoIncrement property set to true.</param>
        /// <param name="autoIncrementStep">Sets the increment used by a column with its AutoIncrement property set to true.</param>
        /// <param name="readOnly">Sets a value that indicates whether the column allows for changes as soon as a row has been added to the table.</param>
        /// <param name="unique">Set a value that indicates whether the values in each row of the column must be unique.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        private void AddUInt64Column(String columnName,
                                     UInt64? defaultValue = null,
                                     Boolean allowDbNull = true,
                                     Boolean autoIncrement = false,
                                     Int64 autoIncrementSeed = 0,
                                     Int64 autoIncrementStep = 1,
                                     Boolean readOnly = false,
                                     Boolean unique = false)
        {
            AddColumn(columnName, Type.GetType("System.UInt64", false, true), defaultValue, allowDbNull,
                       autoIncrement, autoIncrementSeed, autoIncrementStep, null, readOnly, unique);
        }

        /// <summary>
        /// Adds a DataColumn to DataTable instance.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="dataType">Type of the data stored in the column.</param>
        /// <param name="allowDbNull">Sets a value that indicates whether null values are allowed in this column for rows that belong to the table.</param>
        /// <param name="autoIncrement">Sets the starting value for a column that has its AutoIncrement property set to true.</param>
        /// <param name="autoIncrementSeed">Sets the starting value for a column that has its AutoIncrement property set to true.</param>
        /// <param name="autoIncrementStep">Sets the increment used by a column with its AutoIncrement property set to true.</param>
        /// <param name="caption">Sets the caption for the column.</param>
        /// <param name="readOnly">Sets a value that indicates whether the column allows for changes as soon as a row has been added to the table.</param>
        /// <param name="unique">Set a value that indicates whether the values in each row of the column must be unique.</param>
        /// <param name="maxLength">Sets the maximum length of a text column.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        protected void AddColumn(String columnName,
                                 Type dataType,
                                 Object defaultValue = null,
                                 Boolean allowDbNull = true,
                                 Boolean autoIncrement = false,
                                 Int64 autoIncrementSeed = 0,
                                 Int64 autoIncrementStep = 1,
                                 String caption = null,
                                 Boolean readOnly = false,
                                 Boolean unique = false,
                                 Int32? maxLength = null)
        {
            // Create a data column.
            var dataColumn = CreateColumn(columnName, dataType, defaultValue, allowDbNull, autoIncrement,
                                                  autoIncrementSeed, autoIncrementStep, caption, readOnly, unique);

            // Assert
            Debug.Assert(dataColumn != null, "dataColumn != null");

            // Add the column to the table.
            AddColumn(dataColumn);
        }

        /// <summary>
        /// Adds a DataColumn to DataTable instance.
        /// </summary>
        /// <param name="dataColumn">DataColumn instance.</param>
        protected virtual void AddColumn(DataColumn dataColumn)
        {
            // Validate the argument(s)
            if (dataColumn == null) throw new ArgumentNullException(nameof(dataColumn));

            // Check if the column already exists.
            if (CheckIfColumnExist(dataColumn.ColumnName)) return;

            // Add the column.
            GuardDataTableExist();
            InternalDataTable.Columns.Add(dataColumn);
        }

        /// <summary>
        /// Creates a new DataColumn instance.
        /// </summary>
        /// <param name="columnName">A string that represents the name of the column to be created.</param>
        /// <param name="dataType">A supported DataType.</param>
        /// <param name="allowDbNull">Sets a value that indicates whether null values are allowed in this column for rows that belong to the table.</param>
        /// <param name="autoIncrement">Sets the starting value for a column that has its AutoIncrement property set to true.</param>
        /// <param name="autoIncrementSeed">Sets the starting value for a column that has its AutoIncrement property set to true.</param>
        /// <param name="autoIncrementStep">Sets the increment used by a column with its AutoIncrement property set to true.</param>
        /// <param name="caption">Sets the caption for the column.</param>
        /// <param name="readOnly">Sets a value that indicates whether the column allows for changes as soon as a row has been added to the table.</param>
        /// <param name="unique">Set a value that indicates whether the values in each row of the column must be unique.</param>
        /// <param name="maxLength">Sets the maximum length of a text column.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        /// <returns>DataColumn instance.</returns>
        protected DataColumn CreateColumn(String columnName,
                                          Type dataType,
                                          Object defaultValue = null,
                                          Boolean allowDbNull = true,
                                          Boolean autoIncrement = false,
                                          Int64 autoIncrementSeed = 0,
                                          Int64 autoIncrementStep = 1,
                                          String caption = null,
                                          Boolean readOnly = false,
                                          Boolean unique = false,
                                          Int32? maxLength = null)
        {
            // A new DataColumn instance.
            var dataColumn = new DataColumn(columnName, dataType);

            // Set the column values.
            ConfigureColumn(dataColumn, defaultValue, allowDbNull, autoIncrement, autoIncrementSeed, autoIncrementStep,
                             caption, readOnly, unique, maxLength);
            return dataColumn;
        }

        /// <summary>
        /// Configures the DataColumn instance with values.
        /// </summary>
        /// <param name="dataColumn">DataColumn instance.</param>
        /// <param name="allowDbNull">Sets a value that indicates whether null values are allowed in this column for rows that belong to the table.</param>
        /// <param name="autoIncrement">Sets the starting value for a column that has its AutoIncrement property set to true.</param>
        /// <param name="autoIncrementSeed">Sets the starting value for a column that has its AutoIncrement property set to true.</param>
        /// <param name="autoIncrementStep">Sets the increment used by a column with its AutoIncrement property set to true.</param>
        /// <param name="caption">Sets the caption for the column.</param>
        /// <param name="readOnly">Sets a value that indicates whether the column allows for changes as soon as a row has been added to the table.</param>
        /// <param name="unique">Set a value that indicates whether the values in each row of the column must be unique.</param>
        /// <param name="maxLength">Sets the maximum length of a text column.</param>
        /// <param name="defaultValue">Sets the default value for the column when you are creating new rows.</param>
        protected virtual void ConfigureColumn(DataColumn dataColumn,
                                               Object defaultValue = null,
                                               Boolean allowDbNull = true,
                                               Boolean autoIncrement = false,
                                               Int64 autoIncrementSeed = 0,
                                               Int64 autoIncrementStep = 1,
                                               String caption = null,
                                               Boolean readOnly = false,
                                               Boolean unique = false,
                                               Int32? maxLength = null)
        {
            // Set the property values.
            dataColumn.AllowDBNull = allowDbNull;
            dataColumn.AutoIncrement = autoIncrement;
            dataColumn.AutoIncrementSeed = autoIncrementSeed;
            dataColumn.AutoIncrementStep = autoIncrementStep;
            dataColumn.ReadOnly = readOnly;
            dataColumn.Unique = unique;

            if (String.IsNullOrEmpty(caption) == false)
                dataColumn.Caption = caption;
            if (maxLength != null)
                dataColumn.MaxLength = maxLength.GetValueOrDefault();
            if (defaultValue != null)
                dataColumn.DefaultValue = defaultValue;
        }

        /// <summary>
        /// Checks whether the collection contains a column with the specified name.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns><b>true</b> if a column exists with this name; otherwise, <b>false</b>.</returns>
        private Boolean CheckIfColumnExist(String columnName)
        {
            GuardDataTableExist();
            return InternalDataTable.Columns.Contains(columnName);
        }

        /// <summary>
        /// Determines whether the internal <see cref="DataTable"/> exists.
        /// </summary>
        private void GuardDataTableExist()
        {
            if (InternalDataTable == null)
                throw new InvalidOperationException(Resources.ExceptionDataTableInvalid);
        }

        #endregion

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            InternalDataTable?.Dispose();
        }

        #endregion
    }
}
