# SpeedTest
The purpose of this project is to install RPi's in the field and receive performance telemetry on a centralized server.  Code written in C# Core, SpeedTest.Client on RPi w/ Docker and SpeedTest.Server on Ubuntu 18.04.4 LTS (Bionic Beaver) w/ Docker

SpeedTest.Client is an Ookla SpeedTest written in C# and cross-compiled to ARM64 Docker Image pushed to Docker Hub @ https://hub.docker.com/repository/docker/alabrunda/speedtest.client.arm for a RaspberryPi deployment.  After speedtest, SpeedTest.Client pushes results via HTTP Post to SpeedTest.Server instance written in ASP.NET Core and deployed to Docker Hub @ https://hub.docker.com/repository/docker/alabrunda/speedtest.server.webapi.  

SpeedTest.Server receives HTTP Post speed infromation and will write file to MSSQL DB.  The MSSQL instance is part of Docker-Compose file and will create DB automatically.  

SpeedTesting  code from https://github.com/JoyMoe/SpeedTest.Net
