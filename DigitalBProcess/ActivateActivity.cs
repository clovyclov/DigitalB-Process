
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
using DigitalBProcess.qadigitalb360;

namespace DigitalBProcess
{
    [Activity(Label = "ActivateActivity")]
    public class ActivateActivity : Activity
    {
        Button activateBtn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ActivationLayout);

            activateBtn = FindViewById<Button>(Resource.Id.activateBtn);
            activateBtn.Click += delegate {
                StartActivity(typeof(DownloadFilesActivity));
            };
            // Create your application here
            //var contInfo = qadigitalb360.eview360.net.GetContactInfo();
        }

        //public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        //{
        //    base.OnCreate(savedInstanceState, persistentState);
        //    SetContentView(Resource.Layout.ActivationLayout);
        //    Button button = (Button)FindViewById(Resource.Id.activateBtn);
        //}
    }
}
