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
    class OsuBeatmapSet : IEnumerable<OsuBeatmap>
    {
        List<OsuBeatmap> m_maps = new List<OsuBeatmap>();
        private Org.Json.JSONArray GetArrayUsingSetId(string id)
        {
            return OsuApi.Request(
                "get_beatmaps", new Dictionary<string, string> {
                    { "k", OsuApi.Key },
                    { "limit", "int" },
                    { "s", id }
                });
        }
        public OsuBeatmapSet(string id)
        {
            Org.Json.JSONArray arr = GetArrayUsingSetId(id);

            for(int i = 0; i < arr.Length(); i++)
            {
                m_maps.Add(new OsuBeatmap(arr.GetJSONObject(i)));
            }
        }

        public IEnumerator<OsuBeatmap> GetEnumerator()
        {
            return ((IEnumerable<OsuBeatmap>)m_maps).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<OsuBeatmap>)m_maps).GetEnumerator();
        }

        public OsuBeatmap GetDifficulty(string difficultyName)
        {
            return m_maps.Single(map => map.DifficultyName == difficultyName);
        }
    }
}