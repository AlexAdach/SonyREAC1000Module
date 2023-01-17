using System.Threading.Tasks;

namespace SonyREAC1000Module
{
    public interface ICallManager
    {
        HttpResponseStatusHandler MyHttpResponseStatusHandler { get; set; }
        void GetALLDeviceStatus();
        Task<ushort> StartPtz();
        Task<ushort> StopPtz();
        Task<ushort> SendPTZCommand(string command);
        //event EventHandler<KeyValuePair<string, string>>();

    }
}
