using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;

    void Start()
    {
        if (PlayerPrefs.GetInt("Lives", 0) <= 0)
            continueButton.SetActive(false);
    }

    public void NewGame()
    {
        SaveGame.SG.Load(true);
        SceneManager.LoadScene("Game");
    }

    public void LoadGame()
    {
        SaveGame.SG.Load(false);
        SceneManager.LoadScene("Game");
    }
    
}
