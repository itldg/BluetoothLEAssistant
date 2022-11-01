﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.GenericAttributeProfile;

namespace 蓝牙调试助手.Tools
{
    /// <summary>
    /// 格式化显示信息的助手。
    /// </summary>
    public static class DisplayHelpers
    {
        /// <summary>
        /// 获取服务名称
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static string GetServiceName(GattDeviceService service)
        {
            if (IsSigDefinedUuid(service.Uuid))
            {
                GattNativeServiceUuid serviceName;
                if (Enum.TryParse(ConvertUuidToShortId(service.Uuid).ToString(), out serviceName))
                {
                    return serviceName.ToString();
                }
            }

            if(IsCustomService(service, out string name))
            {
                return name;
            }
            return service.Uuid.ToString().Split('-')[0];
        }

        /// <summary>
        /// 获取特征名称
        /// </summary>
        /// <param name="characteristic"></param>
        /// <returns></returns>
        public static string GetCharacteristicName(GattCharacteristic characteristic)
        {
            if (IsSigDefinedUuid(characteristic.Uuid))
            {
                GattNativeCharacteristicUuid characteristicName;
                if (Enum.TryParse(ConvertUuidToShortId(characteristic.Uuid).ToString(), out characteristicName))
                {
                    return characteristicName.ToString();
                }
            }

            if (!string.IsNullOrEmpty(characteristic.UserDescription))
            {
                return characteristic.UserDescription;
            }

            else
            {
                if (IsCustomCharacteristic(characteristic, out string name))
                {
                    return name;
                }

                return characteristic.Uuid.ToString().Split('-')[0];
            }
        }

       /// <summary>
       /// 是否符合标准定义的UUid
       /// </summary>
       /// <param name="uuid"></param>
       /// <returns></returns>
        private static bool IsSigDefinedUuid(Guid uuid)
        {
            var bluetoothBaseUuid = new Guid("00000000-0000-1000-8000-00805F9B34FB");

            var bytes = uuid.ToByteArray();
            
            bytes[0] = 0;
            bytes[1] = 0;
            var baseUuid = new Guid(bytes);
            return baseUuid == bluetoothBaseUuid;
        }

        public static ushort ConvertUuidToShortId(Guid uuid)
        {
            // Get the short Uuid
            var bytes = uuid.ToByteArray();
            var shortUuid = (ushort)(bytes[0] | (bytes[1] << 8));
            return shortUuid;
        }


        /// <summary>
        /// 是否包含自定义服务
        /// </summary>
        /// <param name="gattCharacteristicProperties"></param>
        /// <returns></returns>
        public static bool IsCustomService(GattDeviceService service ,out string name)
        {
            string serviceUUid = (service.Uuid.ToString().Split('-'))[0];
            

            switch (serviceUUid)
            {
                case "03010000":
                    name = "DownloadService";
                    break;
                default:
                    name =  string.Empty ;
                    return false;
            }
            return true;

        }

        /// <summary>
        /// 是否包含自定义特征
        /// </summary>
        /// <param name="gattCharacteristicProperties"></param>
        /// <returns></returns>
        public static bool IsCustomCharacteristic(GattCharacteristic characteristic, out string name)
        {
            string characteristicUUid = (characteristic.Uuid.ToString().Split('-'))[0];
           

            switch (characteristicUUid)
            {
                case "01050304":
                    name = "DataSize";
                    break;
                default:
                    name = string.Empty;
                    return false;
            }
            return true;
        }


    }

    /// <summary>
    /// 预定义的服务
    /// </summary>
    public enum GattNativeServiceUuid : ushort
    {
        None = 0,
        AlertNotification = 0x1811,
        Battery = 0x180F,
        BloodPressure = 0x1810,
        CurrentTimeService = 0x1805,
        CyclingSpeedandCadence = 0x1816,
        DeviceInformation = 0x180A,
        GenericAccess = 0x1800,
        GenericAttribute = 0x1801,
        Glucose = 0x1808,
        HealthThermometer = 0x1809,
        HeartRate = 0x180D,
        HumanInterfaceDevice = 0x1812,
        ImmediateAlert = 0x1802,
        LinkLoss = 0x1803,
        NextDSTChange = 0x1807,
        PhoneAlertStatus = 0x180E,
        ReferenceTimeUpdateService = 0x1806,
        RunningSpeedandCadence = 0x1814,
        ScanParameters = 0x1813,
        TxPower = 0x1804,
        SimpleKeyService = 0xFFE0,
        UserData = 0x181C
    }

