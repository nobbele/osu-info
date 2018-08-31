using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace osu_info
{
    public struct Difficulty
    {
        public double StarRating;
        public double CS;
        public double OD;
        public double AR;
        public double HP;

        public Difficulty(Org.Json.JSONObject obj)
        {
            StarRating = obj.GetDouble("difficultyrating");
            CS = obj.GetDouble("diff_size");
            OD = obj.GetDouble("diff_overall");
            AR = obj.GetDouble("diff_approach");
            HP = obj.GetDouble("diff_drain");
        }
    }
    public class OsuBeatmap
    {
        private Org.Json.JSONObject GetObjectUsingMapId(string id)
        {
            return OsuApi.Request(
                "get_beatmaps", new Dictionary<string, string> {
                    { "k", OsuApi.Key },
                    { "limit", "int" },
                    { "b", id }
                }).GetJSONObject(0);
        }
        public enum ApproveState
        {
            Graveyard = -2,
            WIP = -1,
            Pending = 0,
            Ranked = 1,
            Approved = 2,
            Qualified = 3,
            Loved = 4
        }
        //0 = any, 1 = unspecified, 2 = video game, 3 = anime, 4 = rock, 5 = pop, 6 = other, 7 = novelty, 9 = hip hop, 10 = electronic(note that there's no 8)
        public enum GenreType
        {
            Any = 0,
            Unspecified = 1,
            Video_Game = 2,
            Anime = 3,
            Rock = 4,
            Pop = 5,
            Other = 6,
            Novelty = 7,
            Hip_Hop = 9,
            Electroni = 10
        }
        // 0 = any, 1 = other, 2 = english, 3 = japanese, 4 = chinese, 5 = instrumental, 6 = korean, 7 = french, 8 = german, 9 = swedish, 10 = spanish, 11 = italian
        public enum LanguageType
        {
            Any = 0,
            Other = 1,
            English = 2,
            Japanese = 3,
            Chinese = 4,
            Instrumental = 5,
            Korean = 6,
            French = 7,
            German = 8,
            Swedish = 9,
            Spanish = 10,
            Italian = 11
        }
        public ApproveState Approved;
        public string ApprovedDate;
        public string LastUpdate;
        public string Artist;
        public string Id;
        public string SetId;
        public string Bpm;
        public string Creator;
        public Difficulty Difficulty;
        public long HitLength;
        public string Source;
        public GenreType Genre;
        public LanguageType Language;
        public string Title;
        public long TotalLength;
        public string DifficultyName;
        public string MD5;
        public GameMode Mode;
        private string m_tags;
        public string[] Tags => m_tags.Split(' ');
        public long FavoriteCount;
        public long PlayCount;
        public long PassCount;
        public long MaxCombo;
        public OsuBeatmap(string id)
        {
            Org.Json.JSONObject obj = GetObjectUsingMapId(id);
            //Parse it
            Approved = (ApproveState)obj.GetInt("approved");
            ApprovedDate = obj.GetString("approved_date");
            LastUpdate = obj.GetString("last_update");
            Artist = obj.GetString("artist");
            Id = obj.GetString("beatmap_id");
            SetId = obj.GetString("beatmapset_id");
            Bpm = obj.GetString("bpm");
            Creator = obj.GetString("creator");
            obj.GetString("difficultyrating");
            Difficulty = new Difficulty(obj);
            HitLength = obj.GetLong("hitlength");
            Source = obj.GetString("source");
            Genre = (GenreType)obj.GetInt("genre");
            Language = (LanguageType)obj.GetInt("language");
            Title = obj.GetString("title");
            TotalLength = obj.GetLong("total_length");
            DifficultyName = obj.GetString("version");
            MD5 = obj.GetString("file_md5");
            Mode = (GameMode)obj.GetInt("mode");
            m_tags = obj.GetString("tags");
            FavoriteCount = obj.GetLong("favourite_count");
            PlayCount = obj.GetLong("playcount");
            PassCount = obj.GetLong("passcount");
            MaxCombo = obj.GetLong("maxcombo");
        }
    }
}