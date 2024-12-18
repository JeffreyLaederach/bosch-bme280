# Overview

This repository demonstrates reading environmental conditions from a Bosch BME280 sensor over the I2C bus of a Raspberry Pi while using SSH connection to display sensor data on the integrated PowerShell Terminal in Visual Studio Code while also displaying the climate readings on an external LCD. The units shown in the terminal are in Metric while the units displayed on the LCD are Imperial. The sensor records temperature, pressure, relative humidity, and estimated altitude. Note that the NuGet package "Iot.Device.Bindings" is required to interface with the GPIO pins of the microcontroller. A pinout diagram is shown below for reference.

## Raspberry Pi 4 Pinout Diagram:

![image](https://github.com/user-attachments/assets/86a6601d-1aaf-4216-962a-6ae12bc40492)

## Useful Links:

[Microsoft .NET IoT Read Environmental Conditions from a Sensor](https://learn.microsoft.com/en-us/dotnet/iot/tutorials/temp-sensor#prerequisites)

[Microsoft .NET IoT Display Text on an LCD](https://learn.microsoft.com/en-us/dotnet/iot/tutorials/lcd-display)
