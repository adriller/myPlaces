using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using SQLite;
using static mirainikki.MainActivity;
using Android.Gms.Maps.Model;
using static mirainikki.mainClass;

namespace mirainikki
{
    [Activity(Label = "Gerenciar")]
    public class Gerenciar : Activity
    {
        double defaultValue;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MenuGerenciar);
            // Create your application here
            LatLng point = new LatLng(Intent.GetDoubleExtra("lat", defaultValue), Intent.GetDoubleExtra("lon", defaultValue));
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            path = Path.Combine(path, "Base.db3");
            var conn = new SQLiteConnection(path);
            conn.CreateTable<InfoMarker>();

            EditText descricao = FindViewById<EditText>(Resource.Id.txtInfo);
            try
            {
                var elementos = from s in conn.Table<InfoMarker>() select s;

                foreach (var file in elementos)
                {
                    if(Intent.GetDoubleExtra("lat", defaultValue) == file.lat)
                    {
                        if (file.descricao != null)
                            descricao.Text = file.descricao;
                    }
                    
                }
               
            }
            catch(System.Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
            
            Button adicionarDescricao = FindViewById<Button>(Resource.Id.btnDescricao);
            adicionarDescricao.Click += delegate
            {
                

                try
                {
                    var inserir = new InfoMarker();
                    //armazenar pin location 
                    inserir.lat = point.Latitude;
                    inserir.lon = point.Longitude;


                    //armazenar text
                    inserir.descricao = descricao.Text;

                    Boolean flag = true;
                    var elementos = from s in conn.Table<InfoMarker>() select s;

                    foreach (var file in elementos)
                    {
                        if (Intent.GetDoubleExtra("lat", defaultValue) == file.lat)
                        {
                            conn.Update(inserir);
                            flag = false;
                        } 

                    }

                    if (flag)
                    {
                        conn.Insert(inserir);
                    }
                    
                    Intent objIntent = new Intent(this, typeof(mainClass));
                    StartActivity(objIntent);

                }
                catch (System.Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
                }

            };

            Button excluirPin = FindViewById<Button>(Resource.Id.btnExcluir);
            excluirPin.Click += delegate
            {
                try
                {
                    Intent objIntent = new Intent(this, typeof(mainClass));
                    //apagar na memoria
                    conn = new SQLiteConnection(path);
                    conn.CreateTable<InfoMarker>();
                    
                    var elementos = from s in conn.Table<InfoMarker>() select s;
                    foreach (var file in elementos)
                    {
                        if (Intent.GetDoubleExtra("lat", defaultValue) == file.lat)
                        {
                            conn.Delete(file);
                        }

                    }
                    StartActivity(objIntent);
                    Toast.MakeText(this, "PIN exluído", ToastLength.Short).Show();

                }
                catch (System.Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
                }
            };

            Button voltar = FindViewById<Button>(Resource.Id.btnVoltar);
            voltar.Click += delegate
            {
                try
                {
                    Intent objIntent = new Intent(this, typeof(mainClass));
                    StartActivity(objIntent);

                }
                catch (System.Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
                }
            };

        }
    }
}