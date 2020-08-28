using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Plugin.CurrentActivity;
using System;
using Xamarin.Forms.OpenTok.Android.Service;

namespace Shikkhanobish.Droid
{
    [Activity(Label = "Shikkhanobish", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true,  ScreenOrientation = ScreenOrientation.Portrait )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        [Obsolete]
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            PlatformOpenTokService.Init();
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            base.SetTheme(global::Android.Resource.Style.ThemeHoloLight);

            //this.Window.AddFlags(WindowManagerFlags.Fullscreen);
            //this.Window.AddFlags(WindowManagerFlags.KeepScreenOn);

            //this.Window.ClearFlags(WindowManagerFlags.Fullscreen);

            CrossCurrentActivity.Current.Activity = this;

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            Window.SetStatusBarColor ( Android.Graphics.Color.Rgb ( 151 , 97 , 253 ) );

            //Intialize plugin
            Rg.Plugins.Popup.Popup.Init ( this , savedInstanceState );
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

        public override void OnBackPressed ( )
        {
            if ( Rg.Plugins.Popup.Popup.SendBackPressed ( base.OnBackPressed ) )
            {

            }
            else
            {

            }
        }
    }
}