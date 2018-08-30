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
    public class OsuBeatmap
    {
        public static GameMode StringToGameMode(string mode)
        {
            mode = mode.Replace("osu!", "");
            if (mode == string.Empty)
                mode = "standard";
            return (GameMode)Enum.Parse(typeof(GameMode), mode, true);
        }
        public enum GameMode
        {
            Standard, Taiko, CatchTheBeat, Mania
        }
        private Org.Json.JSONObject GetObjectUsingName(string name, GameMode gameMode)
        {
            return OsuApi.Request("get_beatmaps", new Dictionary<string, string> { { "k", OsuApi.Key }, { "limit", "int" }, { "type", "string" }, { "m", ((int)gameMode).ToString() } }).GetJSONObject(0);
        }
    }
}