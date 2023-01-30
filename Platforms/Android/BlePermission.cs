namespace KS_Fit_Pro
{
    public class BlePermision : Permissions.BasePlatformPermission
    {
        public override (string androidPermission, bool isRuntime)[] RequiredPermissions =>
            new List<(string androidPermission, bool isRuntime)>
            {
        (Android.Manifest.Permission.AccessCoarseLocation, true),
        (Android.Manifest.Permission.AccessFineLocation, true),
        (Android.Manifest.Permission.BluetoothScan, true),
        (Android.Manifest.Permission.BluetoothAdmin, true),
        (Android.Manifest.Permission.Bluetooth, true),
        (Android.Manifest.Permission.BluetoothConnect, true),
        (Android.Manifest.Permission.BluetoothAdvertise, true),
        (Android.Manifest.Permission.AccessBackgroundLocation, true),
        (Android.Manifest.Permission.ReadExternalStorage, true),
        (Android.Manifest.Permission.WriteExternalStorage, true),
            }.ToArray();
    }
}
