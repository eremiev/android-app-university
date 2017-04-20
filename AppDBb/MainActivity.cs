using SQLite;
using System;
using Android.OS;
using Android.App;
using Android.Widget;
using Android.Content;

namespace AppDBb
{
    [Activity(Label = "Main", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int vote = -1;
        Button vibrate;
        EditText text;
        TextView display_text;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            vibrate = FindViewById<Button>(Resource.Id.vibrate);
            text = FindViewById<EditText>(Resource.Id.editText1);
            Button toast = FindViewById<Button>(Resource.Id.toast);
            Button checkVote = FindViewById<Button>(Resource.Id.vote);
            Button button3 = FindViewById<Button>(Resource.Id.button3);
            Button button4 = FindViewById<Button>(Resource.Id.button4);
            Button website = FindViewById<Button>(Resource.Id.website);
            display_text = FindViewById<TextView>(Resource.Id.textView1);

            toast.Click += ToastVote;
            website.Click += Website;
            vibrate.Click += Vibrate;
            checkVote.Click += DialogVote;
            button3.Click += Button3_Click;
            button4.Click += Button4_Click;
        }

        private void Website(object sender, EventArgs e)
        {
            var intent = new Intent();
            intent.SetAction(Intent.ActionView);
            intent.SetData(Android.Net.Uri.Parse("http://www.ue-varna.bg"));
            StartActivity(intent);
        }

        private void ToastVote(object sender, EventArgs e)
        {
            if (vote == 0)
                Toast.MakeText(this, "I don't like this app!", ToastLength.Long).Show();
            else if (vote == 1)
                Toast.MakeText(this, "I like this app!", ToastLength.Long).Show();
            else
                Toast.MakeText(this, "Please rate this app!", ToastLength.Long).Show();
        }

        private void DialogVote(object sender, EventArgs e)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            AlertDialog alertDialog = builder.Create();
            alertDialog.SetTitle("Your rate:");

            if (vote == 0)
                alertDialog.SetMessage("I don't like this app!");
            else if (vote == 1)
                alertDialog.SetMessage("I like this app!");
            else
                alertDialog.SetMessage("Please rate this app!");

            alertDialog.SetButton("OK", (s, ev) => { });
            alertDialog.Show();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(SetUpActivity));
            StartActivity(intent);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(About));
            StartActivityForResult(intent, 100);
        }

        private void Vibrate(object sender, System.EventArgs e)
        {
            string dbPath = System.IO.Path.Combine(
       System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
       "time.db3");

            var db = new SQLiteConnection(dbPath);
            try
            {
                // var time = db.Table<Items>().Where(x => x.Id == 1).FirstOrDefault();
                long timee = 0;
                foreach (var item in db.Table<Items>())
                {
                    if (item.Id == 1)
                        timee = Convert.ToInt64(item.Time);
                }
                Vibrator vibrator = (Vibrator)GetSystemService(VibratorService);
                // vibrator.Vibrate(500);
                vibrator.Vibrate(timee);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == 100 && resultCode == Result.Ok)
            {
                int like = data.GetIntExtra("Like", -1);
                vote = like;
            }
        }
    }
}

