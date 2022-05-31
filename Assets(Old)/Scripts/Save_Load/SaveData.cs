using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton class
[System.Serializable]
public class SaveData
{
    private static SaveData _current;
    public static SaveData current
    {
        get
        {
            if (_current == null)
            {
                _current = new SaveData();
            }
            return _current;
        }
        set
        {
            _current = value;
        }
    }

    public PlayerData player = new PlayerData();
}
