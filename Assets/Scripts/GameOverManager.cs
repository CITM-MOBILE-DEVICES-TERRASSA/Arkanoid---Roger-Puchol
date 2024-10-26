using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    // UI elements to update
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    
    
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = SaveGame.SG.currentScore.ToString();
        highScoreText.text = SaveGame.SG.highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Action") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
