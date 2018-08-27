using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace osu_info
{
    [Activity(Label = "ProfileActivity")]
    public class ProfileActivity : Activity
    {
        string username = string.Empty;
        string userID = string.Empty;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.content_main);

            username = Intent.GetStringExtra("username");

            Org.Json.JSONObject userObj = OsuApi.Request("get_user", new Dictionary<string, string> { { "k", OsuApi.Key }, { "u", username }, { "type", "string" } }).GetJSONObject(0);

            userID = userObj.GetString("user_id");

            FindViewById<TextView>(Resource.Id.textUsername).Text = username;
            FindViewById<ImageView>(Resource.Id.imageProfileImage).SetImageBitmap(GetImageBitmapFromUrl(userID));
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