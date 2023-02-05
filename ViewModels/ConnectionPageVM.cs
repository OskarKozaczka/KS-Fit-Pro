using KS_Fit_Pro.Pages;
using KS_Fit_Pro.Source;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS_Fit_Pro.ViewModels
{
    public partial class ConnectionPageVM
    {
        private readonly BLEConnector _connector;
        public ObservableCollection<IDevice> devices { get; set; }

        public bool isConnected { get; set; }

        public ConnectionPageVM(BLEConnector bLEConnector)
        {
            _connector = bLEConnector;
            devices = new ObservableCollection<IDevice>();
            //devices.Add(new DiscoveredDevice() { Name = "Tesasdasdt1" });
            //devices.Add(new DiscoveredDevice() { Name = "Test2" });
            //devices.Add(new DiscoveredDevice() { Name = "Testasdasdasdasdasdasd3" });
            _connector.adapter.DeviceDiscovered += AddDevice;        
        }
        private void AddDevice(object sender, DeviceEventArgs e)
        {
            if (e.Device.Name != null && !devices.Contains(e.Device))
            {
                devices.Add(e.Device);
            }
        }

        internal async Task<bool> ButtonClicked(IDevice device)
        {
            if (isConnected) return false;
            return await _connector.Connect(device);
        }

        internal async void StartScan()
        {
#if ANDROID
        await Permissions.RequestAsync<BlePermision>();
#endif
            await  _connector.adapter.StartScanningForDevicesAsync();
        }

        internal void StopScan()
        {
            _connector.adapter.StopScanningForDevicesAsync();
        }
    }
}