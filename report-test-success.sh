DEVICE_NAME=$1

if [ ! $DEVICE_NAME ]; then
  echo "Provide a device name as an argument."
  exit 1
fi

MQTT_HOST=$(cat mqtt-host.security)
MQTT_USERNAME=$(cat mqtt-username.security)
MQTT_PASSWORD=$(cat mqtt-password.security)

echo "Reporting test success via MQTT"

mosquitto_pub -h $MQTT_HOST -t "/$DEVICE_NAME/StatusMessage" -u "$MQTT_USERNAME" -P "$MQTT_PASSWORD" -m "Passed" && \
mosquitto_pub -h $MQTT_HOST -t "/$DEVICE_NAME/Status" -u "$MQTT_USERNAME" -P "$MQTT_PASSWORD" -m "0" && \
mosquitto_pub -h $MQTT_HOST -t "/$DEVICE_NAME/Error" -u "$MQTT_USERNAME" -P "$MQTT_PASSWORD" -m ""
