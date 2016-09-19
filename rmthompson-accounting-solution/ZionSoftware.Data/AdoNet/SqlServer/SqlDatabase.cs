/***************************************************************************
**
**      Auth:   Rodrigues M. Thompson, Chief Software Architect
**
**      Name:   Sql Database Class
**
**      Date:   2013 May 13
**
**      Desc:   Handles the low-level database access.
**
** Copyright © 2016 Zion Software Solutions, LLC. All Rights Reserved.
**
** Unpublished copyright. This material contains proprietary information
** that shall be used or copied only within Zion Software Solutions, 
** except with written permission of Zion Software Solutions.
**
***************************************************************************/

using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using ZionSoftware.Solutions.Data.Properties;

namespace ZionSoftware.Solutions.Data.AdoNet.SqlServer
{
    public class SqlDatabase : Database
    {
        private const Char _ParameterToken = '@';

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="connectionString">A connection string.</param>
        /// <param name="commandTimeout">A command timeout.</param>
        public SqlDatabase(String connectionString,
                           Int32 commandTimeout = 60)
            : base(SqlClientFactory.Instance, new SqlConnectionStringBuilder(connectionString), commandTimeout)
        {
        }

        /// <summary>
        /// Adds a <see cref="IDataParameter"/> to a <see cref="IDbCommand"/> instance.
        /// </summary>
        /// <param name="command">A <see cref="IDbCommand"/> instance.</param>
        /// <param name="parameterName">The name of a <see cref="IDataParameter"/>.</param>
        /// <param name="dbType">The <see cref="DbType"/> of a <see cref="IDataParameter"/>.</param>
        /// <param name="value">The value of a <see cref="IDataParameter"/>.</param>
        public void AddInParameter(IDbCommand command,
                                   String parameterName,
                                   SqlDbType dbType,
                                   Object value)
        {
            // Validate the argument(s)
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            // Create the parameter.
            AddParameter(command, parameterName, dbType, 0, ParameterDirection.Input, true,
                          String.Empty, DataRowVersion.Default, value);
        }

