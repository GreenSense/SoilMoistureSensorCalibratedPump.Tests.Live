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
	[TestFixture]
	public class ReadIntervalMqttTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_ReadInterval_1Second()
		{
			var deviceName = GetDeviceName();

			var mqttHost = GetSecurityValue("mqtt-host", "MQTT_HOST");
			var mqttUsername = GetSecurityValue("mqtt-username", "MQTT_USERNAME");
			var mqttPassword = GetSecurityValue("mqtt-password", "MQTT_PASSWORD");

			var helper = new ReadIntervalMqttTestHelper();

			helper.ReadInterval = 1;
			helper.MqttDeviceName = deviceName;
			helper.MqttHost = mqttHost;
			helper.MqttUsername = mqttUsername;
			helper.MqttPassword = mqttPassword;

			helper.Start();

			helper.RunReadIntervalTests();

			helper.End();
		}

		[Test]
		public void Test_ReadInterval_5Seconds()
		{
			var deviceName = GetDeviceName();

			var mqttHost = GetSecurityValue("mqtt-host", "MQTT_HOST");
			var mqttUsername = GetSecurityValue("mqtt-username", "MQTT_USERNAME");
			var mqttPassword = GetSecurityValue("mqtt-password", "MQTT_PASSWORD");

			var helper = new ReadIntervalMqttTestHelper();

			helper.ReadInterval = 5;
			helper.MqttDeviceName = deviceName;
			helper.MqttHost = mqttHost;
			helper.MqttUsername = mqttUsername;
			helper.MqttPassword = mqttPassword;

			helper.Start();

			helper.RunReadIntervalTests();

			helper.End();
		}
	}
}

