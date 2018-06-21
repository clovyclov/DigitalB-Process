using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace DigitalBProcess
{
    public class DialogWifiConn : DialogFragment
    {
        //EditText wifiPass;
        //TextView wifiName;
        //Button connBtn;
        public DialogWifiConn()
        {
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var netName = Arguments.GetString("networkName");

            var view = inflater.Inflate(Resource.Layout.WifiConnectionLayout, container, true);

            var wifiNameTextView = view.FindViewById<TextView>(Resource.Id.wifiName);
            wifiNameTextView.Text = netName;

            return view;
        }


    }
}
