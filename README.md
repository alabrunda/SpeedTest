# SpeedTest
SpeedTest from RaspberryPi, Results pushed to ASPNET Core Server > MSSQL

SpeedTest.Client is an Ookla SpeedTest written in C# and cross-compiled to ARM64 Docker Image pushed to Docker Hub @ alabrunda/SpeedTest.Client for a RaspberryPi deployment.

SpeedTest.Client pushes results via HTTP Post to ASP.NET Core Imaged pushed to Docker Hub @ alabrunda/SpeedTest.Server.  On Recipt of HTTP Post ASP.NET will write file to MSSQL DB.  The MSSQL instance is part of Docker-Compose file and will create DB automatically.  

