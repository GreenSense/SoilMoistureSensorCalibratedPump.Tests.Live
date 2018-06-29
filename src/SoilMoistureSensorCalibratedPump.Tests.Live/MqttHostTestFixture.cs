using System;
using System.Text;
using System.Threading;
using NUnit.Framework;
using NUnit.Framework.Internal;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace SoilMoistureSensorCalibratedPump.Tests.Live
{
	[TestFixture]
	public class MqttHostTestFixture : BaseTestFixture
	{
		public bool MessageReceived = false;

		[Test]
		public void Test_MqttHost_SendAndReceive()
		{
			var deviceName = GetDeviceName();

			var mqttHost = GetSecurityValue("mqtt-host", "MQTT_HOST");
			var mqttUsername = GetSecurityValue("mqtt-username", "MQTT_USERNAME");
			var mqttPassword = GetSecurityValue("mqtt-password", "MQTT_PASSWORD");

			Console.WriteLine("==========");
			Console.WriteLine("Testing MQTT server is working for live GreenSense project");
			Console.WriteLine("==========");

			Assert.IsNotEmpty(mqttHost, "MQTT_HOST environment variable is not set.");
			Assert.IsNotEmpty(mqttUsername, "MQTT_USERNAME environment variable is not set.");
			Assert.IsNotEmpty(mqttPassword, "MQTT_PASSWORD environment variable is not set.");

			Console.WriteLine("Host: " + mqttHost);
			Console.WriteLine("Username: " + mqttUsername);

			var mqttClient = new MqttClient(mqttHost);

			var clientId = Guid.NewGuid().ToString();

			mqttClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
			mqttClient.Connect(clientId, mqttUsername, mqttPassword);

			var subscribeTopic = "/CustomTest/Key";
			mqttClient.Subscribe(new string[] { subscribeTopic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

			mqttClient.Publish(subscribeTopic, Encoding.UTF8.GetBytes("TestValue"));

			Thread.Sleep(1000);


			Assert.IsTrue(MessageReceived);

			mqttClient.Disconnect();
		}

		public void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
		{
			var topic = e.Topic;

			var message = System.Text.Encoding.Default.GetString(e.Message);

			Console.WriteLine("Message received: " + message);

			Assert.AreEqual("TestValue", message);

			MessageReceived = true;
		}

	}
}

