using System;
using System.Collections;
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

        List<string> gameTypeList = new List<string> { "osu!", "osu!taiko", "osu!catch", "osu!mania" };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            //Toolbar
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;
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
                LinearLayout layout = new LinearLayout(this);
                layout.Orientation = Orientation.Vertical;
                EditText usernamePopup = new EditText(this);
                Spinner gameTypeSpinner = new Spinner(this);

                ArrayAdapter gameTypes = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, gameTypeList);
                gameTypeSpinner.Adapter = gameTypes;

                aDialog.SetTitle("Search For User");
                layout.AddView(usernamePopup);
                layout.AddView(gameTypeSpinner);
                aDialog.SetView(layout);
                aDialog.SetNegativeButton("Search!", delegate 
                {
                    if(usernamePopup.Text != string.Empty)
                    { 
                        //Fill it up with required funtions
                        Toast.MakeText(this, $"Searching for \"{usernamePopup.Text}\" ", ToastLength.Short).Show();
                        Intent profileActivity = new Intent(this, typeof(ProfileActivity));
                        profileActivity.PutExtra("username", usernamePopup.Text);
                        profileActivity.PutExtra("gamemode", gameTypeList[gameTypeSpinner.SelectedItemPosition]);
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
                string beatmapID = "1655222";
                OsuBeatmap beatmap = new OsuBeatmap(beatmapID);
                Toast.MakeText(this, $"Beatmap {beatmap.Title} \n Made By \"{beatmap.Artist}\" ", ToastLength.Short).Show();
                //Fill it up with required funtions
                Intent beatmapActivity = new Intent(this, typeof(BeatmapActivity));
                beatmapActivity.PutExtra("beatmapID", beatmapID);
                StartActivity(beatmapActivity);
            }
            if (id == Resource.Id.menu_news)
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

