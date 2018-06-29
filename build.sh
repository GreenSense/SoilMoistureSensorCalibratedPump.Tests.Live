echo "Starting build for project"
echo "Dir: $PWD"

DIR=$PWD

msbuild src/SoilMoistureSensorCalibratedPump.Tests.Live.sln /p:Configuration=Release
