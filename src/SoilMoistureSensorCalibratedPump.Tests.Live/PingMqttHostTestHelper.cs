using System;
using NUnit.Framework;
using System.Net.NetworkInformation;

namespace SoilMoistureSensorCalibratedPump.Tests.Live
{
	public class PingMqttHostTestHelper
	{
		public string MqttHost;

		public PingMqttHostTestHelper(string mqttHost)
		{
			MqttHost = mqttHost;
		}

		public void TestPingMqttHost()
		{
			Console.WriteLine("==========");
			Console.WriteLine("Checking that the garden host is active");
			Console.WriteLine("==========");
			Console.WriteLine("Host: " + MqttHost);

			Assert.IsNotEmpty(MqttHost, "MQTT host is not set.");

			PingHost(MqttHost);
		}

		protected bool PingHost(string nameOrAddress)
		{
			bool pingable = false;
			Ping pinger = new Ping();
			try
			{
				PingReply reply = pinger.Send(nameOrAddress);
				pingable = reply.Status == IPStatus.Success;
			}
			catch (PingException ex)
			{
				Assert.Fail(ex.ToString());
			}
			return pingable;
		}
	}
}
