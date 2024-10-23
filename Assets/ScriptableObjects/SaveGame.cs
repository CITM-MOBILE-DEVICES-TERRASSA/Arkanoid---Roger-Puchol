using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveGame", menuName = "ScriptableObjects/SaveGame")]
public class SaveGame : ScriptableObject
{
    public static SaveGame SG;
    
    public int highScore;
    public int lives;
    public int lastLevel;

    public void Load()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        lives = PlayerPrefs.GetInt("Lives", 0);
        lastLevel = PlayerPrefs.GetInt("LastLevel", 0);
    }
    
    public void Save()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.SetInt("Lives", lives);
        PlayerPrefs.SetInt("LastLevel", lastLevel);
    }

}
