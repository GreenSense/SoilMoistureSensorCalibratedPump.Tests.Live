using System;
using NUnit.Framework;
using System.IO;

namespace SoilMoistureSensorCalibratedPump.Tests.Live
{
	public abstract class BaseTestFixture
	{
		public BaseTestFixture()
		{
		}

		[SetUp]
		public void SetUp()
		{
		}

		[TearDown]
		public void TearDown()
		{
		}

		public void WriteTestHeading(string text)
		{
			Console.WriteLine("==========");
			Console.WriteLine(text);
			Console.WriteLine("==========");
		}

		public string GetDeviceName()
		{
			var deviceName = TestContext.Parameters["DeviceName"];

			if (String.IsNullOrEmpty(deviceName))
				deviceName = "irrigator1";

			return deviceName;
		}

		public string GetSecurityValue(string key, string environmentVariable)
		{
			Console.WriteLine("Retrieving security value: " + key);

			var value = Environment.GetEnvironmentVariable(environmentVariable);

			if (String.IsNullOrEmpty(value))
			{
				var projectDirectory = GetProjectDirectory();

				value = File.ReadAllText(Path.Combine(projectDirectory, key + ".security")).Trim();
			}

			return value;
		}

		public string GetProjectDirectory()
		{
			var currentDirectory = Environment.CurrentDirectory;
			var projectDirectory = currentDirectory;


			if (projectDirectory.EndsWith("/bin/Debug"))
				projectDirectory = projectDirectory.Replace("/bin/Debug", "");
			if (projectDirectory.EndsWith("/bin/Release"))
				projectDirectory = projectDirectory.Replace("/bin/Release", "");
			if (projectDirectory.EndsWith("/src"))
				projectDirectory = projectDirectory.Replace("/src", "");

			Console.WriteLine("Project directory:");
			Console.WriteLine(projectDirectory);

			return projectDirectory;
		}
	}
}

