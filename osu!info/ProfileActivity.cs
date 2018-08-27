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
        string username = string.Empty;
        string userID = string.Empty;
        string userPP = string.Empty;

        //Other
        ImageView UserIcon;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.content_main);

            Org.Json.JSONObject userObj = OsuApi.Request("get_user", new Dictionary<string, string> { { "k", OsuApi.Key }, { "u", Intent.GetStringExtra("username") }, { "type", "string" } }).GetJSONObject(0);

            userID = userObj.GetString("user_id");
            username = userObj.GetString("username");
            userPP = userObj.GetString("pp_raw");

            //Set user settings
            FindViewById<TextView>(Resource.Id.textUsername).Text = username;
            FindViewById<TextView>(Resource.Id.textCurrentPP).Text = $"{userPP} pp";
            UserIcon = FindViewById<ImageView>(Resource.Id.imageProfileImage);

            //Others
            UserIcon.SetImageBitmap(GetImageBitmapFromUrl(userID));
        }

        public static Bitmap GetImageBitmapFromUrl(string userID)
        {
            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData($"https://a.ppy.sh/{userID}");
                if (imageBytes != null && imageBytes.Length > 0)
                    return BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
            }
            return null;
        }

        public override void OnBackPressed()
        {
            this.Finish();
        }
    }
}