namespace KS_Fit_Pro
{
    public class StoragePermision : Permissions.BasePlatformPermission
    {
        public override (string androidPermission, bool isRuntime)[] RequiredPermissions =>
            new List<(string androidPermission, bool isRuntime)>
            {
        (Android.Manifest.Permission.ReadExternalStorage, true),
        (Android.Manifest.Permission.WriteExternalStorage, true),
        (Android.Manifest.Permission.ManageExternalStorage, true),
            }.ToArray();
    }
}
