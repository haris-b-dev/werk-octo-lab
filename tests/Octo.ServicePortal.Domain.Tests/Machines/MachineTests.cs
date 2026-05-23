using Octo.ServicePortal.Domain.Machines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octo.ServicePortal.Domain.Tests.Machines
{
	public class MachineTests
	{
		[Test]
		public void Constructor_ShouldCreateMachine_WhenDataIsValid()
		{
			var modelName = "Model X";
			var serialNumber = "GG123456";
			var companyCustomerId = 1;

			var machine = new Machine(
				modelName,
				serialNumber,
				companyCustomerId
			);

			Assert.That(machine.ModelName, Is.EqualTo(modelName));
			Assert.That(machine.SerialNumber, Is.EqualTo(serialNumber));
			Assert.That(machine.CompanyCustomerId, Is.EqualTo(companyCustomerId));
			Assert.That(machine.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(10).Seconds);

			Assert.That(machine.ServiceRequests, Is.Empty);
		}

		[Test]
		public void Constructor_ShouldThrowArgumentException_WhenModelNameIsEmpty()
		{
			var modelName = "";
			var serialNumber = "GG123456";
			var companyCustomerId = 1;

			var exception = Assert.Throws<ArgumentException>(() =>
				new Machine(
					modelName,
					serialNumber,
					companyCustomerId
				));

			Assert.That(exception!.ParamName, Is.EqualTo("modelName"));
		}

		[Test]
		public void Constructor_ShouldThrowArgumentException_WhenSerialNumberIsEmpty()
		{
			var modelName = "Model X";
			var serialNumber = "";
			var companyCustomerId = 1;

			var exception = Assert.Throws<ArgumentException>(() =>
				new Machine(
					modelName,
					serialNumber,
					companyCustomerId
				));

			Assert.That(exception!.ParamName, Is.EqualTo("serialNumber"));
		}

		[Test]
		public void Constructor_ShouldThrowArgumentException_WhenCompanyCustomerIdIsInvalid()
		{
			var modelName = "Model X";
			var serialNumber = "GG123456";
			var companyCustomerId = 0;

			var exception = Assert.Throws<ArgumentException>(() =>
				new Machine(
					modelName,
					serialNumber,
					companyCustomerId
				));

			Assert.That(exception!.ParamName, Is.EqualTo("companyCustomerId"));
		}

		[Test]
		public void UpdateModelName_ShouldUpdateModelName_WhenDataIsValid()
		{
			var modelName = "Model X";
			var serialNumber = "GG123456";
			var companyCustomerId = 1;


			var machine = new Machine(
				modelName,
				serialNumber,
				companyCustomerId
			);

			var newModelName = "Model A";
			machine.UpdateModelName(newModelName);

			Assert.That(machine.ModelName, Is.EqualTo(newModelName));
		}

		[Test]
		public void UpdateModelName_ShouldThrowThrowArgumentException_WhenModelNameIsEmpty()
		{
			var modelName = "Model X";
			var serialNumber = "GG123456";
			var companyCustomerId = 1;

			var machine = new Machine(
				modelName,
				serialNumber,
				companyCustomerId
			);

			var newModelName = "";


			var exception = Assert.Throws<ArgumentException>(() =>
				machine.UpdateModelName(newModelName));

			Assert.That(exception!.ParamName, Is.EqualTo("modelName"));
		}
	}
}
