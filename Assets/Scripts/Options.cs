using UnityEngine;


public static class Options {
    private static readonly string _music = "music volume";
    private static readonly string _effects = "effects volume";
    private static readonly string _autofire = "autofire";
    private static readonly string _player_name = "player name";

    public static float MusicVolume {
        get {
            return PlayerPrefs.GetFloat(_music);
        }

        set {
            PlayerPrefs.SetFloat(_music, value);
        }
    }

    public static float EffectsVolume {
        get {
            return PlayerPrefs.GetFloat(_effects);
        }

        set {
            PlayerPrefs.SetFloat(_effects, value);
        }
    }

    public static bool Autofire {
        get {
            return PlayerPrefsExt.GetBool(_autofire);
        }

        set {
            PlayerPrefsExt.SetBool(_autofire, value);
        }
    }

    public static string PlayerName {
        get {
            return PlayerPrefs.GetString(_player_name, PlayerData.GenerateRandomName());
        }

        set {
            PlayerPrefs.SetString(_player_name, value);
        }
    }

    /// <summary>
    /// Extensions for Unity PlayerPrefs class
    /// </summary>
    private static class PlayerPrefsExt {
        /// <summary>
        /// Returns bool value from PlayerPrefs.
        /// </summary>
        /// <param name="name">Setting name.</param>
        /// <param name="defaultValue">Default value if setting was not set.</param>
        /// <returns>Setting value.</returns>
        public static bool GetBool(string name, bool defaultValue = false) {
            return PlayerPrefs.GetInt(name, defaultValue ? 1 : 0) == 1;
        }

        /// <summary>
        /// Sets bool setting.
        /// </summary>
        /// <param name="name">Setting name.</param>
        /// <param name="value">Setting value.</param>
        public static void SetBool(string name, bool value) {
            PlayerPrefs.SetInt(name, value ? 1 : 0);
        }
    }
}