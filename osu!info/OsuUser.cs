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
    public class HitCounter
    {
        public enum Accuracy
        {
            Hit300, Hit100, Hit50
        }
        private int[] Counts = new int[Enum.GetValues(typeof(Accuracy)).Length];
        public long GetCountFor(Accuracy acc) => Counts[(int)acc];
        public HitCounter(Org.Json.JSONObject obj)
        {
            Counts[(int)Accuracy.Hit300] = obj.GetInt("count300");
            Counts[(int)Accuracy.Hit100] = obj.GetInt("count100");
            Counts[(int)Accuracy.Hit50] = obj.GetInt("count50");
        }
    }
    public class RankCounter
    {
        public enum Rank
        {
            SS, SSH, S, SH, A
        }
        private int[] Counts = new int[Enum.GetValues(typeof(Rank)).Length];
        public long GetCountFor(Rank acc) => Counts[(int)acc];
        public RankCounter(Org.Json.JSONObject obj)
        {
            foreach(var rank in Enum.GetValues(typeof(Rank)).Cast<Rank>())
            {
                Counts[(int)rank] = obj.GetInt($"count_rank_{Enum.GetName(typeof(Rank), rank).ToLower()}");
            }
        }
    }
    public class OsuUser
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
        public string ID = string.Empty;
        public string Username = string.Empty;
        public string Country = string.Empty;
        public long PP = 0;
        public int Level = 0;
        public string Accuracy = string.Empty;
        public string PlayCount = string.Empty;
        public string CountryRank = string.Empty;
        public string GlobalRank = string.Empty;
        public string AccountDate = string.Empty;
        public string RankedScore = string.Empty;
        public string TotalScore = string.Empty;
        public HitCounter HitAccuracyCount;
        public RankCounter RankCount;
        private Org.Json.JSONObject GetObjectUsingName(string name, GameMode gameMode)
        {
            return OsuApi.Request("get_user", new Dictionary<string, string> { { "k", OsuApi.Key }, { "u", name }, { "type", "string" }, { "m", ((int)gameMode).ToString() } }).GetJSONObject(0);
        }
        public OsuUser(string username, GameMode gameMode) {
            Org.Json.JSONObject obj = GetObjectUsingName(username, gameMode);

            ID = obj.GetString("user_id");
            Username = obj.GetString("username");
            //AccountDate = obj.GetString("event_date");
            HitAccuracyCount = new HitCounter(obj);
            PlayCount = obj.GetString("playcount");
            RankedScore = obj.GetString("ranked_score");
            TotalScore = obj.GetString("total_score");
            GlobalRank = obj.GetString("pp_rank");
            Level = obj.GetInt("level");
            PP = obj.GetInt("pp_raw") + 1;
            Accuracy = obj.GetString("accuracy");
            RankCount = new RankCounter(obj);
            Country = obj.GetString("country");
            CountryRank = obj.GetString("pp_country_rank");
        }
    } 
}