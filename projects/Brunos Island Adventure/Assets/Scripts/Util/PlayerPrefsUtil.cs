using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.Util
{
    public static class PlayerPrefsUtil
    {
        public static void SetStrings(string key, List<string> value)
        {
            var formattedValue = string.Join(",", value);

            PlayerPrefs.SetString(key, formattedValue);
        }

        public static List<string> GetStrings(string key)
        {
            string unformattedValue = PlayerPrefs.GetString(key);

            return unformattedValue.Split(',').Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        public static void SetBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        public static bool GetBool(string key)
        {
            return PlayerPrefs.GetInt(key) == 1;
        }
    }
}
