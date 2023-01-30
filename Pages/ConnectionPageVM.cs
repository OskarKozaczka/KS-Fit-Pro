using KS_Fit_Pro.Source;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS_Fit_Pro.Pages
{ 
    public partial class ConnectionPageVM
    {
        private readonly BLEConnector _connector;
        public ObservableCollection<DiscoveredDevice> devices { get; set; }

        public ConnectionPageVM(BLEConnector bLEConnector)
        {
            _connector = bLEConnector;
            devices = new ObservableCollection<DiscoveredDevice>();
            _connector.adapter.DeviceDiscovered += AddDevice;
        }

        private void AddDevice(object sender, DeviceEventArgs e)
        {
            devices.Add(new DiscoveredDevice() { Name = e.Device.Name });
        }

        public class DiscoveredDevice
        {
            public string Name { get; set; }
        }
    }

}