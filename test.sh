echo "Testing project"
echo "  Dir: $PWD"

DEVICE_NAMES_LIST=$IRRIGATOR_DEVICE_NAMES

if [ ! $DEVICE_NAMES_LIST ]; then
  echo "Getting device names from file..."
  DEVICE_NAMES_LIST=$(cat mqtt-device-names.security)
fi

echo "Device names list: \"$DEVICE_NAMES_LIST\""

EXIT_CODE=0

for i in $(echo $DEVICE_NAMES_LIST | sed "s/,/ /g")
do
    sh test-device.sh $i || EXIT_CODE=1
done

exit $EXIT_CODE