    /// <summary>
    /// 预定义的特征
    /// </summary>
    public enum GattNativeCharacteristicUuid : ushort
    {
        None = 0,
        AlertCategoryID = 0x2A43,
        AlertCategoryIDBitMask = 0x2A42,
        AlertLevel = 0x2A06,
        AlertNotificationControlPoint = 0x2A44,
        AlertStatus = 0x2A3F,
        Appearance = 0x2A01,
        BatteryLevel = 0x2A19,
        BloodPressureFeature = 0x2A49,
        BloodPressureMeasurement = 0x2A35,
        BodySensorLocation = 0x2A38,
        BootKeyboardInputReport = 0x2A22,
        BootKeyboardOutputReport = 0x2A32,
        BootMouseInputReport = 0x2A33,
        CSCFeature = 0x2A5C,
        CSCMeasurement = 0x2A5B,
        CurrentTime = 0x2A2B,
        DateTime = 0x2A08,
        DayDateTime = 0x2A0A,
        DayofWeek = 0x2A09,
        DeviceName = 0x2A00,
        DSTOffset = 0x2A0D,
        ExactTime256 = 0x2A0C,
        FirmwareRevisionString = 0x2A26,
        GlucoseFeature = 0x2A51,
        GlucoseMeasurement = 0x2A18,
        GlucoseMeasurementContext = 0x2A34,
        HardwareRevisionString = 0x2A27,
        HeartRateControlPoint = 0x2A39,
        HeartRateMeasurement = 0x2A37,
        HIDControlPoint = 0x2A4C,
        HIDInformation = 0x2A4A,
        IEEE11073_20601RegulatoryCertificationDataList = 0x2A2A,
        IntermediateCuffPressure = 0x2A36,
        IntermediateTemperature = 0x2A1E,
        LocalTimeInformation = 0x2A0F,
        ManufacturerNameString = 0x2A29,
        MeasurementInterval = 0x2A21,
        ModelNumberString = 0x2A24,
        NewAlert = 0x2A46,
        PeripheralPreferredConnectionParameters = 0x2A04,
        PeripheralPrivacyFlag = 0x2A02,
        PnPID = 0x2A50,
        ProtocolMode = 0x2A4E,
        ReconnectionAddress = 0x2A03,
        RecordAccessControlPoint = 0x2A52,
        ReferenceTimeInformation = 0x2A14,
        Report = 0x2A4D,
        ReportMap = 0x2A4B,
        RingerControlPoint = 0x2A40,
        RingerSetting = 0x2A41,
        RSCFeature = 0x2A54,
        RSCMeasurement = 0x2A53,
        SCControlPoint = 0x2A55,
        ScanIntervalWindow = 0x2A4F,
        ScanRefresh = 0x2A31,
        SensorLocation = 0x2A5D,
        SerialNumberString = 0x2A25,
        ServiceChanged = 0x2A05,
        SoftwareRevisionString = 0x2A28,
        SupportedNewAlertCategory = 0x2A47,
        SupportedUnreadAlertCategory = 0x2A48,
        SystemID = 0x2A23,
        TemperatureMeasurement = 0x2A1C,
        TemperatureType = 0x2A1D,
        TimeAccuracy = 0x2A12,
        TimeSource = 0x2A13,
        TimeUpdateControlPoint = 0x2A16,
        TimeUpdateState = 0x2A17,
        TimewithDST = 0x2A11,
        TimeZone = 0x2A0E,
        TxPowerLevel = 0x2A07,
        UnreadAlertStatus = 0x2A45,
        AggregateInput = 0x2A5A,
        AnalogInput = 0x2A58,
        AnalogOutput = 0x2A59,
        CyclingPowerControlPoint = 0x2A66,
        CyclingPowerFeature = 0x2A65,
        CyclingPowerMeasurement = 0x2A63,
        CyclingPowerVector = 0x2A64,
        DigitalInput = 0x2A56,
        DigitalOutput = 0x2A57,
        ExactTime100 = 0x2A0B,
        LNControlPoint = 0x2A6B,
        LNFeature = 0x2A6A,
        LocationandSpeed = 0x2A67,
        Navigation = 0x2A68,
        NetworkAvailability = 0x2A3E,
        PositionQuality = 0x2A69,
        ScientificTemperatureinCelsius = 0x2A3C,
        SecondaryTimeZone = 0x2A10,
        String = 0x2A3D,
        TemperatureinCelsius = 0x2A1F,
        TemperatureinFahrenheit = 0x2A20,
        TimeBroadcast = 0x2A15,
        BatteryLevelState = 0x2A1B,
        BatteryPowerState = 0x2A1A,
        PulseOximetryContinuousMeasurement = 0x2A5F,
        PulseOximetryControlPoint = 0x2A62,
        PulseOximetryFeatures = 0x2A61,
        PulseOximetryPulsatileEvent = 0x2A60,
        SimpleKeyState = 0xFFE1
    }



    public enum SSTDCustomService : uint
    {
        DownloadService = 3010000
    }

    public enum SSTDCustomCharacteristic : uint
    {
        DataSize = 1050340
    }
}
