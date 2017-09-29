using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Study.Motion.Web
{
    public class JsonParse
    {
        public static MotionClass.MotionResult Parse(string json)
        {
            var settings = new JsonSerializerSettings();
            settings.MissingMemberHandling = MissingMemberHandling.Error;

            var jsonObj = JsonConvert.DeserializeObject<MotionClass.MotionResult>(json, settings);
            return jsonObj;
        }

        public static string Json(MotionClass.MotionResult results)
        {
            string json = JsonConvert.SerializeObject(results, Newtonsoft.Json.Formatting.Indented);
            return json;
        }

        public static string Json_word(List<MotionClass.MotionWords> words)
        {
            string json = JsonConvert.SerializeObject(words, Newtonsoft.Json.Formatting.Indented);
            return json;
        }
    }
}