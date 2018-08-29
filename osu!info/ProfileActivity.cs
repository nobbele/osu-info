using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace osu_info
{
    [Activity(Label = "ProfileActivity")]
    public class ProfileActivity : Activity
    {
        //Important
        OsuUser user;

        //Other
        ImageView UserIcon;
        ImageView CountryIcon;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.content_main);

            user = new OsuUser(Intent.GetStringExtra("username"), OsuUser.StringToGameMode(Intent.GetStringExtra("gamemode")));
    
            //Make it so if the user name cannot be found return to profile with a Toast telling the error

            //Set user settings
            FindViewById<TextView>(Resource.Id.textUsername).Text = user.Username;
            FindViewById<TextView>(Resource.Id.textCurrentPP).Text = $"{user.PP} pp";
            FindViewById<TextView>(Resource.Id.textAccuracy).Text = $"Hit Accuracy {user.Accuracy}";
            UserIcon = FindViewById<ImageView>(Resource.Id.imageProfileImage);
            CountryIcon = FindViewById<ImageView>(Resource.Id.imageCountry);

            //Others
            UserIcon.SetImageBitmap(Helper.GetImageBitmapFromUrl($"https://a.ppy.sh/{user.ID}"));
            CountryIcon.SetImageBitmap(Helper.GetImageBitmapFromUrl($"https://osu.ppy.sh/images/flags/{user.Country}.png"));
        }

        public override void OnBackPressed()
        {
            Toast.MakeText(this, "Going back", ToastLength.Short).Show();
            this.Finish();
        }
    }
}