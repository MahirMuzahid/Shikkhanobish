using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;

namespace Shikkhanobish.Droid
{
    [Activity(Label = "Shikkhanobish", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            base.SetTheme(global::Android.Resource.Style.ThemeHoloLight);

            this.Window.AddFlags(WindowManagerFlags.Fullscreen);
            this.Window.AddFlags(WindowManagerFlags.KeepScreenOn);

            //this.Window.ClearFlags(WindowManagerFlags.Fullscreen);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected bool OnBackButtonPressed()
        {


            return true;
        }
    }
}