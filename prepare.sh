echo "Preparing for project"
echo "Dir: $PWD"

export DEBIAN_FRONTEND=noninteractive

sudo apt-get update
  
if ! type "msbuild" > /dev/null; then
  VERSION_NAME=$(lsb_release -cs)

  apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF && \
  echo "deb https://download.mono-project.com/repo/ubuntu stable-$VERSION_NAME main" | tee /etc/apt/sources.list.d/mono-official-stable.list && \
  apt-get update -qq && \
  apt-get install -y mono-complete ca-certificates-mono msbuild
else
  echo "Mono/msbuild is already installed. Skipping install."
fi

if ! type "mosquitto_pub" > /dev/null; then
  sudo apt-get install -y --no-install-recommends mosquitto-clients
else
  echo "Mosquitto clients already installed. Skipping install."
fi

if ! type "git" > /dev/null; then
  sudo apt-get install -y --no-install-recommends git
else
  echo "git is already installed. Skipping install."
fi

if ! type "wget" > /dev/null; then
  sudo apt-get install -y --no-install-recommends wget
else
  echo "wget is already installed. Skipping install."
fi

