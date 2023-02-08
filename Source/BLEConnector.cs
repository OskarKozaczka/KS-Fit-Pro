using Microsoft.Maui.Controls;
using Plugin.BLE;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using Plugin.BLE.Abstractions.Exceptions;
using System;
using System.Collections.ObjectModel;

namespace KS_Fit_Pro.Source
{
    public class BLEConnector
    {
        public IAdapter adapter;
        public ICharacteristic StatusCharacteristic;
        private ICharacteristic RequestCharacteristic;
        public bool isConnected = false;
        public ObservableCollection<IDevice> devices = new ObservableCollection<IDevice>();
        public Guid lastDeviceID;
        public event EventHandler<CharacteristicUpdatedEventArgs> OnMessageReceived;
        public event EventHandler<DeviceEventArgs> OnDeviceConnected;

        public BLEConnector()
        {
            adapter = CrossBluetoothLE.Current.Adapter;
            TryRecconect();
        }

        private async void TryRecconect()
        {
            var loadedID = await SecureStorage.Default.GetAsync("lastDevice");
            lastDeviceID = loadedID != null ? Guid.Parse(loadedID) : Guid.Empty;
            if (lastDeviceID == Guid.Empty) return; 

            var device =  await adapter.ConnectToKnownDeviceAsync(lastDeviceID);
            await Connect(device);
        }

        async internal Task SendMessage(byte[] request)
        {
            if (!isConnected) return;

            await RequestCharacteristic.WriteAsync(request);
            Thread.Sleep(1000);
        }

        internal async Task<bool> Connect(IDevice device)
        {
            try
            {
                await adapter.ConnectToDeviceAsync(device);
            }
            catch (DeviceConnectionException ex)
            {
                throw new DeviceConnectionException(device.Id, device.Name, ex.Message);
            }

            var result = await GetCharacteristics(device);
            if(result)
            {
                StatusCharacteristic.ValueUpdated += MessageReceived;
                bool isConnected = true;

                var eventArgs = new DeviceEventArgs();
                eventArgs.Device = device;
                OnDeviceConnected?.Invoke(this, eventArgs);
                lastDeviceID = device.Id;
                await SecureStorage.Default.SetAsync("lastDevice", lastDeviceID.ToString());
            }
            return isConnected;
        }

        public void MessageReceived(object sender, CharacteristicUpdatedEventArgs e)
        {
            OnMessageReceived?.Invoke(this, e);
        }

        async Task<bool> GetCharacteristics(IDevice device)
        {
            if (device.Name != "KS-F0") return false;

            foreach (var service in device.GetServicesAsync().Result)
            {
                var characteristics = await service.GetCharacteristicsAsync();
                foreach (var characteristic in characteristics)
                {
                    if (characteristic.Id.ToString().Split('-')[0] == "0000fe01") StatusCharacteristic = characteristic;
                    if (characteristic.Id.ToString().Split('-')[0] == "0000fe02") RequestCharacteristic = characteristic;
                }
            }

            if (StatusCharacteristic == null || RequestCharacteristic == null) return false;
            await StatusCharacteristic.StartUpdatesAsync();
            return true;
        }
    }
}
