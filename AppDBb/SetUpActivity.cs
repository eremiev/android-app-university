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
using SQLite;

namespace AppDBb
{
    [Activity(Label = "Setup")]
    public class SetUpActivity : Activity
    {

        SQLiteConnection db;
        string dbPath = System.IO.Path.Combine(
     System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
     "time.db3");

        public SetUpActivity()
        {
            db = new SQLiteConnection(dbPath);
            db.CreateTable<Items>();
            Items new_item = new Items();
            new_item.Id = 1;
            new_item.Time = "0";
            db.Insert(new_item);
        }

        EditText text;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Setup);

            text = FindViewById<EditText>(Resource.Id.editText1);

            var first = db.Table<Items>().FirstOrDefault();
            if (first != null)
                text.Text = first.Time;

            Button save = FindViewById<Button>(Resource.Id.save);
            save.Click += Save;
        }

        private void Save(object sender, EventArgs e)
        {
            foreach (var item in db.Table<Items>())
            {
                if (item.Id == 1)
                {
                    Items new_item = new Items();
                    new_item.Id = 1;
                    new_item.Time = text.Text;
                    db.Update(new_item);
                    db.Close();
                }
            }
            Finish();
        }
    }
}