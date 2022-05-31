using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerPreferences
{
    private static PlayerPreferences _current;
    public static PlayerPreferences current
    {
        get
        {
            if (_current == null)
            {
                _current = new PlayerPreferences();
            }
            return _current;
        }
        set
        {
            _current = value;
        }
    }

    public float masterVolumeLevel;
    public float musicVolumeLevel;
    public float sfxVolumeLevel;
}
