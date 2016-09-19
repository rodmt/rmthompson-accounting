/***************************************************************************
**
**      Auth:   Rodrigues M. Thompson, Chief Software Architect
**
**      Name:   Database Extensions Test Fixture
**
**      Date:   2013 May 30
**
**      Desc:   Handles the tests for the database extensions class.
**
** Copyright © 2016 Zion Software Solutions, LLC. All Rights Reserved.
**
** Unpublished copyright. This material contains proprietary information
** that shall be used or copied only within Zion Software Solutions, 
** except with written permission of Zion Software Solutions.		
**              
*******************************************************************************
**  Change History
****************************************************************************
**
** $Author$
** $DateTime$
** $Change$
** $Revision$
** $HeadURL$
**
***************************************************************************/

using System;
using System.Globalization;
using NUnit.Framework;
using Zion.Solutions.Data;

namespace ZionSoftware.Data.Tests.NUnit
{
	[TestFixture]
	public class DatabaseExtensionsFixture
	{
		private readonly DBNull m_dbNull = DBNull.Value;
		private readonly Object m_nullObject = null;
		private readonly Object m_guidObject = "01234567-89AB-CDEF-0123-456789ABCDEF";

		#region AsBoolean Tests

		[Test]
		public void ADbNullObjectShouldReturnDefaultBoolean()
		{
			// Arrange
			const Boolean expectedValue = default(Boolean);

			// Act - Convert to Boolean.
			var actualValue = m_dbNull.AsBoolean();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ADbNullObjectShouldReturnGivenDefaultBoolean()
		{
			// Arrange
			const Boolean expectedValue = true;

			// Act - Convert to Boolean.
			var actualValue = m_dbNull.AsBoolean(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnDefaultBoolean()
		{
			// Arrange
			const Boolean expectedValue = default(Boolean);

			// Act - Convert to Boolean.
			var actualValue = m_nullObject.AsBoolean();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnGivenDefaultBoolean()
		{
			// Arrange
			const Boolean expectedValue = true;

			// Act - Convert to Boolean.
			var actualValue = m_nullObject.AsBoolean(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AnInvalidObjectShouldReturnDefaultBoolean()
		{
			// Arrange
			const Boolean expectedValue = default(Boolean);

			// Act - Convert to Boolean.
			var actualValue = "Invalid Object".AsBoolean();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AnInvalidObjectShouldReturnGivenDefaultBoolean()
		{
			// Arrange
			const Boolean expectedValue = true;

			// Act - Convert to Boolean.
			var actualValue = "Invalid Object".AsBoolean(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AValidBooleanObjectShouldReturnBoolean()
		{
			// Arrange
			Object booleanObject = true;
			var expectedValue = Boolean.Parse(booleanObject.ToString());

			// Act - Convert to Boolean.
			var actualValue = booleanObject.AsBoolean();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		#endregion

		#region AsByte Tests

		[Test]
		public void ADbNullObjectShouldReturnDefaultByte()
		{
			// Arrange
			const Byte expectedValue = default(Byte);

			// Act - Convert to Byte.
			var actualValue = m_dbNull.AsByte();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ADbNullObjectShouldReturnGivenDefaultByte()
		{
			// Arrange
			const Byte expectedValue = Byte.MaxValue - 1;

			// Act - Convert to Byte.
			var actualValue = m_dbNull.AsByte(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnDefaultByte()
		{
			// Arrange
			const Byte expectedValue = default(Byte);

			// Act - Convert to Byte.
			var actualValue = m_nullObject.AsByte();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnGivenDefaultByte()
		{
			// Arrange
			const Byte expectedValue = Byte.MaxValue - 1;

			// Act - Convert to Byte.
			var actualValue = m_nullObject.AsByte(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AnInvalidObjectShouldReturnDefaultByte()
		{
			// Arrange
			const Byte expectedValue = default(Byte);

			// Act - Convert to Byte.
			var actualValue = (Byte.MaxValue + 1).ToString(CultureInfo.InvariantCulture)
												 .AsByte();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AnInvalidObjectShouldReturnGivenDefaultByte()
		{
			// Arrange
			const Byte expectedValue = Byte.MaxValue - 1;

			// Act - Convert to Byte.
			var actualValue = (Byte.MaxValue + 1).ToString(CultureInfo.InvariantCulture)
												 .AsByte(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AValidByteObjectShouldReturnByte()
		{
			// Arrange
			Object byteObject = (Byte.MaxValue / 2);
			var expectedValue = Byte.Parse(byteObject.ToString());

			// Act - Convert to Byte.
			var actualValue = byteObject.AsByte();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		#endregion

		#region AsByteArray Tests

		[Test]
		public void ADbNullObjectShouldReturnDefaultByteArray()
		{
			// Arrange
			const Byte[] expectedValue = default(Byte[]);

			// Act - Convert to ByteArray.
			var actualValue = m_dbNull.AsBytes();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ADbNullObjectShouldReturnGivenDefaultByteArray()
		{
			// Arrange
			var expectedValue = new Byte[] { 0x1, 0x2, 0x3, 0x4, 0xAA, 0xAF };

			// Act - Convert to ByteArray.
			var actualValue = m_dbNull.AsBytes(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnDefaultByteArray()
		{
			// Arrange
			const Byte[] expectedValue = default(Byte[]);

			// Act - Convert to ByteArray.
			var actualValue = m_nullObject.AsBytes();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnGivenDefaultByteArray()
		{
			// Arrange
			var expectedValue = new Byte[] { 0x1, 0x2, 0x3, 0x4, 0xAA, 0xAF };

			// Act - Convert to ByteArray.
			var actualValue = m_nullObject.AsBytes(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		[Ignore("Need to refactor the test.")]
		public void AnInvalidObjectShouldReturnDefaultByteArray()
		{
			// Arrange
			const Byte[] expectedValue = default(Byte[]);

			// Act - Convert to ByteArray.
			var actualValue = (Byte.MaxValue + 1).ToString(CultureInfo.InvariantCulture)
												   .AsBytes();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		[Ignore("Need to refactor the test.")]
		public void AnInvalidObjectShouldReturnGivenDefaultByteArray()
		{
			// Arrange
			var expectedValue = new Byte[] { 0x1, 0x2, 0x3, 0x4, 0xAA, 0xAF };

			// Act - Convert to ByteArray.
			var actualValue = (Byte.MaxValue + 1).ToString(CultureInfo.InvariantCulture)
												   .AsBytes(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		[Ignore("Need to refactor the test.")]
		public void AValidByteArrayObjectShouldReturnByteArray()
		{
			// Arrange
			Object byteArrayObject = Int64.MaxValue;
			var expectedValue = BitConverter.GetBytes(Int64.MaxValue);

			// Act - Convert to ByteArray.
			var actualValue = byteArrayObject.AsBytes();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		#endregion

		#region AsChar Tests

		[Test]
		public void ADbNullObjectShouldReturnDefaultChar()
		{
			// Arrange
			const Char expectedValue = default(Char);

			// Act - Convert to Char.
			var actualValue = m_dbNull.AsChar();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ADbNullObjectShouldReturnGivenDefaultChar()
		{
			// Arrange
			const Char expectedValue = 'A';

			// Act - Convert to Char.
			var actualValue = m_dbNull.AsChar(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnDefaultChar()
		{
			// Arrange
			const Char expectedValue = default(Char);

			// Act - Convert to Char.
			var actualValue = m_nullObject.AsChar();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnGivenDefaultChar()
		{
			// Arrange
			const Char expectedValue = 'A';

			// Act - Convert to Char.
			var actualValue = m_nullObject.AsChar(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AnInvalidObjectShouldReturnDefaultChar()
		{
			// Arrange
			const Char expectedValue = default(Char);

			// Act - Convert to Char.
			var actualValue = (Char.MaxValue + 1).ToString(CultureInfo.InvariantCulture)
												   .AsChar();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AnInvalidObjectShouldReturnGivenDefaultChar()
		{
			// Arrange
			const Char expectedValue = 'A';

			// Act - Convert to Char.
			var actualValue = (Char.MaxValue + 1).ToString(CultureInfo.InvariantCulture)
												   .AsChar(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AValidCharObjectShouldReturnChar()
		{
			// Arrange
			Object charObject = Char.MaxValue;
			var expectedValue = Char.Parse(charObject.ToString());

			// Act - Convert to Char.
			var actualValue = charObject.AsChar();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		#endregion

		#region AsDateTime Tests

		[Test]
		public void ADbNullObjectShouldReturnDefaultDateTime()
		{
			// Arrange
			var expectedValue = default(DateTime);

			// Act - Convert to DateTime.
			var actualValue = m_dbNull.AsDateTime();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ADbNullObjectShouldReturnGivenDefaultDateTime()
		{
			// Arrange
			var expectedValue = DateTime.UtcNow;

			// Act - Convert to DateTime.
			var actualValue = m_dbNull.AsDateTime(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnDefaultDateTime()
		{
			// Arrange
			var expectedValue = default(DateTime);

			// Act - Convert to DateTime.
			var actualValue = m_nullObject.AsDateTime();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnGivenDefaultDateTime()
		{
			// Arrange
			var expectedValue = DateTime.UtcNow;

			// Act - Convert to DateTime.
			var actualValue = m_nullObject.AsDateTime(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AnInvalidObjectShouldReturnDefaultDateTime()
		{
			// Arrange
			var expectedValue = default(DateTime);

			// Act - Convert to DateTime.
			var actualValue = "Invalid Object".AsDateTime();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AnInvalidObjectShouldReturnGivenDefaultDateTime()
		{
			// Arrange
			var expectedValue = DateTime.UtcNow;

			// Act - Convert to DateTime.
			var actualValue = "Invalid Object".AsDateTime(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AValidDateTimeObjectShouldReturnDateTime()
		{
			// Arrange
			Object dateTimeObject = new DateTime(1976, 7, 14);
			var expectedValue = Convert.ToDateTime(dateTimeObject);

			// Act - Convert to DateTime.
			var actualValue = dateTimeObject.AsDateTime();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		#endregion

		#region AsDecimal Tests

		[Test]
		public void ADbNullObjectShouldReturnDefaultDecimal()
		{
			// Arrange
			const Decimal expectedValue = default(Decimal);

			// Act - Convert to Decimal.
			var actualValue = m_dbNull.AsDecimal();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ADbNullObjectShouldReturnGivenDefaultDecimal()
		{
			// Arrange
			var expectedValue = new Decimal(7.2398);

			// Act - Convert to Decimal.
			var actualValue = m_dbNull.AsDecimal(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnDefaultDecimal()
		{
			// Arrange
			const Decimal expectedValue = default(Decimal);

			// Act - Convert to Decimal.
			var actualValue = m_nullObject.AsDecimal();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnGivenDefaultDecimal()
		{
			// Arrange
			var expectedValue = new Decimal(7.2398);

			// Act - Convert to Decimal.
			var actualValue = m_nullObject.AsDecimal(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AnInvalidObjectShouldReturnDefaultDecimal()
		{
			// Arrange
			const Decimal expectedValue = default(Decimal);

			// Act - Convert to Decimal.
			var actualValue = "Invalid Object".AsDecimal();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AnInvalidObjectShouldReturnGivenDefaultDecimal()
		{
			// Arrange
			var expectedValue = new Decimal(7.2398);

			// Act - Convert to Decimal.
			var actualValue = "Invalid Object".AsDecimal(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AValidDecimalObjectShouldReturnDecimal()
		{
			// Arrange
			Object decimalObject = (Decimal.MaxValue / 2);
			var expectedValue = Decimal.Parse(decimalObject.ToString());

			// Act - Convert to Decimal.
			var actualValue = decimalObject.AsDecimal();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		#endregion

		#region AsGuid Tests

		[Test]
		public void ADbNullObjectShouldReturnDefaultGuid()
		{
			// Arrange
			var expectedValue = default(Guid);

			// Act - Convert to GUID.
			var actualValue = m_dbNull.AsGuid();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ADbNullObjectShouldReturnGivenDefaultGuid()
		{
			// Arrange
			var expectedValue = new Guid();

			// Act - Convert to GUID.
			var actualValue = m_dbNull.AsGuid(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnDefaultGuid()
		{
			// Arrange
			var expectedValue = default(Guid);

			// Act - Convert to GUID.
			var actualValue = m_nullObject.AsGuid();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnGivenDefaultGuid()
		{
			// Arrange
			var expectedValue = new Guid();

			// Act - Convert to GUID.
			var actualValue = m_nullObject.AsGuid(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AnInvalidObjectShouldReturnDefaultGuid()
		{
			// Arrange
			var expectedValue = default(Guid);

			// Act - Convert to GUID.
			var actualValue = "01234567-89AB-CDEF-0123-ZZZ789ABCDEF".AsGuid();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AnInvalidObjectShouldReturnGivenDefaultGuid()
		{
			// Arrange
			var expectedValue = new Guid();

			// Act - Convert to GUID.
			var actualValue = "01234567-89AB-CDEF-0123-ZZZ789ABCDEF".AsGuid(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AValidGuidObjectShouldReturnGuid()
		{
			// Arrange
			Object guidObject = "01234567-89AB-CDEF-0123-456789ABCDEF";
			var expectedValue = new Guid(guidObject.ToString());

			// Act - Convert to GUID.
			var actualValue = guidObject.AsGuid();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		#endregion

		#region AsInt16 Tests

		[Test]
		public void ADbNullObjectShouldReturnDefaultInt16()
		{
			// Arrange
			const Int16 expectedValue = default(Int16);

			// Act - Convert to Int16.
			var actualValue = m_dbNull.AsInt16();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ADbNullObjectShouldReturnGivenDefaultInt16()
		{
			// Arrange
			const Int16 expectedValue = Int16.MaxValue;

			// Act - Convert to Int16.
			var actualValue = m_dbNull.AsInt16(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnDefaultInt16()
		{
			// Arrange
			const Int16 expectedValue = default(Int16);

			// Act - Convert to Int16.
			var actualValue = m_nullObject.AsInt16();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnGivenDefaultInt16()
		{
			// Arrange
			const Int16 expectedValue = Int16.MaxValue;

			// Act - Convert to Int16.
			var actualValue = m_nullObject.AsInt16(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AnInvalidObjectShouldReturnDefaultInt16()
		{
			// Arrange
			const Int16 expectedValue = default(Int16);

			// Act - Convert to Int16.
			var actualValue = "Invalid Object".AsInt16();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AnInvalidObjectShouldReturnGivenDefaultInt16()
		{
			// Arrange
			const Int16 expectedValue = Int16.MaxValue;

			// Act - Convert to Int16.
			var actualValue = "Invalid Object".AsInt16(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AValidInt16ObjectShouldReturnInt16()
		{
			// Arrange
			const Int16 expectedValue = Int16.MaxValue;

			// Act - Convert to Int16.
			var actualValue = Int16.MaxValue.ToString(CultureInfo.InvariantCulture).AsInt16();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		#endregion

		#region AsInt32 Tests

		[Test]
		public void ADbNullObjectShouldReturnDefaultInt32()
		{
			// Arrange
			const Int32 expectedValue = default(Int32);

			// Act - Convert to Int32.
			var actualValue = m_dbNull.AsInt32();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ADbNullObjectShouldReturnGivenDefaultInt32()
		{
			// Arrange
			const Int32 expectedValue = Int32.MaxValue;

			// Act - Convert to Int32.
			var actualValue = m_dbNull.AsInt32(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnDefaultInt32()
		{
			// Arrange
			const Int32 expectedValue = default(Int32);

			// Act - Convert to Int32.
			var actualValue = m_nullObject.AsInt32();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnGivenDefaultInt32()
		{
			// Arrange
			const Int32 expectedValue = Int32.MaxValue;

			// Act - Convert to Int32.
			var actualValue = m_nullObject.AsInt32(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AnInvalidObjectShouldReturnDefaultInt32()
		{
			// Arrange
			const Int32 expectedValue = default(Int32);

			// Act - Convert to Int32.
			var actualValue = "Invalid Object".AsInt32();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AnInvalidObjectShouldReturnGivenDefaultInt32()
		{
			// Arrange
			const Int32 expectedValue = Int32.MaxValue;

			// Act - Convert to Int32.
			var actualValue = "Invalid Object".AsInt32(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AValidInt32ObjectShouldReturnInt32()
		{
			// Arrange
			const Int32 expectedValue = Int32.MaxValue;

			// Act - Convert to Int32.
			var actualValue = Int32.MaxValue.ToString(CultureInfo.InvariantCulture).AsInt32();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		#endregion

		#region AsInt64 Tests

		[Test]
		public void ADbNullObjectShouldReturnDefaultInt64()
		{
			// Arrange
			const Int64 expectedValue = default(Int64);

			// Act - Convert to Int64.
			var actualValue = m_dbNull.AsInt64();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ADbNullObjectShouldReturnGivenDefaultInt64()
		{
			// Arrange
			const Int64 expectedValue = Int64.MaxValue;

			// Act - Convert to Int64.
			var actualValue = m_dbNull.AsInt64(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnDefaultInt64()
		{
			// Arrange
			const Int64 expectedValue = default(Int64);

			// Act - Convert to Int64.
			var actualValue = m_nullObject.AsInt64();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnGivenDefaultInt64()
		{
			// Arrange
			const Int64 expectedValue = Int64.MaxValue;

			// Act - Convert to Int64.
			var actualValue = m_nullObject.AsInt64(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AnInvalidObjectShouldReturnDefaultInt64()
		{
			// Arrange
			const Int64 expectedValue = default(Int64);

			// Act - Convert to Int64.
			var actualValue = "Invalid Object".AsInt64();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AnInvalidObjectShouldReturnGivenDefaultInt64()
		{
			// Arrange
			const Int64 expectedValue = Int64.MaxValue;

			// Act - Convert to Int64.
			var actualValue = "Invalid Object".AsInt64(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AValidInt64ObjectShouldReturnInt64()
		{
			// Arrange
			const Int64 expectedValue = Int64.MaxValue;

			// Act - Convert to Int64.
			var actualValue = Int64.MaxValue.ToString(CultureInfo.InvariantCulture).AsInt64();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		#endregion

		#region AsSByte Tests

		[Test]
		public void ADbNullObjectShouldReturnDefaultSByte()
		{
			// Arrange
			const SByte expectedValue = default(SByte);

			// Act - Convert to SByte.
			var actualValue = m_dbNull.AsSByte();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ADbNullObjectShouldReturnGivenDefaultSByte()
		{
			// Arrange
			const SByte expectedValue = SByte.MaxValue - 1;

			// Act - Convert to SByte.
			var actualValue = m_dbNull.AsSByte(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnDefaultSByte()
		{
			// Arrange
			const SByte expectedValue = default(SByte);

			// Act - Convert to SByte.
			var actualValue = m_nullObject.AsSByte();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnGivenDefaultSByte()
		{
			// Arrange
			const SByte expectedValue = SByte.MaxValue - 1;

			// Act - Convert to SByte.
			var actualValue = m_nullObject.AsSByte(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AnInvalidObjectShouldReturnDefaultSByte()
		{
			// Arrange
			const SByte expectedValue = default(SByte);

			// Act - Convert to SByte.
			var actualValue = (SByte.MaxValue + 1).ToString(CultureInfo.InvariantCulture)
													.AsSByte();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AnInvalidObjectShouldReturnGivenDefaultSByte()
		{
			// Arrange
			const SByte expectedValue = SByte.MaxValue - 1;

			// Act - Convert to SByte.
			var actualValue = (SByte.MaxValue + 1).ToString(CultureInfo.InvariantCulture)
													.AsSByte(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AValidSByteObjectShouldReturnSByte()
		{
			// Arrange
			Object sbyteObject = (SByte.MaxValue / 2);
			var expectedValue = SByte.Parse(sbyteObject.ToString());

			// Act - Convert to SByte.
			var actualValue = sbyteObject.AsSByte();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		#endregion

		#region AsSingle Tests

		[Test]
		public void ADbNullObjectShouldReturnDefaultSingle()
		{
			// Arrange
			const Single expectedValue = default(Single);

			// Act - Convert to Single.
			var actualValue = m_dbNull.AsSingle();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ADbNullObjectShouldReturnGivenDefaultSingle()
		{
			// Arrange
			var expectedValue = Convert.ToSingle(7.2398);

			// Act - Convert to Single.
			var actualValue = m_dbNull.AsSingle(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnDefaultSingle()
		{
			// Arrange
			const Single expectedValue = default(Single);

			// Act - Convert to Single.
			var actualValue = m_nullObject.AsSingle();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnGivenDefaultSingle()
		{
			// Arrange
			var expectedValue = Convert.ToSingle(7.2398);

			// Act - Convert to Single.
			var actualValue = m_nullObject.AsSingle(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AnInvalidObjectShouldReturnDefaultSingle()
		{
			// Arrange
			const Single expectedValue = default(Single);

			// Act - Convert to Single.
			var actualValue = "Invalid Object".AsSingle();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AnInvalidObjectShouldReturnGivenDefaultSingle()
		{
			// Arrange
			var expectedValue = Convert.ToSingle(7.2398);

			// Act - Convert to Single.
			var actualValue = "Invalid Object".AsSingle(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AValidSingleObjectShouldReturnSingle()
		{
			// Arrange
			Object singleObject = Single.Epsilon;
			var expectedValue = Single.Parse(singleObject.ToString());

			// Act - Convert to Single.
			var actualValue = singleObject.AsSingle();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		#endregion

		#region AsString Tests

		[Test]
		public void ADbNullObjectShouldReturnDefaultString()
		{
			// Arrange
			const String expectedValue = default(String);

			// Act - Convert to String.
			var actualValue = DatabaseExtensions.AsString(m_dbNull);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ADbNullObjectShouldReturnGivenDefaultString()
		{
			// Arrange
			const String expectedValue = "Given Default String";

			// Act - Convert to String.
			var actualValue = m_dbNull.AsString(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnDefaultString()
		{
			// Arrange
			const String expectedValue = default(String);

			// Act - Convert to String.
			var actualValue = DatabaseExtensions.AsString(m_nullObject);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnGivenDefaultString()
		{
			// Arrange
			const String expectedValue = "Given Default String";

			// Act - Convert to String.
			var actualValue = m_nullObject.AsString(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		#endregion

		#region AsUInt16 Tests

		[Test]
		public void ADbNullObjectShouldReturnDefaultUInt16()
		{
			// Arrange
			const UInt16 expectedValue = default(UInt16);

			// Act - Convert to UInt16.
			var actualValue = m_dbNull.AsUInt16();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ADbNullObjectShouldReturnGivenDefaultUInt16()
		{
			// Arrange
			const UInt16 expectedValue = UInt16.MaxValue;

			// Act - Convert to UInt16.
			var actualValue = m_dbNull.AsUInt16(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnDefaultUInt16()
		{
			// Arrange
			const UInt16 expectedValue = default(UInt16);

			// Act - Convert to UInt16.
			var actualValue = m_nullObject.AsUInt16();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnGivenDefaultUInt16()
		{
			// Arrange
			const UInt16 expectedValue = UInt16.MaxValue;

			// Act - Convert to UInt16.
			var actualValue = m_nullObject.AsUInt16(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AnInvalidObjectShouldReturnDefaultUInt16()
		{
			// Arrange
			const UInt16 expectedValue = default(UInt16);

			// Act - Convert to UInt16.
			var actualValue = "Invalid Object".AsUInt16();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AnInvalidObjectShouldReturnGivenDefaultUInt16()
		{
			// Arrange
			const UInt16 expectedValue = UInt16.MaxValue;

			// Act - Convert to UInt16.
			var actualValue = "Invalid Object".AsUInt16(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AValidUInt16ObjectShouldReturnUInt16()
		{
			// Arrange
			const UInt16 expectedValue = UInt16.MaxValue;

			// Act - Convert to UInt16.
			var actualValue = UInt16.MaxValue.ToString(CultureInfo.InvariantCulture).AsUInt16();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		#endregion

		#region AsUInt32 Tests

		[Test]
		public void ADbNullObjectShouldReturnDefaultUInt32()
		{
			// Arrange
			const UInt32 expectedValue = default(UInt32);

			// Act - Convert to UInt32.
			var actualValue = m_dbNull.AsUInt32();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ADbNullObjectShouldReturnGivenDefaultUInt32()
		{
			// Arrange
			const UInt32 expectedValue = UInt32.MaxValue;

			// Act - Convert to UInt32.
			var actualValue = m_dbNull.AsUInt32(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnDefaultUInt32()
		{
			// Arrange
			const UInt32 expectedValue = default(UInt32);

			// Act - Convert to UInt32.
			var actualValue = m_nullObject.AsUInt32();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnGivenDefaultUInt32()
		{
			// Arrange
			const UInt32 expectedValue = UInt32.MaxValue;

			// Act - Convert to UInt32.
			var actualValue = m_nullObject.AsUInt32(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AnInvalidObjectShouldReturnDefaultUInt32()
		{
			// Arrange
			const UInt32 expectedValue = default(UInt32);

			// Act - Convert to UInt32.
			var actualValue = "Invalid Object".AsUInt32();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AnInvalidObjectShouldReturnGivenDefaultUInt32()
		{
			// Arrange
			const UInt32 expectedValue = UInt32.MaxValue;

			// Act - Convert to UInt32.
			var actualValue = "Invalid Object".AsUInt32(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AValidUInt32ObjectShouldReturnUInt32()
		{
			// Arrange
			const UInt32 expectedValue = UInt32.MaxValue;

			// Act - Convert to UInt32.
			var actualValue = UInt32.MaxValue.ToString(CultureInfo.InvariantCulture).AsUInt32();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		#endregion

		#region AsUInt64 Tests

		[Test]
		public void ADbNullObjectShouldReturnDefaultUInt64()
		{
			// Arrange
			const UInt64 expectedValue = default(UInt64);

			// Act - Convert to UInt64.
			var actualValue = m_dbNull.AsUInt64();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ADbNullObjectShouldReturnGivenDefaultUInt64()
		{
			// Arrange
			const UInt64 expectedValue = UInt64.MaxValue;

			// Act - Convert to UInt64.
			var actualValue = m_dbNull.AsUInt64(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnDefaultUInt64()
		{
			// Arrange
			const UInt64 expectedValue = default(UInt64);

			// Act - Convert to UInt64.
			var actualValue = m_nullObject.AsUInt64();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void ANullObjectShouldReturnGivenDefaultUInt64()
		{
			// Arrange
			const UInt64 expectedValue = UInt64.MaxValue;

			// Act - Convert to UInt64.
			var actualValue = m_nullObject.AsUInt64(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AnInvalidObjectShouldReturnDefaultUInt64()
		{
			// Arrange
			const UInt64 expectedValue = default(UInt64);

			// Act - Convert to UInt64.
			var actualValue = "Invalid Object".AsUInt64();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AnInvalidObjectShouldReturnGivenDefaultUInt64()
		{
			// Arrange
			const UInt64 expectedValue = UInt64.MaxValue;

			// Act - Convert to UInt64.
			var actualValue = "Invalid Object".AsUInt64(expectedValue);

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		[Test]
		public void AValidUInt64ObjectShouldReturnUInt64()
		{
			// Arrange
			const UInt64 expectedValue = UInt64.MaxValue;

			// Act - Convert to UInt64.
			var actualValue = UInt64.MaxValue.ToString(CultureInfo.InvariantCulture).AsUInt64();

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

		#endregion
	}
}
