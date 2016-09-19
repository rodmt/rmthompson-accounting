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
		/// Converts the object to a <see cref="Boolean"/>.
		/// </summary>
		/// <param name="value">The value from the database.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns>The value converted to <see cref="Boolean"/> or a default value.</returns>
		public static Boolean AsBoolean(this Object value,
										Boolean defaultValue = default(Boolean))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;

			Boolean result;
			return !Boolean.TryParse(value.ToString(), out result) ? defaultValue : result;
		}

		/// <summary>
		/// Converts the object to a <see cref="Byte"/>.
		/// </summary>
		/// <param name="value">The value from the database.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns>The value converted to <see cref="Byte"/> or a default value.</returns>
		public static Byte AsByte(this Object value,
								  Byte defaultValue = default(Byte))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;

			Byte result;
			return !Byte.TryParse(value.ToString(), out result) ? defaultValue : result;
		}

		/// <summary>
		/// Convert the object to an array <see cref="Byte"/>s.
		/// </summary>
		/// <param name="value">The value from the database.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns>The value converted to <see cref="Byte"/> or a default value.</returns>
		public static Byte[] AsBytes(this Object value,
									 Byte[] defaultValue = default(Byte[]))
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
		public static Char AsChar(this Object value,
								  Char defaultValue = default(Char))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;

			Char result;
			return !Char.TryParse(value.ToString(), out result) ? defaultValue : result;
		}

		/// <summary>
		/// Converts the object to a <see cref="Decimal"/>.
		/// </summary>
		/// <param name="value">The value from the database.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns>The value converted to <see cref="Decimal"/> or a default value.</returns>
		public static Decimal AsDecimal(this Object value,
										Decimal defaultValue = default(Decimal))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;

			Decimal result;
			return !Decimal.TryParse(value.ToString(), out result) ? defaultValue : result;
		}

		/// <summary>
		/// Converts the object to a <see cref="DateTime"/>.
		/// </summary>
		/// <param name="value">The value from the database.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns>The value converted to <see cref="DateTime"/> or a default value.</returns>
		public static DateTime AsDateTime(this Object value,
										  DateTime defaultValue = default(DateTime))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;

			DateTime result;
			return !DateTime.TryParse(value.ToString(), out result) ? defaultValue : result;
		}

		/// <summary>
		/// Converts the object to a <see cref="Guid"/>.
		/// </summary>
		/// <param name="value">The value from the database.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns>The value converted to <see cref="Guid"/> or a default value.</returns>
		public static Guid AsGuid(this Object value,
								  Guid defaultValue = default(Guid))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;

			Guid result;
			return !Guid.TryParse(value.ToString(), out result) ? defaultValue : result;
		}

		/// <summary>
		/// Converts the object to a <see cref="Int16"/>.
		/// </summary>
		/// <param name="value">The value from the database.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns>The value converted to <see cref="Int16"/> or a default value.</returns>
		public static Int16 AsInt16(this Object value,
									Int16 defaultValue = default(Int16))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;

			Int16 result;
			return !Int16.TryParse(value.ToString(), out result) ? defaultValue : result;
		}

		/// <summary>
		/// Converts the object to a <see cref="Int32"/>.
		/// </summary>
		/// <param name="value">The value from the database.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns>The value converted to <see cref="Int32"/> or a default value.</returns>
		public static Int32 AsInt32(this Object value,
									Int32 defaultValue = default(Int32))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;

			Int32 result;
			return !Int32.TryParse(value.ToString(), out result) ? defaultValue : result;
		}

		/// <summary>
		/// Converts the object to a <see cref="Int64"/>.
		/// </summary>
		/// <param name="value">The value from the database.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns>The value converted to <see cref="Int64"/> or a default value.</returns>
		public static Int64 AsInt64(this Object value,
									Int64 defaultValue = default(Int64))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;

			Int64 result;
			return !Int64.TryParse(value.ToString(), out result) ? defaultValue : result;
		}

		/// <summary>
		/// Converts the object to a <see cref="SByte"/>.
		/// </summary>
		/// <param name="value">The value from the database.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns>The value converted to <see cref="SByte"/> or a default value.</returns>
		public static SByte AsSByte(this Object value,
									SByte defaultValue = default(SByte))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;

			SByte result;
			return !SByte.TryParse(value.ToString(), out result) ? defaultValue : result;
		}

		/// <summary>
		/// Converts the object to a <see cref="Single"/>.
		/// </summary>
		/// <param name="value">The value from the database.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns>The value converted to <see cref="Single"/> or a default value.</returns>
		public static Single AsSingle(this Object value,
									  Single defaultValue = default(Single))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;

			Single result;
			return !Single.TryParse(value.ToString(), out result) ? defaultValue : result;
		}

		/// <summary>
		/// Converts the object to a <see cref="String"/>.
		/// </summary>
		/// <param name="value">The value from the database.</param>
		/// <param name="defaultValue">A default value.</param>
		/// <returns>The value converted to <see cref="String"/> or a default value.</returns>
		public static String AsString(this Object value,
									  String defaultValue = default(String))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;
			return value.ToString();
		}

		/// <summary>
		/// Converts the object to a <see cref="UInt16"/>.
		/// </summary>
		/// <param name="value">The value from the database.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns>The value converted to <see cref="UInt16"/> or a default value.</returns>
		public static UInt16 AsUInt16(this Object value,
									  UInt16 defaultValue = default(UInt16))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;

			UInt16 result;
			return !UInt16.TryParse(value.ToString(), out result) ? defaultValue : result;
		}

		/// <summary>
		/// Converts the object to a <see cref="UInt32"/>.
		/// </summary>
		/// <param name="value">The value from the database.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns>The value converted to <see cref="UInt32"/> or a default value.</returns>
		public static UInt32 AsUInt32(this Object value,
									  UInt32 defaultValue = default(UInt32))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;

			UInt32 result;
			return !UInt32.TryParse(value.ToString(), out result) ? defaultValue : result;
		}

		/// <summary>
		/// Converts the object to a <see cref="UInt64"/>.
		/// </summary>
		/// <param name="value">The value from the database.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns>The value converted to <see cref="UInt64"/> or a default value.</returns>
		public static UInt64 AsUInt64(this Object value,
									  UInt64 defaultValue = default(UInt64))
		{
			if (value == null || Convert.IsDBNull(value))
				return defaultValue;

			UInt64 result;
			return !UInt64.TryParse(value.ToString(), out result) ? defaultValue : result;
		}
	}
}
