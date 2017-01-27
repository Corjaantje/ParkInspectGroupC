using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkInspectGroupC.Validation;
using System.Globalization;

namespace ParkInspectGroupCTests
{
	[TestClass]
	public class EmailValidationTest
	{
		[TestMethod]
		public void TestValid0()
		{

			EmailValidation EV = new EmailValidation();
			var result = EV.Validate("email@example.com", new CultureInfo("en-GB"));

			Assert.AreEqual(true, result.IsValid, "email@example.com returns not valid");
		}

		[TestMethod]
		public void TestValid1()
		{

			EmailValidation EV = new EmailValidation();
			var result = EV.Validate("email@[123.123.123.123]", new CultureInfo("en-GB"));

			Assert.AreEqual(true, result.IsValid, "email@[123.123.123.123] returns not valid");
		}

		[TestMethod]
		public void TestValid2()
		{

			EmailValidation EV = new EmailValidation();
			var result = EV.Validate("1234567890@example.com", new CultureInfo("en-GB"));

			Assert.AreEqual(true, result.IsValid, "1234567890@example.com returns not valid");
		}

		[TestMethod]
		public void TestValid3()
		{

			EmailValidation EV = new EmailValidation();
			var result = EV.Validate("email@example-one.com", new CultureInfo("en-GB"));

			Assert.AreEqual(true, result.IsValid, "email@example-one.com returns not valid");
		}

		[TestMethod]
		public void TestValid4()
		{

			EmailValidation EV = new EmailValidation();
			var result = EV.Validate("firstname-lastname@example.com", new CultureInfo("en-GB"));

			Assert.AreEqual(true, result.IsValid, "firstname-lastname@example.com returns not valid");
		}

		[TestMethod]
		public void TestInvalid0()
		{

			EmailValidation EV = new EmailValidation();
			var result = EV.Validate("plainaddress", new CultureInfo("en-GB"));

			Assert.AreEqual(false, result.IsValid, "plainaddress returns valid");
		}

		[TestMethod]
		public void TestInvalid1()
		{

			EmailValidation EV = new EmailValidation();
			var result = EV.Validate("@example.com", new CultureInfo("en-GB"));

			Assert.AreEqual(false, result.IsValid, "@example.com returns valid");
		}

		[TestMethod]
		public void TestInvalid2()
		{

			EmailValidation EV = new EmailValidation();
			var result = EV.Validate("email@-example.com", new CultureInfo("en-GB"));

			Assert.AreEqual(false, result.IsValid, "email@-example.com returns valid");
		}

		[TestMethod]
		public void TestInvalid3()
		{

			EmailValidation EV = new EmailValidation();
			var result = EV.Validate("Joe Smith <email@example.com>", new CultureInfo("en-GB"));

			Assert.AreEqual(false, result.IsValid, "Joe Smith <email@example.com> returns valid");
		}

	}
}
