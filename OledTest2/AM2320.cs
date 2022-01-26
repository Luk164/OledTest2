using System;
using System.Device.I2c;
using System.Diagnostics;
using System.Threading;

namespace OledTest2
{
    public struct AM2320Data
    {
        public double Temperature;
        public double Humidity;
    }

    public class AM2320 : IDisposable
    {
        private I2cDevice _sensor;

        public const int AM2320Addr = 0x5C;

        /// <summary>
        /// Initialize
        /// </summary>
        public void Initialize(I2cConnectionSettings settings)
        {
            settings ??= new I2cConnectionSettings(1, AM2320Addr, I2cBusSpeed.StandardMode);

            _sensor = I2cDevice.Create(settings);
        }

        public AM2320Data ReadTempHum()
        {
            var readBuf = new byte[8];

            //Ping required to wake up the sensor
            _sensor.WriteByte(0);
            Thread.Sleep(10);
            var test = _sensor.WriteByte(0);
            Thread.Sleep(10);

            var writeResult = _sensor.Write(new byte[] { 0x03, 0x00, 0x04 });
            Thread.Sleep(2);
            var readResult = _sensor.Read(readBuf);

            double rawH = (readBuf[2] << 8) + readBuf[3];
            double rawT = (readBuf[4] << 8) + readBuf[5];

            // Debug.WriteLine($"Ping result: {test.Status}");
            // Debug.WriteLine($"Write: {writeResult.Status}");
            // Debug.WriteLine($"ReadTempHum: {readResult.Status}");

            var data = new AM2320Data
            {
                Temperature = rawT / 10.0,
                Humidity = rawH / 10.0
            };

            return data;
        }

        public double ReadTemp()
        {
            var readBuf = new byte[6];

            //Ping required to wake up the sensor
            var test = _sensor.WriteByte(0);
            Thread.Sleep(10);

            var writeResult = _sensor.Write(new byte[] { 0x03, 0x02, 0x02 });
            Thread.Sleep(2);
            var readResult = _sensor.Read(readBuf);
            double temp = ((readBuf[2] << 8) + readBuf[3]) / 10.0;

            // Debug.WriteLine($"Ping result: {test.Status}");
            // Debug.WriteLine($"Write: {writeResult.Status}");
            // Debug.WriteLine($"ReadTempHum: {readResult.Status}");

            return temp;
        }

        public I2cDevice GetDevice()
        {
            return _sensor;
        }

        public void Dispose()
        {
            _sensor.Dispose();
        }
    }
}