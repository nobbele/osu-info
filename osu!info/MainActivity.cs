using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace osu_info
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            //Toolbar
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            OsuApi.Request("get_user", new Dictionary<string, string> { { "u", "nobbele" }, { "m", "0" }, { "type", "string" } });
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }
            if(id == Resource.Id.menu_profile)
            {
                Android.App.AlertDialog.Builder aDialog;
                aDialog = new Android.App.AlertDialog.Builder(this);
                EditText editText = new EditText(this);

                aDialog.SetTitle("Search For User");
                aDialog.SetView(editText);
                aDialog.SetNegativeButton("Search!", delegate 
                {
                    if(editText.Text != string.Empty)
                    {
                        //Fill it up with required funtions
                        Toast.MakeText(this, $"Searching for {editText.Text}", ToastLength.Short).Show();
                        Intent profileActivity = new Intent(this, typeof(ProfileActivity));
                        StartActivity(profileActivity);
                    }
                    else
                    {
                        Toast.MakeText(this, $"You cant leave this part empty", ToastLength.Short).Show();
                    }
                });
                aDialog.Show();
            }
            if (id == Resource.Id.menu_beatmaps)
            {
                //Put Functions Here
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }
	}
}

