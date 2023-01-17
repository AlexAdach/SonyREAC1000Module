using Crestron.SimplSharp;
using System.Collections.Generic;

namespace SonyREAC1000Module
{
    public delegate void HttpResponseStatusHandler(ushort status);
    public delegate void DevicePropertyChangedHandler(string propName, string propValue);
    public delegate void CrestronDevicePropertiesHandler(SimplSharpString key, SimplSharpString value);
    public delegate void CrestronHttpResponseStatusHandler(ushort status);
    public class MainClass
    {
        private ICallManager callManager;
        private IDeviceManager deviceManager;
        public static CrestronDevicePropertiesHandler MyCrestronDevicePropertiesHandler { get; set; }
        public static CrestronHttpResponseStatusHandler MyCrestronHttpResponseStatusHandler { get; set; }
        public MainClass() { }
        public int Initialize(string ip, string username, string password)
        {
            //Creates the device manager instance
            deviceManager = new DeviceManager();

            //subscribes to all the property change events in device manager
            SetPropertyEventSubscriptions();

            //Creates the call manager
            callManager = new CallManager(ip, username, password, deviceManager);
            callManager.MyHttpResponseStatusHandler = HttpResponseStatusChanged;
            return 1;
        }


        public void GetStatus()
        {
            callManager.GetALLDeviceStatus();
        }

        public ushort UpdatePTZProperty(string key, string value)
        {
            string updateString = key + "=" + value;    
            CrestronConsole.PrintLine(updateString);
            return callManager.SendPTZCommand(updateString).Result;
        }

        public ushort StartPTZ()
        {
            return callManager.StartPtz().Result;
        }

        public ushort StopPTZ()
        {
            return callManager.StopPtz().Result;
        }


        //Subscribes "NotifyDevicePropertyChanged" to every device property in the Device Manager
        private void SetPropertyEventSubscriptions()
        {
            foreach (KeyValuePair<string, DeviceProperty> prop in deviceManager.DeviceProperties)
            {
                prop.Value.DevicePropertyChanged += NotifyDevicePropertyChange;
            }
            foreach (KeyValuePair<string, PtzProperty> prop in deviceManager.PtzProperties)
            {
                prop.Value.DevicePropertyChanged += NotifyDevicePropertyChange;
            }
        }

        //Event Handlers
        private void HttpResponseStatusChanged(ushort status)
        {
            MyCrestronHttpResponseStatusHandler.Invoke(status);
        }

        private static void NotifyDevicePropertyChange(string propName, string propValue)
        {
            //CrestronConsole.PrintLine($"{propName} - {propValue}");
            MyCrestronDevicePropertiesHandler.Invoke(propName, propValue);
        }






    }
}
