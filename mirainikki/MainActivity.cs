using Android.App;
using Android.Widget;
using Android.OS;
using Android.Gms.Maps;
using System;
using Android.Gms.Maps.Model;
using static Android.Gms.Maps.GoogleMap;
using Android.Views;
using Android.Content;
using System.IO;
using SQLite;

namespace mirainikki
{
    [Activity(Label = "myPlaces", MainLauncher = true, Icon = "@drawable/logo")]
    public class MainActivity : Activity
    {
        private GoogleMap mMap;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.inicio);



            Button entrar = FindViewById<Button>(Resource.Id.btnEntrar);
            entrar.Click += delegate
            {
                Intent objIntent = new Intent(this, typeof(mainClass));

                StartActivity(objIntent);
            };



        }
    }
}

