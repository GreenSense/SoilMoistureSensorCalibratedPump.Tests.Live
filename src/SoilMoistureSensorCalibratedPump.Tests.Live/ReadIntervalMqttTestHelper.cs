using System;
using NUnit.Framework;
using System.IO;
using System.Net.NetworkInformation;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Text;
using System.Threading;
using System.Collections.Generic;

namespace SoilMoistureSensorCalibratedPump.Tests.Live
{
	public class ReadIntervalMqttTestHelper : MqttTestHelper
	{
		public int ReadInterval = 1;

		public void RunReadIntervalTests()
		{

			try
			{
				Console.WriteLine("Testing read interval change via MQTT...");
				Console.WriteLine("Device name: " + MqttDeviceName);

				Console.WriteLine("Sending a command to force an output...");

				SendCommand("F", 1);

				Console.WriteLine("Waiting for the first data line to know what existing settings are...");

				WaitForData(1);

				Console.WriteLine("Checking MQTT data...");

				Assert.AreEqual(1, Data.Count, "No data found.");

				var dataEntry = Data[Data.Count - 1];

				Assert.IsTrue(dataEntry.ContainsKey("V"));

				var existingInterval = dataEntry["V"];

				Console.WriteLine("Existing reading interval: " + existingInterval);

				ExecuteReadIntervalTest(ReadInterval);

				Console.WriteLine("");
				Console.WriteLine("Restoring original reading interval: " + existingInterval);

				SendCommand("V", existingInterval);

				Thread.Sleep(1000);
			}
			catch (Exception ex)
			{
				PublishError(ex.Message);

				throw ex;
			}
		}

		protected void ExecuteReadIntervalTest(int interval)
		{
			Console.WriteLine("----------");
			Console.WriteLine("Running read interval test...");
			Console.WriteLine("Interval: " + interval);

			Console.WriteLine("");
			Console.WriteLine("Setting read interval to " + interval);

			SendCommand("V", interval);

			Thread.Sleep(1000);

			WaitForData(3);

			Console.WriteLine("");
			Console.WriteLine("Checking entry times...");

			CheckDataEntryTimes(interval);

			Console.WriteLine("");
			Console.WriteLine("Checking read interval value");

			Assert.AreEqual(interval.ToString(), Data[Data.Count - 1]["V"], "Invalid read interval value");
		}
	}
}