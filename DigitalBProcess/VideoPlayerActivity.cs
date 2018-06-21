
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DigitalBProcess
{
    [Activity(Label = "VideoPlayerActivity", MainLauncher = false)]
    public class VideoPlayerActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.VideoPlayer);

            // Create your application here
            VideoView videoPlayer = (VideoView)FindViewById<VideoView>(Resource.Id.videoView1);
            TextView pathText = FindViewById<TextView>(Resource.Id.pathText);

            //Android.Net.Uri uri = Android.Net.Uri.Parse("https://digitalb2017q3dev.blob.core.windows.net/prg-67/video/Black%20Label%20Ralph.mp4");
            //var uri = Android.Net.Uri.Parse("https://redirector.googlevideo.com/videoplayback?mm=31,29&signature=4012B0B76402679A1A9A6D0F379E2A44071A09CC.D2FA83083D17BD14AF4FC5DE9428DB5957C9AA1A&mn=sn-5hnekn7d,sn-5hne6nsd&ratebypass=yes&ipbits=0&key=yt6&ip=93.158.200.189&fvip=1&requiressl=yes&source=youtube&lmt=1508369789111750&dur=629.887&expire=1529529148&mt=1529507274&mv=u&ei=3G4qW5O4H9uKgAfP46uIBg&id=o-AF2e7eqorcJX7dh4KoXFTkffRSPnLxxE9825cQsZTAqf&ms=au,rdu&fexp=23709359&c=WEB&pl=26&mime=video/mp4&sparams=dur,ei,id,ip,ipbits,itag,lmt,mime,mm,mn,ms,mv,pl,ratebypass,requiressl,source,expire&itag=22&title=2018-Lincoln-Continental---FULL-REVIEW");
            //https://www.youtube.com/watch?v=edhzKV_Ul7c
            //videoPlayer.SetVideoURI(uri);

            //string appDir = Application.Context.GetExternalFilesDir(null).AbsolutePath;
            //string appDir = Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryMovies).ToString();
            //Java.IO.File appDir = Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryMovies);
            //Java.IO.File file = new Java.IO.File(appDir, "Black-Label-Ralph.mp4");

            string appDir = (string)Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryMovies);

            var dlPath = Path.Combine(appDir, "/Black-Label-Ralph.mp4");

            var files = Directory.GetFiles(appDir);

            foreach (var file in files)
            {
                pathText.Text += file + " - ";
            }

            //var uri = Android.Net.Uri.Parse(dlPath);

            //My just in case Video lol
            //var uri = Android.Net.Uri.Parse("android.resource://" + Application.PackageName + "/" + Resource.Raw.Black);

            //Show the path in the TextView
            //pathText.Text = appDir + "/Black-Label-Ralph.mp4";

            MediaController mediaController = new MediaController(this);
            mediaController.SetAnchorView(videoPlayer);
            // Set video link (mp4 format )
            //var video = Android.Net.Uri.Parse("http://ia800500.us.archive.org/33/items/Cartoontheater1930sAnd1950s1/PigsInAPolka1943.mp4");

            videoPlayer.SetMediaController(mediaController);


            //videoPlayer.SetVideoURI(uri);
            videoPlayer.SetVideoURI(Android.Net.Uri.Parse(appDir + "/Black-Label-Ralph.mp4"));

            videoPlayer.Start();
        }
    }
}
