using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    public static LivesManager LM;
    
    public GameObject livesPrefab;
    public List<GameObject> lives = new List<GameObject>();
    public float offsetX = 0.1538461f;
    
    // Start is called before the first frame update

    void Start()
    {
        LM = this;
        UpdateLives();
    }

    public void UpdateLives()
    {
        for (int i = 0; i < SaveGame.SG.lives; i++)
        {
            Vector2 position = transform.position;
            position.x += offsetX*i;
            lives.Add(Instantiate(livesPrefab,position,Quaternion.identity));
        }
    
        if (lives.Count > 0)
            for (int i = lives.Count; i > SaveGame.SG.lives; i--)
            {
                GameObject liv = lives[i-1];
                lives.Remove(liv);
                Destroy(liv);
            }
    }
}