        /// <summary>
        /// Adds a <see cref="IDataParameter"/> to a <see cref="IDbCommand"/> instance.
        /// </summary>
        /// <param name="command">A <see cref="IDbCommand"/> instance.</param>
        /// <param name="parameterName">The name of a <see cref="IDataParameter"/>.</param>
        /// <param name="dbType">The <see cref="DbType"/> of a <see cref="IDataParameter"/>.</param>
        /// <param name="size">The size of a <see cref="IDataParameter"/>.</param>
        /// <param name="value">The value of a <see cref="IDataParameter"/>.</param>
        public void AddInParameter(IDbCommand command,
                                   String parameterName,
                                   SqlDbType dbType,
                                   Int32 size,
                                   Object value)
        {
            // Validate the argument(s)
            if (command == null)
                throw new ArgumentNullException(nameof(command));
            if (String.IsNullOrEmpty(parameterName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(parameterName));

            // Create the parameter.
            AddParameter(command, parameterName, dbType, size, ParameterDirection.Input, true,
                          null, DataRowVersion.Default, value);
        }

        /// <summary>
        /// Adds a <see cref="IDataParameter"/> to a <see cref="IDbCommand"/> instance.
        /// </summary>
        /// <param name="command">A <see cref="IDbCommand"/> instance.</param>
        /// <param name="parameterName">The name of a <see cref="IDataParameter"/>.</param>
        /// <param name="dbType">The <see cref="DbType"/> of a <see cref="IDataParameter"/>.</param>
        /// <param name="value">The value of a <see cref="IDataParameter"/>.</param>
        public void AddOutParameter(IDbCommand command,
                                    String parameterName,
                                    SqlDbType dbType,
                                    Object value)
        {
            // Validate the argument(s)
            if (command == null)
                throw new ArgumentNullException(nameof(command));
            if (String.IsNullOrEmpty(parameterName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(parameterName));

            // Create the parameter.
            AddParameter(command, parameterName, dbType, 0, ParameterDirection.Output, true,
                          String.Empty, DataRowVersion.Default, value);
        }

        /// <summary>
        /// Adds a <see cref="IDataParameter"/> to a <see cref="IDbCommand"/> instance.
        /// </summary>
        /// <param name="command">A <see cref="IDbCommand"/> instance.</param>
        /// <param name="parameterName">The name of a <see cref="IDataParameter"/>.</param>
        /// <param name="dbType">The <see cref="DbType"/> of a <see cref="IDataParameter"/>.</param>
        public void AddOutParameter(IDbCommand command,
                                    String parameterName,
                                    SqlDbType dbType)
        {
            // Validate the argument(s)
            if (command == null)
                throw new ArgumentNullException(nameof(command));
            if (String.IsNullOrEmpty(parameterName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(parameterName));

            // Create the parameter.
            AddParameter(command, parameterName, dbType, 0, ParameterDirection.Output, true,
                          null, DataRowVersion.Default, null);
        }

        /// <summary>
        /// Adds a <see cref="IDataParameter"/> to a <see cref="IDbCommand"/> instance.
        /// </summary>
        /// <param name="command">A <see cref="IDbCommand"/> instance.</param>
        /// <param name="parameterName">The name of a <see cref="IDataParameter"/>.</param>
        /// <param name="dbType">The <see cref="DbType"/> of a <see cref="IDataParameter"/>.</param>
        /// <param name="size">The size of a <see cref="IDataParameter"/>.</param>
        public void AddOutParameter(IDbCommand command,
                                    String parameterName,
                                    SqlDbType dbType,
                                    Int32 size)
        {
            // Validate the argument(s)
            if (command == null)
                throw new ArgumentNullException(nameof(command));
            if (String.IsNullOrEmpty(parameterName))
                throw new ArgumentException(Resources.ExceptionNullOrEmptyString, nameof(parameterName));

            // Create the parameter.
            AddParameter(command, parameterName, dbType, size, ParameterDirection.Output, true,
                          null, DataRowVersion.Default, null);
        }

        /// <summary>
        /// Adds a <see cref="IDataParameter"/> to a <see cref="IDbCommand"/> instance.
        /// </summary>
        /// <param name="command">A <see cref="IDbCommand"/> instance.</param>
        /// <param name="parameterName">The name of a <see cref="IDataParameter"/>.</param>
        /// <param name="dbType">The <see cref="DbType"/> of a <see cref="IDataParameter"/>.</param>
        /// <param name="size">The size of a <see cref="IDataParameter"/>.</param>
        /// <param name="parameterDirection">The <see cref="ParameterDirection"/> of a <see cref="IDataParameter"/>.</param>
        /// <param name="isNullable">Indicates whether a parameter accepts null values.</param>
        /// <param name="sourceColumn">Name of the source column.</param>
        /// <param name="dataRowVersion">A <see cref="DataRowVersion"/>.</param>
        /// <param name="value">The value of a <see cref="IDataParameter"/>.</param>
        public virtual void AddParameter(IDbCommand command,
                                         String parameterName,
                                         SqlDbType dbType,
                                         Int32 size,
                                         ParameterDirection parameterDirection,
                                         Boolean isNullable,
                                         String sourceColumn,
                                         DataRowVersion dataRowVersion,
                                         Object value)
        {
            // Validate the argument(s)
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            // Create the parameter.
            IDataParameter dataParameter = CreateParameter(parameterName, dbType, size, parameterDirection, isNullable,
                                                            sourceColumn, dataRowVersion, value);
            // Add the parameter.
            command.Parameters.Add(dataParameter);
        }

        /// <summary>
        /// Creates a <see cref="IDbDataParameter"/> instance.
        /// </summary>
        /// <param name="parameterName">Parameter name of a <see cref="IDbDataParameter"/>.</param>
        /// <param name="dbType">A <see cref="DbType"/>.</param>
        /// <param name="size">Maximum size, in bytes, of the data within the column.</param>
        /// <param name="parameterDirection">A <see cref="ParameterDirection"/>.</param>
        /// <param name="isNullable">Indicates whether a parameter accepts null values.</param>
        /// <param name="sourceColumn">Name of the source column.</param>
        /// <param name="dataRowVersion">A <see cref="DataRowVersion"/>.</param>
        /// <param name="value">The value of parameter.</param>
        /// <returns>A <see cref="IDbDataParameter"/>.</returns>
        protected virtual IDbDataParameter CreateParameter(String parameterName,
                                                           SqlDbType dbType,
                                                           Int32 size,
                                                           ParameterDirection parameterDirection,
                                                           Boolean isNullable,
                                                           String sourceColumn,
                                                           DataRowVersion dataRowVersion,
                                                           Object value)
        {
            // Create the parameter.
            var dbParameter = CreateParameter(parameterName) as SqlParameter;

            // Check if the parameter is null.
            if (dbParameter == null)
                throw new InvalidOperationException();

            // Configure the parameter.
            ConfigureParameter(dbParameter, dbType, size, parameterDirection, isNullable,
                                sourceColumn, dataRowVersion, value);
            return dbParameter;
        }

        /// <summary>
        /// Configures a <see cref="DbParameter"/>.
        /// </summary>
        /// <param name="parameter">A <see cref="DbParameter"/> instance.</param>
        /// <param name="dbType">A <see cref="DbType"/>.</param>
        /// <param name="size">Maximum size, in bytes, of the data within the column.</param>
        /// <param name="parameterDirection">A <see cref="ParameterDirection"/>.</param>
        /// <param name="isNullable">Indicates whether a parameter accepts null values.</param>
        /// <param name="sourceColumn">Name of the source column.</param>
        /// <param name="dataRowVersion">A <see cref="DataRowVersion"/>.</param>
        /// <param name="value">The value of parameter.</param>
        /// <param name="precision">The maximum number of digits used to represent the Value property of a data provider Parameter object.</param>
        /// <param name="scale">The number of decimal places to which Value is resolved.</param>
        protected virtual void ConfigureParameter(SqlParameter parameter,
                                                  SqlDbType dbType,
                                                  Int32 size,
                                                  ParameterDirection parameterDirection,
                                                  Boolean isNullable,
                                                  String sourceColumn,
                                                  DataRowVersion dataRowVersion,
                                                  Object value,
                                                  Byte precision = 0,
                                                  Byte scale = 0)
        {
            // Validate the argument(s)
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));

            // Set the properties.
            parameter.SqlDbType = dbType;
            parameter.Direction = parameterDirection;
            parameter.IsNullable = isNullable;
            parameter.Precision = precision;
            parameter.Scale = scale;
            parameter.Size = size;
            parameter.SourceColumn = sourceColumn;
            parameter.SourceVersion = dataRowVersion;
            parameter.Value = value ?? DBNull.Value;
        }

        /// <summary>
        /// Formats the parameter name.
        /// </summary>
        /// <param name="parameterName">A parameter name.</param>
        /// <returns>A Formatted parameter name.</returns>
        protected override String FormatParameterName(String parameterName)
        {
            if (parameterName == null) throw new ArgumentNullException(nameof(parameterName));

            // Check if the parameter has valid format token.
            return parameterName[0] != _ParameterToken ? parameterName.Insert(0, new String(_ParameterToken, 1)) : parameterName;
        }

        /// <summary>
        /// Retrieves parameter information from the stored procedure specified in the <see cref="DbCommand"/> and populates the Parameters collection of the specified <see cref="DbCommand"/> object. 
        /// </summary>
        /// <param name="discoveryCommand">The <see cref="DbCommand"/> to do the discovery.</param>
        /// <remarks>The <see cref="DbCommand"/> must be a <see cref="SqlCommand"/> instance.</remarks>
        protected override void DeriveParameters(DbCommand discoveryCommand)
        {
            SqlCommandBuilder.DeriveParameters((SqlCommand)discoveryCommand);
        }
    }
}
