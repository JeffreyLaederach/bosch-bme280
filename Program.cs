using System;
using System.Device.Gpio;
using System.Device.I2c;
using System.Threading;
using Iot.Device.Bmxx80;
using Iot.Device.Bmxx80.PowerMode;
using Iot.Device.CharacterLcd;
using Iot.Device.Pcx857x;

var sensorSettings = new I2cConnectionSettings(1, Bme280.DefaultI2cAddress);
using I2cDevice sensorI2cDevice = I2cDevice.Create(sensorSettings);
using var bme280 = new Bme280(sensorI2cDevice);

using I2cDevice gpioExpanderI2cDevice = I2cDevice.Create(new I2cConnectionSettings(1, 0x3f));
using var gpioExpander = new Pcf8574(gpioExpanderI2cDevice);
using var lcd = new Lcd1602(registerSelectPin: 0,
                        enablePin: 2,
                        dataPins: new int[] { 4, 5, 6, 7 },
                        backlightPin: 3,
                        backlightBrightness: 0.1f,
                        readWritePin: 1,
                        controller: new GpioController(PinNumberingScheme.Logical, gpioExpander));

int measurementTime = bme280.GetMeasurementDuration();

while (true)
{
    Console.Clear();
    lcd.Clear();

    bme280.SetPowerMode(Bmx280PowerMode.Forced);
    Thread.Sleep(measurementTime);

    bme280.TryReadTemperature(out var tempValue);
    bme280.TryReadPressure(out var preValue);
    bme280.TryReadHumidity(out var humValue);
    bme280.TryReadAltitude(out var altValue);

    Console.WriteLine($"Temperature: {tempValue.DegreesCelsius:0.##}\u00B0C");
    Console.WriteLine($"Pressure: {preValue.Hectopascals:#.##} hPa");
    Console.WriteLine($"Relative humidity: {humValue.Percent:#.##}%");
    Console.WriteLine($"Estimated altitude: {altValue.Meters:#} m");

    lcd.SetCursorPosition(0, 0);
    lcd.Write($"T:{tempValue.DegreesFahrenheit:0.#}F P:{preValue.InchesOfMercury:#.#}Hg");
    lcd.SetCursorPosition(0, 1);
    lcd.Write($"H:{humValue.Percent:#.#}% A:{altValue.Feet:#}ft");

    Thread.Sleep(1000);
}