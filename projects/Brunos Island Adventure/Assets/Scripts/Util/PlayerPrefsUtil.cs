using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.Util
{
    public static class PlayerPrefsUtil
    {
        // TODO - refactor to extension method?
        public static void SetString(string key, List<string> value)
        {
            var formattedValue = string.Join(",", value);

            PlayerPrefs.SetString(key, formattedValue);
        }

        public static List<string> GetString(string key)
        {
            string unformattedValue = PlayerPrefs.GetString(key);

            Debug.Log(unformattedValue);

            return unformattedValue.Split(',').Where(x => !string.IsNullOrEmpty(x)).ToList();
        }
    }
}
