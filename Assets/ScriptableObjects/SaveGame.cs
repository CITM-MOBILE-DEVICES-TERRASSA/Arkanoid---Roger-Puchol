using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SaveGame", menuName = "ScriptableObjects/SaveGame")]
public class SaveGame : ScriptableObject
{
    public static SaveGame SG;
    
    
    [FormerlySerializedAs("scoreValue")] public int currentScore = 0;
    
    public int highScore;
    public int lives;
    public int currentLevel;

    // gameLoad references if the game was just started
    public void Load(bool newGame)
    {
        highScore = PlayerPrefs.GetInt("HighScore", 200);
        lives = newGame ? 3 : PlayerPrefs.GetInt("Lives", 3);
        currentLevel = newGame ? 0 : PlayerPrefs.GetInt("CurrentLevel", 0);
        currentScore = newGame ? 0 : PlayerPrefs.GetInt("CurrentScore", 0);

        if (lives < 0)
            lives = 3;
    }
    
    public void Save()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.SetInt("Lives", lives);
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.SetInt("CurrentScore", currentScore);
        PlayerPrefs.Save();
    }

}
