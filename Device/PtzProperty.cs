using System;


namespace SonyREAC1000Module
{
    public class PtzProperty : DeviceProperty
    {
        
        public PtzProperty(string name) : base(name)
        {

        }

        public string CreateParameterString()
        {
            if (base._data != String.Empty)
            {
                return base._name + "=" + base._data;
            }
            else
            {
                return String.Empty;
            }
        }

    }
}
