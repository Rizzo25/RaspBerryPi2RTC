using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RaspBerryPi2RTC
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const string I2C_CONTROLLER_NAME = "I2C1";
        private const byte RTC_I2C_ADDRESS = 0x6f;
        private const byte RTC_IODIR_REGISTER_ADDRESS = 0x00;
        private const byte RTC_DAY_REGISTER_ADDRESS = 0x06;
        private const byte RTC_MONTH_REGISTER_ADDRESS = 0x05;
        private const byte RTC_YEAR_REGISTER_ADDRESS = 0x04;
        private const byte RTC_HOUR_REGISTER_ADDRESS = 0x02;
        private const byte RTC_MINUTE_REGISTER_ADDRESS = 0x01;
        private const byte RTC_SECOND_REGISTER_ADDRESS = 0x00;
        private byte RTC_LOCAL_DAY;
        private byte RTC_LOCAL_MONTH;
        private byte RTC_LOCAL_YEAR;
        private byte RTC_LOCAL_HOUR;
        private byte RTC_LOCAL_MINUTE;
        private byte RTC_LOCAL_SECOND;
        private I2cDevice i2cPIFACERTC;

        public MainPage()
        {
            this.InitializeComponent();
            //  Unloaded += MainPage_Unloaded;
            InitializeSystem();
        }
        private async void InitializeSystem()
        {
            // byte[] i2CWriteBuffer;
             byte[] i2CReadBuffer;
            // byte bitMask;
            try
            {
                var i2cSettings = new I2cConnectionSettings(RTC_I2C_ADDRESS);
                i2cSettings.BusSpeed = I2cBusSpeed.FastMode;
                string deviceSelector = I2cDevice.GetDeviceSelector(I2C_CONTROLLER_NAME);
                var i2cDeviceControllers = await DeviceInformation.FindAllAsync(deviceSelector);
                i2cPIFACERTC = await I2cDevice.FromIdAsync(i2cDeviceControllers[0].Id, i2cSettings);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: {0}", e.Message);
                return;
            }

            try
            {
                // initialize local copies of the IODIR, GPIO, and OLAT registers
                i2CReadBuffer = new byte[1];

                // read in each register value on register at a time (could do this all at once but
                // for example clarity purposes we do it this way)
                i2cPIFACERTC.WriteRead(new byte[] { RTC_IODIR_REGISTER_ADDRESS }, i2CReadBuffer);
                RTC_LOCAL_DAY = i2CReadBuffer[0];

                i2cPIFACERTC.WriteRead(new byte[] { RTC_IODIR_REGISTER_ADDRESS }, i2CReadBuffer);
                RTC_LOCAL_MONTH = i2CReadBuffer[0];

                i2cPIFACERTC.WriteRead(new byte[] { RTC_IODIR_REGISTER_ADDRESS }, i2CReadBuffer);
                RTC_LOCAL_YEAR = i2CReadBuffer[0];

                i2cPIFACERTC.WriteRead(new byte[] { RTC_IODIR_REGISTER_ADDRESS }, i2CReadBuffer);
                RTC_LOCAL_HOUR = i2CReadBuffer[0];

                i2cPIFACERTC.WriteRead(new byte[] { RTC_IODIR_REGISTER_ADDRESS }, i2CReadBuffer);
                RTC_LOCAL_MINUTE = i2CReadBuffer[0];

                i2cPIFACERTC.WriteRead(new byte[] { RTC_IODIR_REGISTER_ADDRESS }, i2CReadBuffer);
                RTC_LOCAL_SECOND = i2CReadBuffer[0];

                // configure the LED pin output to be logic high, leave the other pins as they are.
               // olatRegister |= LED_GPIO_PIN;
               // i2CWriteBuffer = new byte[] { PORT_EXPANDER_OLAT_REGISTER_ADDRESS, olatRegister };
               // i2cPortExpander.Write(i2CWriteBuffer);

                // configure only the LED pin to be an output and leave the other pins as they are.
                // input is logic low, output is logic high
               // bitMask = (byte)(0xFF ^ LED_GPIO_PIN); // set the LED GPIO pin mask bit to '0', all other bits to '1'
               // iodirRegister &= bitMask;
               // i2CWriteBuffer = new byte[] { PORT_EXPANDER_IODIR_REGISTER_ADDRESS, iodirRegister };
               // i2cPortExpander.Write(i2CWriteBuffer);

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: {0}", e.Message);
                return;
            }



            try
            {
                //TxtDay = str(RTC_LOCAL_DAY);

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: {0}", e.Message);
                return;
            }

        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
