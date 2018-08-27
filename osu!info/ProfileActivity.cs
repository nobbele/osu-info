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

namespace osu_info
{
    [Activity(Label = "ProfileActivity")]
    public class ProfileActivity : Activity
    {
        string username;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.content_main);

            username = Intent.GetStringExtra("username");
        }

        public override void OnBackPressed()
        {
            this.Finish();
        }
    }
}