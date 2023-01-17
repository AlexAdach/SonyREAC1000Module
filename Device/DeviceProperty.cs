

namespace SonyREAC1000Module
{
    public class DeviceProperty
    {
        public DevicePropertyChangedHandler DevicePropertyChanged { get; set; }

        protected string _data;
        protected string _name;
        
        public DeviceProperty(string name)
        {
            _data = string.Empty;
            _name = name;
        }

        public string Data
        {
            get
            {
                return _data;
            }
            set
            {
                if (value != _data)
                {
                    _data = value;
                    NotifyPropertyChanged();
                }
            }
        }
        protected void NotifyPropertyChanged()
        {
            DevicePropertyChanged.Invoke(_name, _data);
        }


    }
}
