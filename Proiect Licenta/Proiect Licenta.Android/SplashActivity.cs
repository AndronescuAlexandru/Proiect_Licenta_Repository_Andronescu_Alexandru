using Android.App;
using Android.Content;
using Android.OS;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Proiect_Licenta.Droid
{
    [Activity(Icon = "@mipmap/icon",Theme = "@style/Theme.Splash",MainLauncher = true,NoHistory = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActivityIndicator activityIndicator = new ActivityIndicator { IsRunning = true };
            StartActivity(typeof(MainActivity));
        }
    }
}