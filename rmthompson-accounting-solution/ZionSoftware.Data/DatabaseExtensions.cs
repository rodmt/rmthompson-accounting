/******************************************************************************
**
**      Auth:   Rodrigues M. Thompson, Chief Software Architect
**
**      Name:   Database Extensions Class
**
**      Date:   2013 May 13
**
**      Desc:   Implements additional functionality for the database class.
**
** Copyright © 2016 Zion Software Solutions, LLC. All Rights Reserved.
**
** Unpublished copyright. This material contains proprietary information
** that shall be used or copied only within Zion Software Solutions, 
** except with written permission of Zion Software Solutions.		
**              
******************************************************************************/

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ZionSoftware.Solutions.Data
{
	/// <summary>
	/// Implements additional functionality for the database class.
	/// </summary>
	public static class DatabaseExtensions
	{
		/// <summary>
		/// Converts the object to a <see cref="bool"/>.
		/// </summary>
		/// <param name="value">The value from the database.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns>The value converted to <see cref="bool"/> or a default value.</returns>
		public static bool AsBoolean(this object value,
									 bool defaultValue = default(bool))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;

            return !bool.TryParse(value.ToString(), out var result) ? defaultValue : result;
        }

		/// <summary>
		/// Converts the object to a <see cref="Byte"/>.
		/// </summary>
		/// <param name="value">The value from the database.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns>The value converted to <see cref="Byte"/> or a default value.</returns>
		public static byte AsByte(this object value,
								  byte defaultValue = default(byte))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;

            return !byte.TryParse(value.ToString(), out var result) ? defaultValue : result;
        }

		/// <summary>
		/// Convert the object to an array <see cref="Byte"/>s.
		/// </summary>
		/// <param name="value">The value from the database.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns>The value converted to <see cref="Byte"/> or a default value.</returns>
		public static byte[] AsBytes(this object value,
									 byte[] defaultValue = default(byte[]))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;

			// Memory Stream instance.
			MemoryStream memoryStream = null;

			try
			{
				// Create a new memory stream instance.
				memoryStream = new MemoryStream();

				// Convert to an array.
				var binaryWriter = new BinaryFormatter();
				binaryWriter.Serialize(memoryStream, value);
				return memoryStream.ToArray();
			}
			finally
			{
				memoryStream?.Close();
				memoryStream?.Dispose();
			}
		}

		/// <summary>
		/// Converts the object to a <see cref="Char"/>.
		/// </summary>
		/// <param name="value">The value from the database.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns>The value converted to <see cref="Char"/> or a default value.</returns>
		public static char AsChar(this object value,
								  char defaultValue = default(char))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;

            return !char.TryParse(value.ToString(), out var result) ? defaultValue : result;
        }

		/// <summary>
		/// Converts the object to a <see cref="decimal"/>.
		/// </summary>
		/// <param name="value">The value from the database.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns>The value converted to <see cref="decimal"/> or a default value.</returns>
		public static decimal AsDecimal(this object value,
										decimal defaultValue = default(decimal))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;

            return !decimal.TryParse(value.ToString(), out var result) ? defaultValue : result;
        }

		/// <summary>
		/// Converts the object to a <see cref="DateTime"/>.
		/// </summary>
		/// <param name="value">The value from the database.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns>The value converted to <see cref="DateTime"/> or a default value.</returns>
		public static DateTime AsDateTime(this object value,
										  DateTime defaultValue = default(DateTime))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;

            return !DateTime.TryParse(value.ToString(), out var result) ? defaultValue : result;
        }

		/// <summary>
		/// Converts the object to a <see cref="Guid"/>.
		/// </summary>
		/// <param name="value">The value from the database.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns>The value converted to <see cref="Guid"/> or a default value.</returns>
		public static Guid AsGuid(this object value,
								  Guid defaultValue = default(Guid))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;

            return !Guid.TryParse(value.ToString(), out var result) ? defaultValue : result;
        }

        /// <summary>
        /// Converts the object to a <see cref="short"/>.
        /// </summary>
        /// <param name="value">The value from the database.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value converted to <see cref="short"/> or a default value.</returns>
        public static short AsInt16(this object value,
									short defaultValue = default(short))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;

            return !short.TryParse(value.ToString(), out var result) ? defaultValue : result;
        }

        /// <summary>
        /// Converts the object to a <see cref="int"/>.
        /// </summary>
        /// <param name="value">The value from the database.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value converted to <see cref="int"/> or a default value.</returns>
        public static int AsInt32(this object value,
									int defaultValue = default(int))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;

		    return !int.TryParse(value.ToString(), out var result) ? defaultValue : result;
		}

        /// <summary>
        /// Converts the object to a <see cref="long"/>.
        /// </summary>
        /// <param name="value">The value from the database.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value converted to <see cref="long"/> or a default value.</returns>
        public static long AsInt64(this object value,
								   long defaultValue = default(long))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;

            return !long.TryParse(value.ToString(), out var result) ? defaultValue : result;
        }

        /// <summary>
        /// Converts the object to a <see cref="sbyte"/>.
        /// </summary>
        /// <param name="value">The value from the database.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value converted to <see cref="sbyte"/> or a default value.</returns>
        public static sbyte AsSByte(this object value,
									sbyte defaultValue = default(sbyte))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;

            return !sbyte.TryParse(value.ToString(), out var result) ? defaultValue : result;
        }

        /// <summary>
        /// Converts the object to a <see cref="float"/>.
        /// </summary>
        /// <param name="value">The value from the database.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value converted to <see cref="float"/> or a default value.</returns>
        public static float AsSingle(this object value,
									 float defaultValue = default(float))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;

            return !float.TryParse(value.ToString(), out var result) ? defaultValue : result;
        }

        /// <summary>
        /// Converts the object to a <see cref="string"/>.
        /// </summary>
        /// <param name="value">The value from the database.</param>
        /// <param name="defaultValue">A default value.</param>
        /// <returns>The value converted to <see cref="string"/> or a default value.</returns>
        public static string AsString(this object value,
									  string defaultValue = default(string))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;
			return value.ToString();
		}

        /// <summary>
        /// Converts the object to a <see cref="ushort"/>.
        /// </summary>
        /// <param name="value">The value from the database.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value converted to <see cref="ushort"/> or a default value.</returns>
        public static ushort AsUInt16(this object value,
									  ushort defaultValue = default(ushort))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;

            return !ushort.TryParse(value.ToString(), out var result) ? defaultValue : result;
        }

        /// <summary>
        /// Converts the object to a <see cref="uint"/>.
        /// </summary>
        /// <param name="value">The value from the database.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value converted to <see cref="uint"/> or a default value.</returns>
        public static uint AsUInt32(this object value,
									uint defaultValue = default(uint))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;

            return !uint.TryParse(value.ToString(), out var result) ? defaultValue : result;
        }

        /// <summary>
        /// Converts the object to a <see cref="ulong"/>.
        /// </summary>
        /// <param name="value">The value from the database.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value converted to <see cref="ulong"/> or a default value.</returns>
        public static ulong AsUInt64(this object value,
									 ulong defaultValue = default(ulong))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;

            return !ulong.TryParse(value.ToString(), out var result) ? defaultValue : result;
        }
	}
}
