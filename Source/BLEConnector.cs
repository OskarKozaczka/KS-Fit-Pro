using Plugin.BLE;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using Plugin.BLE.Abstractions.Exceptions;
using System.Collections.ObjectModel;

namespace KS_Fit_Pro.Source
{
    public class BLEConnector
    {
        private IBluetoothLE ble;
        public IAdapter adapter;
        public ICharacteristic StatusCharacteristic;
        private ICharacteristic RequestCharacteristic;
        bool isConnected = false;
        public ObservableCollection<IDevice> devices = new ObservableCollection<IDevice>();

        public BLEConnector()
        {

            ble = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;
            //adapter.DeviceDiscovered += GetCharacteristics;
            adapter.DeviceDiscovered += ListDevice;
            AutoScan();
        }

        private void ListDevice(object sender, DeviceEventArgs e)
        {
            devices.Add(e.Device);
        }

        //async void GetCharacteristics(object sender, DeviceEventArgs args)
        //{
        //    if (args.Device.Name != "KS-F0") return;

        //    try
        //    {
        //        await adapter.ConnectToDeviceAsync(args.Device);
        //    }
        //    catch (DeviceConnectionException ex)
        //    {
        //        throw new DeviceConnectionException(args.Device.Id, args.Device.Name, ex.Message);
        //    }

        //    isConnected = true;

        //    foreach (var service in args.Device.GetServicesAsync().Result)
        //    {
        //        var chars = await service.GetCharacteristicsAsync();
        //        foreach (var characteristic in chars)
        //        {
        //            if (characteristic.Id.ToString().Split('-')[0] == "0000fe01") StatusCharacteristic = characteristic;
        //            if (characteristic.Id.ToString().Split('-')[0] == "0000fe02") RequestCharacteristic = characteristic;
        //        }
        //    }

        //    //StatusCharacteristic.ValueUpdated += MessageReceived;
        //    await StatusCharacteristic.StartUpdatesAsync();
        //}

        async internal Task<bool> AutoScan()
        {
            adapter.ScanMode = ScanMode.LowLatency;
            adapter.ScanTimeout = 10000;
            await adapter.StartScanningForDevicesAsync();
            return isConnected;
        }

        async internal Task<bool> SendMessage(byte[] request)
        {
            return await RequestCharacteristic.WriteAsync(request);
        }
    }
}
