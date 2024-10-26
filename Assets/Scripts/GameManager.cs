using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum PowerupState
{
    Normal = 0,
    Wide,
    Magnetic,
    Small
}

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    
    // Sprites and animations to assign to each brick
    public SpriteStorage sprites;

    public Level[] levelList;
    
    // Current level data
    public Level level;

    public GameObject brickPrefab;
    
    // Starting point for the brick grid
    public GameObject gridStart;
    
    // UI elements to update
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    
    private AudioSource audio;
    
    public AudioClip[] audioClips;
    
    // Size of the brick sprite for proper separation
    [Min(0.01f)]
    public Vector2 brickSize;

    public List<BrickScript> bricks;
    
    // Start is called before the first frame update
    void Start()
    {
        //Singleton
        if (GM != null)
        {
            Destroy(this);
            return;
        }
        
        audio = GetComponent<AudioSource>();
        
        SaveGame.SG.Load(false);
        
        LoadLevel(SaveGame.SG.currentLevel);
        
        UpdateScore();

        GM = this;
    }

    private void OnDestroy() => GM = null;

    // Loads the specified level
    private void LoadLevel(int index)
    {
        level = levelList[index];
        
        int columns = 13;
        int rows = level.layout.Length / columns;
        if (rows < 1) rows = 1;
        
        // Spawn the block grid
        for (int j = 0; j < rows; j++)
        {
            for (int i = 0; i < columns; i++)
            {
                BrickColor color = level.layout[j * columns + i];
                if (color == BrickColor.None) continue;
                
                var pos = gridStart.transform.position;
                pos.x += i * brickSize.x;
                pos.y -= j * brickSize.y;
                
                var instance = Instantiate(brickPrefab, pos, Quaternion.identity)?.GetComponent<BrickScript>();
                
                // If successfully instanced, add to the brick list and set the color/type of the instance
                if (instance)
                {
                    instance.brickColor = color;
                    if (color != BrickColor.Gold) // Unbreakable blocks don't count toward victory condition
                        bricks.Add(instance);
                    var anim = instance.GetComponent<Animator>();
                    if (anim)
                    {
                        anim.runtimeAnimatorController = sprites.brickAnimations[(int)color];
                    }
                }

            }
        }
    }

    public void DestroyBrick(BrickScript brick)
    {
        bricks.Remove(brick);
        if (bricks.Count == 0) 
            NextLevel();
    }
    
    void UpdateScore()
    {
        if (SaveGame.SG.currentScore > SaveGame.SG.highScore)
        {
            SaveGame.SG.highScore = SaveGame.SG.currentScore;
        }
        scoreText.text = SaveGame.SG.currentScore.ToString();
        highScoreText.text = SaveGame.SG.highScore.ToString();
    }

    public void LifeLost()
    {
        --SaveGame.SG.lives;
        Debug.Log("Life lost. Lives remaining: " + (SaveGame.SG.lives));

        if (SaveGame.SG.lives <= 0)
            GameOver();
        else
        {
            audio.PlayOneShot(audioClips[1]);
            Invoke(nameof(Respawn), 2.0f);
        }

    }

    void Respawn()
    {
        LivesManager.LM.UpdateLives();
        GetComponent<AudioSource>().PlayOneShot(audioClips[0]);
        PlayerController.PC.GrabBall(FindObjectOfType<Ball>());
    }

    public void NextLevel()
    {
        if (++SaveGame.SG.currentLevel >= levelList.Length)
            SaveGame.SG.currentLevel = 0;
        
        SaveGame.SG.Save();
        
        FindObjectOfType<Ball>().rb.velocity = Vector2.zero;
        
        Invoke(nameof(ReloadScene),3.0f);
    }

    void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    
    void GameOver()
    {
        SaveGame.SG.Save();
        Invoke(nameof(LoadGameOver),3.0f);
    }
    
    void LoadGameOver() => SceneManager.LoadScene("GameOver");

    public void AddScore(int score)
    {
        SaveGame.SG.currentScore += score;
        UpdateScore();
    }

}
