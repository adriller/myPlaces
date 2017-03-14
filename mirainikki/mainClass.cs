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
using Geolocator.Plugin;

namespace mirainikki
{
    [Activity(Label = "Activity1")]
    public class mainClass : Activity, IOnMapReadyCallback, GoogleMap.IOnMapLongClickListener, IInfoWindowAdapter, IOnInfoWindowClickListener
    {
        private GoogleMap mMap;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource

            SetContentView(Resource.Layout.Main);
            SetUpMap();
            

        }

        private void CarregarMarkers()
        {
            try
            {
                var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                path = Path.Combine(path, "Base.db3");
                var conn = new SQLiteConnection(path);
                //conn.DeleteAll<InfoMarker>();
                var elementos = from s in conn.Table<InfoMarker>() select s;

                foreach (var file in elementos)
                {
                    LatLng point = new LatLng(Convert.ToDouble(file.lat), Convert.ToDouble(file.lon));

                    mMap.AddMarker(new MarkerOptions()
                   .SetPosition(point) );

                    mMap.SetInfoWindowAdapter(this);
                    mMap.SetOnInfoWindowClickListener(this);
                }
            }
            catch (System.Exception ex)
            {
                //Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
        }

        private async void SetUpMap()
        {
            if (mMap == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map).GetMapAsync(this);
            }
            try
            {
                var locator = CrossGeolocator.Current;

                locator.DesiredAccuracy = 50; //100 is new default

                var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);

                LatLng point = new LatLng(position.Latitude, position.Longitude);

                CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(point, 15);

                mMap.MoveCamera(camera);
            }
            catch (System.Exception ex)
            {
                //Toast.MakeText(this, "nao deu certo " + ex.Message, ToastLength.Long).Show();
            }
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            mMap = googleMap;
            CarregarMarkers();
            
            mMap.SetOnMapLongClickListener(this);

        }


        public void OnMapLongClick(LatLng point)
        {
            mMap.AddMarker(new MarkerOptions()
                .SetPosition(point));
            mMap.SetInfoWindowAdapter(this);
            mMap.SetOnInfoWindowClickListener(this);



        }

        public View GetInfoContents(Marker marker)
        {
            return null;
        }

        public View GetInfoWindow(Marker marker)
        {
            String desc = null;
            try
            {
                var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                path = Path.Combine(path, "Base.db3");
                var conn = new SQLiteConnection(path);
                var elementos = from s in conn.Table<InfoMarker>() select s;

                foreach (var file in elementos)
                {
                    if (marker.Position.Latitude == file.lat)
                    {
                        desc = file.descricao;
                    }

                }
            }
           catch(System.Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
            View view = LayoutInflater.Inflate(Resource.Layout.info_window, null, false);
            TextView tv = view.FindViewById<TextView>(Resource.Id.txtInfo);
            tv.SetBackgroundColor(Android.Graphics.Color.White);
            if(desc != null)
                tv.Text = desc;
            return view;
        }

        public void OnInfoWindowClick(Marker marker)
        {
            LatLng point = marker.Position;
            Intent objIntent = new Intent(this, typeof(Gerenciar));
            objIntent.PutExtra("lat", point.Latitude);
            objIntent.PutExtra("lon", point.Longitude);
            StartActivity(objIntent);

        }

        public class InfoMarker
        {
            [SQLite.PrimaryKey]
            public Double lat { get; set; }
            public Double lon { get; set; }
            public String descricao { get; set; }
        }
    }
}

