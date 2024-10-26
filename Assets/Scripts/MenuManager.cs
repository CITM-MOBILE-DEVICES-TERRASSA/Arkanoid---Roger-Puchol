using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;

    void Start()
    {
        int lives = PlayerPrefs.GetInt("Lives", 0); 
        int score = PlayerPrefs.GetInt("CurrentScore", 0);
        if (lives <= 0 && score == 0)
            continueButton.SetActive(false);
    }

    public void NewGame()
    {
        SaveGame.SG.Load(true);
        SaveGame.SG.Save();
        SceneManager.LoadScene("Game");
    }

    public void LoadGame()
    {
        SaveGame.SG.Load(false);
        SceneManager.LoadScene("Game");
    }
    
}
