echo "Getting libraries"
echo "Dir: $PWD"

NUGET_FILE="nuget.exe"

if [ ! -f "$NUGET_FILE" ];
then
    wget http://nuget.org/nuget.exe
fi

mono nuget.exe update -self

mono nuget.exe install nunit
mono nuget.exe install nunit.runners
mono nuget.exe install M2Mqtt
