DEVICE_NAME=$1

if [ ! $DEVICE_NAME ]; then
  echo "Provide a device name as an argument."
  exit 1
fi

MQTT_HOST=$(cat mqtt-host.security)
MQTT_USERNAME=$(cat mqtt-username.security)
MQTT_PASSWORD=$(cat mqtt-password.security)

echo "Testing device: $DEVICE_NAME"

sh report-test-start.sh $DEVICE_NAME && \

mono lib/NUnit.ConsoleRunner.3.8.0/tools/nunit3-console.exe bin/Release/SoilMoistureSensorCalibratedSerial.Tests.Live.dll --params=DeviceName=$DEVICE_NAME && \
sh report-test-pass.sh $DEVICE_NAME || (sh report-test-fail.sh $DEVICE_NAME && exit 1)
