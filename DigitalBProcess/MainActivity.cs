using Android.App;
using Android.Widget;
using Android.OS;
using Android.Net;
using Android.Net.Wifi;
using Android.Content;
using System.Text;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Android.Views.InputMethods;
using Android.Support.V4.Net;

namespace DigitalBProcess
{

    [Activity(Label = "DigitalBProcess", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetContentView(Resource.Layout.StepOne);

            TextView txtInternetCheck =  FindViewById<TextView>(Resource.Id.txtInternetCheck);
            TextView wifiName        =   FindViewById<TextView>(Resource.Id.wifiName);
            EditText wifiPass        =   FindViewById<EditText>(Resource.Id.wifiPass);
            ListView wifiNetworkList =   FindViewById<ListView>(Resource.Id.listView1);
            Button connBtn           =   FindViewById<Button>(Resource.Id.wifiConnBtn);
            Button stepTwoBtn        =   FindViewById<Button>(Resource.Id.stepTwoBtn);
            WifiManager wifiManager     =   (WifiManager)Application.Context.GetSystemService(WifiService);

            ArrayList networks = new ArrayList();

            if (wifiManager.StartScan())
            {
                foreach (ScanResult network in wifiManager.ScanResults)
                {
                    networks.Add(network.Ssid);
                }
            }


            //connBtn.Visibility = Android.Views.ViewStates.Invisible;
            stepTwoBtn.Visibility = Android.Views.ViewStates.Invisible;

            //Turn on wifi if not enabled
            if (!wifiManager.IsWifiEnabled)
                wifiManager.SetWifiEnabled(true);





            //Supplies wifi connections picked up
            var adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, networks);
            wifiNetworkList.Adapter = adapter;

            //When network name gets clicked
            wifiNetworkList.ItemClick += WifiItemClicked;

            //Gets SSID of item clicked
            void WifiItemClicked(object sender, AdapterView.ItemClickEventArgs e)
            {
                string selectedItem =          (string)networks[e.Position];
                wifiName.Visibility =   Android.Views.ViewStates.Visible;
                wifiPass.Visibility =   Android.Views.ViewStates.Visible;
                connBtn.Visibility =    Android.Views.ViewStates.Visible;
                wifiName.Text =         selectedItem;
                wifiPass.Hint =         $"Enter password for {selectedItem}";
            }

            //Connect to the wifi
            connBtn.Click += delegate {

                InputMethodManager inputManager = (InputMethodManager)this.GetSystemService(Context.InputMethodService);
                inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);

                var wifiConfig = new WifiConfiguration();
                wifiConfig.Ssid = string.Format("\"{0}\"", wifiName.Text);
                wifiConfig.PreSharedKey = string.Format("\"{0}\"", wifiPass.Text);

                using (WifiManager wifiManager2 = (WifiManager)Application.Context.GetSystemService(Context.WifiService))
                {
                    int netId = wifiManager2.AddNetwork(wifiConfig);
                    wifiManager2.Disconnect();
                    wifiManager2.EnableNetwork(netId, true);
                    wifiManager2.Reconnect();
                }

                txtInternetCheck.Text = "Connecting...Please Wait";

                //Thread.Sleep(7000);

                //ConnectivityManager connectivityManager = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
                //var netInfo = connectivityManager.ActiveNetworkInfo;

                ////netInfo.IsConnected && 
                //if (netInfo.Type == ConnectivityType.Wifi)
                //{
                //    txtInternetCheck.Text = "Great, you are connected!";
                //    stepTwoBtn.Visibility = Android.Views.ViewStates.Visible;
                //}
                //else
                //{
                //    txtInternetCheck.Text = "Something went wrong. Please try again.";
                //}


                Thread thread = new Thread(() =>
                {
                    Thread.Sleep(7000);

                    ConnectivityManager connectivityManager = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
                    var netInfo = connectivityManager.ActiveNetworkInfo;

                    //netInfo.IsConnected && 
                    if (netInfo.Type == ConnectivityType.Wifi)
                    {
                        RunOnUiThread(() =>
                        {
                            txtInternetCheck.Text = "Great, you are connected!";
                            stepTwoBtn.Visibility = Android.Views.ViewStates.Visible;
                        });
                    }
                    else if (netInfo == null)
                    {
                        RunOnUiThread(() =>
                        {
                            txtInternetCheck.Text = "Something went wrong. Please try again.";
                        });
                    }
                });
                thread.Start();
            };

            stepTwoBtn.Click += delegate {
                Intent intent = new Intent(this, typeof(ActivateActivity));
                StartActivity(intent);
                Finish();
            };
        }
    }
}

