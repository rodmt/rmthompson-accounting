/***************************************************************************
**
**      Auth:   Rodrigues Thompson, Chief Software Architect
**
**      Name:   Database Connection Class
**
**      Date:   2013 May 13
**
**      Desc:   A wrapper around a database connection instance.
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

namespace ZionSoftware.Solutions.Data
{
    /// <summary>
    /// A wrapper around a database connection instance.
    /// </summary>
    public sealed class DatabaseConnection : IDisposable
    {
        /// <summary>
        /// Gets a <see cref="IDbConnection"/>.
        /// </summary>
        public IDbConnection Connection { get; private set; }

        /// <summary>
        /// Gets a value that indicates whether the 
        /// <see cref="IDbConnection"/> is open.
        /// </summary>
        public Boolean IsOpen => Connection.State == ConnectionState.Open;

	    /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="dbConnection">A <see cref="IDbConnection"/>.</param>
        public DatabaseConnection(IDbConnection dbConnection)
        {
            // Validate the argument(s)
            Connection = dbConnection;
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (Connection != null)
            {
                Connection.Close();
                Connection.Dispose();
                Connection = null;
            }
        }

        #endregion
    }
}
