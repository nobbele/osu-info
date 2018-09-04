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
    [Activity(Label = "BeatmapActivity")]
    public class BeatmapActivity : Activity
    {
        //Important
        OsuBeatmap currentBeatmap;
        string beatmapID;

        //Other
        TextView BeatmapTitleText;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.beatmap_main);

            beatmapID = Intent.GetStringExtra("beatmapID");
            currentBeatmap = new OsuBeatmap(beatmapID);
            
            //UI
            BeatmapTitleText = FindViewById<TextView>(Resource.Id.beatmapName);

            //Other
            ReloadBeatmap();
        }

        void ReloadBeatmap()
        {
            BeatmapTitleText.Text = currentBeatmap.Title;
        }
    }
}