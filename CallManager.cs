
using Crestron.SimplSharp;
using RestSharp;
using RestSharp.Authenticators.Digest;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SonyREAC1000Module
{
    class CallManager : ICallManager
    {
        private readonly IDeviceManager _deviceManager;
        private readonly RestClient _client;
        public HttpResponseStatusHandler MyHttpResponseStatusHandler { get; set; }

        public CallManager(string ip, string username, string password, IDeviceManager deviceManager)
        {
            _client = new RestClient($"http://{ip}")
            {
                Authenticator = new DigestAuthenticator(username, password)
            };
            _deviceManager = deviceManager;
        }

        public async void GetALLDeviceStatus()
        {
            await Task.Run(() => SendRequest(new RestRequest("analytics/inquiry.cgi?inq=status")));
            await Task.Run(() => SendRequest(new RestRequest("analytics/inquiry.cgi?inq=analyticsbox")));
            await Task.Run(() => SendRequest(new RestRequest("analytics/inquiry.cgi?inq=ptztracking")));
        }

        public async Task<ushort> SendPTZCommand(string command)
        {
            return await Task.Run(() => SendRequestwithResponse(new RestRequest($"analytics/ptztracking.cgi?{command}")));
        }
        public async Task<ushort> StartPtz()
        {
            return await Task.Run(() => SendRequestwithResponse(new RestRequest("analytics/analyticsbox.cgi?AppControl=entry,ptztracking")));
        }
        public async Task<ushort> StopPtz()
        {
            return await Task.Run(() => SendRequestwithResponse(new RestRequest("analytics/analyticsbox.cgi?AppControl=setentry,config")));
        }

        public async Task<ushort> SendRequestwithResponse(RestRequest request)
        {
            try
            {
                CrestronConsole.PrintLine("Sending Request");
                RestResponse response = await _client.ExecuteAsync(request);
                if (response != null)
                {
                    HttpResponseStatus(response);
                    if (response.IsSuccessStatusCode)
                    {
                        return 1;
                    }
                    return 0;    
                }
                else
                {
                    MyHttpResponseStatusHandler.Invoke(0);
                    return 0;     
                }
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine($"Error with Request {e.Message}");
                return 0;
            }      
        }

        public async Task SendRequest(RestRequest request)
        {
            CrestronConsole.PrintLine("Sending Request");
            try
            {
                RestResponse response = await _client.ExecuteAsync(request);

                if (response != null)
                {
                    HttpResponseStatus(response);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        _deviceManager.UpdatePropertiesFromDevice(ParseResponse(response.Content));
                    }
                }
                else
                {
                    MyHttpResponseStatusHandler.Invoke(0);
                }
            }         
            catch (Exception e)
            {
                CrestronConsole.PrintLine($"Error with Request {e.Message}");
            }
        }

        public IEnumerable<KeyValuePair<string, string>> ParseResponse(string response)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            string[] datapairs = response.Split('&');

            foreach (string str in datapairs)
            {
                string[] data = str.Split('=');

                if (!string.IsNullOrEmpty(data[1]))
                    dictionary.Add(data[0], data[1]);
            }
            foreach (KeyValuePair<string, string> data in dictionary)
            {
                yield return new KeyValuePair<string, string>(data.Key, data.Value);
            }
        }

        public void HttpResponseStatus(RestResponse response)
        {
                HttpStatusCode statusCode = response.StatusCode;
                ushort numericStatusCode = (ushort)statusCode;
                MyHttpResponseStatusHandler.Invoke(numericStatusCode);
        }


    }
}
