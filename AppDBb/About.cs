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

namespace AppDBb
{
    [Activity(Label = "About")]
    public class About : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.About);
            // Create your application here
            Button yes = FindViewById<Button>(Resource.Id.yes);
            Button no = FindViewById<Button>(Resource.Id.no);
            yes.Click += Click_yes;
            no.Click += Click_no;
        }

        private void Click_no(object sender, EventArgs e)
        {
            var intent = new Intent();
            intent.PutExtra("Like", 0);
            SetResult(Result.Ok, intent);
            Finish();
        }

        private void Click_yes(object sender, EventArgs e)
        {
            var intent = new Intent();
            intent.PutExtra("Like", 1);
            SetResult(Result.Ok, intent);
            Finish();
        }
    }
}