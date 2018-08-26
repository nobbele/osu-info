using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Org.Json;

namespace osu_info
{
    class OsuApi
    {
        public static string Key => OsuApiKey.Key;
        public static string ApiUrl => "https://osu.ppy.sh/api/";

        public static JSONArray Request(string type, Dictionary<string, string> arguments)
        {
            StringBuilder url = new StringBuilder(ApiUrl);
            url.Append(type + "?");

            url.Append($"k={Key}");
            foreach(var pair in arguments)
            {
                url.Append($"&{pair.Key}={pair.Value}");
            }

            JSONArray json;
            using (WebResponse response = WebRequest.CreateHttp(url.ToString()).GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                json = new JSONArray(reader.ReadToEnd());
            }

            return json;
        }
    }
}