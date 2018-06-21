
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.DownloadManager;
using Android.Net;
using System.IO;

namespace DigitalBProcess
{
    [Activity(Label = "DownloadFilesActivity", MainLauncher = false)]
    public class DownloadFilesActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.DownloadCollectionLayout);

            Button goToPlayBtn = FindViewById<Button>(Resource.Id.goToPlayerBtn);
            ProgressBar progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            TextView textView = FindViewById<TextView>(Resource.Id.textView1);
            TextView dlLocation = FindViewById<TextView>(Resource.Id.dlLocationTxt);

            //var downloadManager = CrossDownloadManager.Current;
            //var file = downloadManager.CreateDownloadFile("https://digitalb2017q3dev.blob.core.windows.net/prg-67/video/Black%20Label%20Ralph.mp4");
            //downloadManager.Start(file);

            //var documents = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            //var directoryname = Path.Combine(documents, "Videos");
            //var path = Path.Combine(directoryname, "video.mp4");

            //Better saving
            //string appDir = Android.App.Application.Context.GetExternalFilesDir(null).AbsolutePath;
            //var dlPath = Path.Combine(appDir, "Black-Label-Ralph.mp4");

            //string appDir = Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryMovies).AbsolutePath;
            string appDir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryMovies).AbsolutePath;
            var dlPath = Path.Combine(appDir, "Black-Label-Vid.mp4");

            DownloadManager downloadManager = (DownloadManager)Application.Context.GetSystemService(Context.DownloadService);
            DownloadManager.Request req = new DownloadManager.Request(
                Android.Net.Uri.Parse("https://digitalb2017q3dev.blob.core.windows.net/prg-67/video/Black%20Label%20Ralph.mp4")
                //Android.Net.Uri.Parse("http://ia800500.us.archive.org/33/items/Cartoontheater1930sAnd1950s1/PigsInAPolka1943.mp4")
                //Android.Net.Uri.Parse("android.resource://" + Application.PackageName + "/" + Resource.Raw.Lincoln)
            );
            
            //req.SetDestinationInExternalFilesDir(Application.Context, null, (string)Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryMovies) + "/Black-Label-Vid.mp4");
            req.SetDestinationUri(Android.Net.Uri.FromFile(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryMovies)));

            req.SetTitle("Black-Label-VIDEO");

            dlLocation.Text = "Downloading videos to: " + (string)Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryMovies);

            downloadManager.Enqueue(req);
            
            Task task = new Task(() =>
            {
                Thread.Sleep(30000);
                RunOnUiThread(() =>
                {
                    textView.Text = "You are ready to watch! Let's begin...";
                    progressBar.Visibility = ViewStates.Invisible;
                    goToPlayBtn.Visibility = ViewStates.Visible;
                });
            });
            task.Start();

            goToPlayBtn.Click += delegate {
                StartActivity(typeof(VideoPlayerActivity));
            };
        }
    }
}
