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
        string userID = string.Empty;
        string username = string.Empty;
        string userCountry = string.Empty;
        float userPP = 0;

        //Other
        ImageView UserIcon;
        ImageView CountryIcon;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.content_main);

            Org.Json.JSONObject userObj = OsuApi.Request("get_user", new Dictionary<string, string> { { "k", OsuApi.Key }, { "u", Intent.GetStringExtra("username") }, { "type", "string" } }).GetJSONObject(0);
    
            username = userObj.GetString("username");
            userID = userObj.GetString("user_id");
            userCountry = userObj.GetString("country");
            userPP = (int)Math.Round(float.Parse(userObj.GetString("pp_raw")));

            //Set user settings
            FindViewById<TextView>(Resource.Id.textUsername).Text = username;
            FindViewById<TextView>(Resource.Id.textCurrentPP).Text = $"{userPP} pp";
            UserIcon = FindViewById<ImageView>(Resource.Id.imageProfileImage);
            CountryIcon = FindViewById<ImageView>(Resource.Id.imageCountry);

            //Others
            UserIcon.SetImageBitmap(Helper.GetImageBitmapFromUrl($"https://a.ppy.sh/{userID}"));
            CountryIcon.SetImageBitmap(Helper.GetImageBitmapFromUrl($"https://osu.ppy.sh/images/flags/{userCountry}.png"));
        }

        public override void OnBackPressed()
        {
            Toast.MakeText(this, "Going back", ToastLength.Short).Show();
            this.Finish();
        }
    }
}