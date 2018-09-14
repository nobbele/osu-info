using System;
using System.Collections;
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
    abstract class OsuBeatmapSet : IEnumerable<OsuBeatmap>
    {
        class Dummy : OsuBeatmapSet {}
        public static OsuBeatmapSet CreateFromSetId(string id) => CreateFromJSONArray(GetArrayUsingSetId(id));
        public static OsuBeatmapSet CreateFromJSONArray(Org.Json.JSONArray arr)
        {
            OsuBeatmapSet instance = new Dummy();

            for (int i = 0; i < arr.Length(); i++)
            {
                instance.m_maps.Add(new OsuBeatmap(arr.GetJSONObject(i)));
            }

            return instance;
        }
        List<OsuBeatmap> m_maps = new List<OsuBeatmap>();
        private static Org.Json.JSONArray GetArrayUsingSetId(string id)
        {
            return OsuApi.Request(
                "get_beatmaps", new Dictionary<string, string> {
                    { "k", OsuApi.Key },
                    { "limit", "int" },
                    { "s", id }
                });
        }
        public IEnumerator<OsuBeatmap> GetEnumerator()
        {
            return ((IEnumerable<OsuBeatmap>)m_maps).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<OsuBeatmap>)m_maps).GetEnumerator();
        }

        public class AmbigousDifficultyNameException : Exception
        {
            public override string Message => "Multiple difficulties with same name";
        }
        public OsuBeatmap GetDifficulty(string difficultyName)
        {
            var list = m_maps.Where(map => map.DifficultyName == difficultyName);
            if(list.Count() > 1)
            {
                throw new AmbigousDifficultyNameException();
            }
            return list.First();
        }
    }
}