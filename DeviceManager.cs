
using System;
using System.Collections.Generic;

namespace SonyREAC1000Module
{
    class DeviceManager : IDeviceManager
    {
        public Dictionary<string, DeviceProperty> DeviceProperties { get; set; }
        public Dictionary<string, PtzProperty> PtzProperties { get; set; }
        public Dictionary<string, string> PTZCommands { get; set; }

        public DeviceManager()
        {
            DeviceProperties = new Dictionary<string, DeviceProperty>()
            {
                {"BehaviorState", new DeviceProperty("BehaviorState") },
                {"CurrentApp", new DeviceProperty("CurrentApp") },
                {"UpdateState", new DeviceProperty("UpdateState") }
            };
            PtzProperties = new Dictionary<string, PtzProperty>()
            {
                {"PtzTrackingMode", new PtzProperty("PtzTrackingMode") },
                {"PtzTrackingShotModeLR", new PtzProperty("PtzTrackingShotModeLR") },
                {"PtzTrackingCurvingCorrectionLR", new PtzProperty("PtzTrackingCurvingCorrectionLR") },
                {"PtzTrackingCamInstallHeightLR", new PtzProperty("PtzTrackingCamInstallHeightLR") },
                {"PtzTrackingCamInstallDistanceLR", new PtzProperty("PtzTrackingCamInstallDistanceLR") },
                {"PtzTrackingCamInstallOffsetLR", new PtzProperty("PtzTrackingCamInstallOffsetLR") },
                {"PtzTrackingCurvingCorrectionHW", new PtzProperty("PtzTrackingCurvingCorrectionHW") },
                {"PtzTrackingCamInstallHeightHW", new PtzProperty("PtzTrackingCamInstallHeightHW ") },
                {"PtzTrackingCamInstallDistanceHW", new PtzProperty("PtzTrackingCamInstallDistanceHW") },
                {"PtzTrackingCamInstallOffsetHW", new PtzProperty("PtzTrackingCamInstallOffsetHW") },
                {"PtzTrackingMoveHomePos", new PtzProperty("PtzTrackingMoveHomePos ") },
                {"PtzTrackingSensitivity", new PtzProperty("PtzTrackingSensitivity") },
                {"PtzTrackingTriggerType", new PtzProperty("PtzTrackingTriggerType") },
                {"PtzTrackingAutoWaitTime", new PtzProperty("PtzTrackingAutoWaitTime") },
                {"PtzTrackingHomePosDetectArea", new PtzProperty("PtzTrackingHomePosDetectArea") },
                {"PtzTrackingMaskArea", new PtzProperty("PtzTrackingMaskArea") },
                {"PtzTrackingPanLimit", new PtzProperty("PtzTrackingPanLimit") },
                {"PtzTrackingTiltLimit", new PtzProperty("PtzTrackingTiltLimit") },
                {"PtzTrackingZoomLimit", new PtzProperty("PtzTrackingZoomLimit") },
                {"PtzTrackingMoveRecoveryPos", new PtzProperty("PtzTrackingMoveRecoveryPos") },
                {"PtzTrackingLostWaitTime", new PtzProperty("PtzTrackingLostWaitTime") },
                {"PtzTrackingHandwritingArea", new PtzProperty("PtzTrackingHandwritingArea") },
                {"PtzTrackingTallyEnable", new PtzProperty("PtzTrackingTallyEnable") },
                {"PtzTrackingAdjustObjectHeight", new PtzProperty("PtzTrackingAdjustObjectHeight") },
                {"PtzTRackingAdjustObjectSize", new PtzProperty("PtzTRackingAdjustObjectSize") }
            };
            PTZCommands = new Dictionary<string, string>()
            {
                {"PtzTrackingCalibrateHomePos", "On"},
                {"PtzTrackingDecideHomePos", "On" },
                {"PtzTrackingDecideRecoveryPos", "On" },
                {"PtzTrackingResetCamera", "On" },
                {"PtzTrackingObjectCoordinate", "10,3,1920,1080" },
                {"PtzTrackingMoveHomePos", "On" },
                {"PtzTrackingMoveRecoveryPos", "On" },
                {"PtzTrackingStartAdjustingFrame", "On" },
                {"PtzTrackingEndAdjustingFrame", "completion" },
            };
        }

        public void UpdatePropertiesFromDevice(IEnumerable<KeyValuePair<string, string>> values)
        {
            foreach (KeyValuePair<string, string> value in values)
            {
                if (DeviceProperties.ContainsKey(value.Key))
                {
                    DeviceProperties[value.Key].Data = value.Value;
                }
                else if (PtzProperties.ContainsKey(value.Key))
                {
                    PtzProperties[value.Key].Data = value.Value;
                }
            }
        }

 /*       public void UpdatePropertyFromCrestron(string key, string value)
        {
            if (DeviceProperties.ContainsKey(key))
            {
                DeviceProperties[key].Data = value;
            }
            else if (PtzProperties.ContainsKey(key))
            {
                PtzProperties[key].Data = value;
            }
        }*/

        public string CreatePTZPropertyString()
        {
            IList<string> ptzProps = new List<string>();

            var deviceProps = this.GetType().GetProperties();

            foreach (KeyValuePair<string, PtzProperty> prop in PtzProperties)
            {
                string newstring = prop.Value.CreateParameterString();
                if (newstring != String.Empty)
                {
                    ptzProps.Add(newstring);
                }
            }

            string ptzPropsString = string.Join("&", ptzProps);
            return ptzPropsString;
        }

    }

}
