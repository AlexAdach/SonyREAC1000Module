
using System.Collections.Generic;

namespace SonyREAC1000Module
{
    public interface IDeviceManager
    {
        Dictionary<string, DeviceProperty> DeviceProperties { get; set; }
        Dictionary<string, PtzProperty> PtzProperties { get; set; }
        Dictionary<string, string> PTZCommands { get; set; }

        //void UpdatePropertiesFromDevice(KeyValuePair<string, string> value);
        void UpdatePropertiesFromDevice(IEnumerable<KeyValuePair<string, string>> value);
        //void DisplayPropertyValueByName(string name);
        string CreatePTZPropertyString();
    }
}
